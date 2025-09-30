
using ExpensesTracker.DTO.CategoryDTOs;
using ExpensesTracker.DTO.BudgetDTOs;
using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using ExpensesTracker.UnitOfWork;

using ExpensesTracker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System;
using System.Globalization;

namespace ExpensesTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : BaseController
    {
        AppUnitOfWork unitOfWork;
        public CategoryController(AppUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("User Unauthenticated.");

            List<Category> categories = unitOfWork.CategoryRepo.GetAll(userID.Value);

            List<ResponseCategoryDTO> categoriesToBeReturned = new List<ResponseCategoryDTO>();

            foreach (Category category in categories)
            {

                ResponseCategoryDTO categoryToBeReturned = new ResponseCategoryDTO()
                {
                    Name = category.Name
                };
                ResponseBudgetDTO budgetToBeReturned = new ResponseBudgetDTO();
                if (category.Budget != null)
                {
                    budgetToBeReturned.CurrentLimit = category.Budget.Limit;
                    budgetToBeReturned.Limit = category.Budget.Limit;
                    budgetToBeReturned.EndDate = category.Budget.EndDate;
                    budgetToBeReturned.StartDate = category.Budget.StartDate;

                    categoryToBeReturned.Budget = budgetToBeReturned;
                }
                else
                {
                    categoryToBeReturned.Budget = null;
                }

                categoriesToBeReturned.Add(categoryToBeReturned);
            }
            return Ok(categoriesToBeReturned);
        }

        [HttpGet("{id}")]
        public IActionResult getByID(int id)
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("User Unauthenticated.");

            Category category = unitOfWork.CategoryRepo.getById(id);

            if (category == null)
                return NotFound("Category not found");

            ResponseBudgetDTO budgetToBeReturned = new ResponseBudgetDTO();

            ResponseCategoryDTO categoryToBeReturned = new ResponseCategoryDTO()
            {
                Name = category.Name
            };
            if (category.Budget != null)
            {
                budgetToBeReturned.CurrentLimit = category.Budget.Limit;
                budgetToBeReturned.Limit = category.Budget.Limit;
                budgetToBeReturned.EndDate = category.Budget.EndDate;
                if (budgetToBeReturned.StartDate == null)
                    budgetToBeReturned.StartDate = DateOnly.FromDateTime(DateTime.Today);

                categoryToBeReturned.Budget = budgetToBeReturned;
            }
            else
            {
                categoryToBeReturned.Budget = null;
            }

            return Ok(categoryToBeReturned);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = unitOfWork.CategoryRepo.getById(id);

            if (category == null)
                return NotFound("Category not found");

            // Prevent deletion of default category
            if (category.UserId == null)
            {
                return BadRequest("can not delete a default category");
            }
            // Assign all items to the "Other" category so each item has category to keep it consistent.
            Category defaultCategory = unitOfWork.CategoryRepo.GetByName("Other");
            if (defaultCategory == null)
                return BadRequest("Default category 'Other' not found. Cannot reassign items.");

            foreach (Item item in unitOfWork.CategoryRepo.GetItems(id))
            {
                item.CategoryId = defaultCategory.Id;

                if (defaultCategory.Budget != null && validBudget(category.Budget, DateTime.Now))
                    defaultCategory.Budget.CurrentLimit -= item.Price;

                unitOfWork.ItemRepo.Update(item);
            }
            unitOfWork.CategoryRepo.Delete(category);

            unitOfWork.Save();

            return Ok("Category deleted successfully");
        }

        // Add a new category
        // Add a new budget if provided
        [HttpPost]
        public IActionResult Add([FromBody] RequestCategoryDTO category)
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("User Unauthorized.");

            var user = unitOfWork.UserRepo.getById(userID.Value);

            if (user == null)
                return Unauthorized("User Unauthorized.");

            if (!ModelState.IsValid)
                return BadRequest();

            string categoryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.Name.ToLower());

            if (unitOfWork.CategoryRepo.GetByName(categoryName) != null)
                return BadRequest("the category already exists");

            Category newCategory = new Category()
            {
                Name = categoryName,
                User = user,
            };

            Budget newBudget = new Budget();

            if (category.Budget != null)
            {
                RequestBudgetDTO budget = category.Budget;

                if (budget.StartDate >= category.Budget.EndDate)
                    return BadRequest("Budget start date must be before end date.");
                if (budget.Limit <= 0)
                    return BadRequest("Budget limit must be greater than zero.");
                if (budget.StartDate < DateOnly.FromDateTime(DateTime.Today))
                    return BadRequest("Budget start date cannot be in the past.");

                newBudget.Limit = budget.Limit;
                newBudget.EndDate = budget.EndDate;
                newBudget.Category = newCategory;
                newBudget.StartDate = budget.StartDate;
                if(newBudget.StartDate == null)
                    newBudget.StartDate = DateOnly.FromDateTime(DateTime.Today);
                newBudget.CurrentLimit = budget.Limit;
                newBudget.User = user;

                unitOfWork.BudgetRepo.Add(newBudget);
                newCategory.Budget = newBudget;

            }
            else
            {
                newCategory.Budget = null;
            }

            unitOfWork.CategoryRepo.Add(newCategory);
            unitOfWork.Save();

            return Created();
        }

        // Update budget current limit according to the new limit and reapply the effect of all items on the current limit within the new budget period
        // Customized category:- update name and budget
        // Default category:- update budget only
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RequestCategoryDTO newCategory)
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("User Unauthenticated.");

            Category existingCategory = unitOfWork.CategoryRepo.getById(id);

            if (existingCategory == null)
                return NotFound("Category not found");

            if (!ModelState.IsValid)
                return BadRequest();

            string newCategoryName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newCategory.Name.ToLower());

            if (newCategory.Budget != null)
            {
                RequestBudgetDTO newBudget = newCategory.Budget;
                Budget existingBudget = existingCategory.Budget;

                if (existingBudget == null)
                {
                    existingBudget = new Budget()
                    {
                        CurrentLimit = newBudget.Limit,
                        Limit = newBudget.Limit,
                        EndDate = newBudget.EndDate,
                        StartDate = newBudget.StartDate,
                        CategoryId = id,
                        UserId = userID.Value
                    };
                    
                    if(existingBudget.StartDate == null)
                        existingBudget.StartDate = DateOnly.FromDateTime(DateTime.Today);

                    unitOfWork.BudgetRepo.Add(existingBudget);
                    existingCategory.Budget = existingBudget;
                }
                else
                {
                    existingBudget.Limit = newBudget.Limit;
                    existingBudget.EndDate = newBudget.EndDate;
                    existingBudget.StartDate = newBudget.StartDate;
                    if (existingBudget.StartDate == null)
                        existingBudget.StartDate = DateOnly.FromDateTime(DateTime.Today);
                    existingBudget.CurrentLimit = newBudget.Limit;

                    // reapply the effect of all items on the current limit within the new budget period
                    foreach (Item item in unitOfWork.CategoryRepo.GetItems(existingCategory.Id))
                    {
                        if (validBudget(existingBudget, item.Expense.Date))
                            existingBudget.CurrentLimit -= item.Price;
                    }
                    unitOfWork.BudgetRepo.Update(existingBudget);
                }
            }
            // No budget provided => remove existing budget if exists
            else
            {
                if(existingCategory.Budget != null)
                {
                    unitOfWork.BudgetRepo.Delete(existingCategory.Budget);
                    existingCategory.Budget = null;
                }
            }
            // Default category:- update budget only
            if (existingCategory.UserId == null)
            {
                unitOfWork.CategoryRepo.Update(existingCategory);
                unitOfWork.Save();
                return BadRequest("can not update the name of a default category, other information has been updated");
            }
            // Customized category:- update budget and name
            else
            {
                existingCategory.Name = newCategoryName;
                unitOfWork.CategoryRepo.Update(existingCategory);
                unitOfWork.Save();
            }
            return Ok();
        }

    }
}
