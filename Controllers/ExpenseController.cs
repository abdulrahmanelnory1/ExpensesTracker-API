using ExpensesTracker.Controllers;
using ExpensesTracker.DTO;
using ExpensesTracker.DTO.ExpenseDTOs;
using ExpensesTracker.DTO.ItemDTOs;
using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using ExpensesTracker.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ExpenseController : BaseController
    {
        AppUnitOfWork unitOfWork;
        public ExpenseController(AppUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var userID = getUserId();

            if (userID == null)
                return Unauthorized("User Unauthenticated.");

            List<Expense> expenses = unitOfWork.ExpenseRepo.getByUserId(userID.Value);
            return Ok(converToDTO(expenses));
        }

        [HttpGet("by-date")]
        public IActionResult getByDate([FromQuery] DateOnly date)
        {
            var userID = getUserId();

            if (userID == null)
                return Unauthorized("User Unauthorized.");

            List<Expense> expenses = unitOfWork.ExpenseRepo.getByDate(userID.Value, date);

            return Ok(converToDTO(expenses));
        }

        [HttpGet("sorted-by-date")]
        public IActionResult sortedByDate()
        {
            List<Expense> expenses = unitOfWork.ExpenseRepo.sortByDate();
            return Ok(converToDTO(expenses));
        }

        // Add a new expense
        // Add all items first then add the expense
        // Make sure the user has enough income to cover the expense
        // Update the user's income after adding the expense
        // Update the budget spent amount if the item category has a budget        
        [HttpPost]
        public IActionResult Add([FromBody] RequestExpenseDTO expense)
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("user not authenticated.");

            User user = unitOfWork.UserRepo.getById(userID.Value);

            // Items to be added to the new expense


            decimal amount = expense.Items.Select(i => i.Price).Sum();

            Expense newExpense = new Expense()
            {
                Amount = amount,
                Date = expense.Date,
                UserId = userID.Value,
            };
            unitOfWork.ExpenseRepo.Add(newExpense);

            foreach (RequestItemDTO itemToAdd in expense.Items)
            {
                Category category = unitOfWork.CategoryRepo.getById(itemToAdd.CategoryId);

                // Update budget if category has one. 
                if (category.Budget != null && validBudget(category.Budget, DateTime.Now))
                    category.Budget.CurrentLimit -= itemToAdd.Price;

                Item newItem = new Item()
                {
                    Price = itemToAdd.Price,
                    Name = itemToAdd.Name,
                    Category = category,
                    Expense = newExpense, // Associate the item with the expense
                };
                unitOfWork.ItemRepo.Add(newItem);
                newExpense.Items.Add(newItem); // Add the item to the expense's item list
            }

            user.Balance -= amount;

            unitOfWork.Save();

            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("user not authenicated.");

            Expense expense = unitOfWork.ExpenseRepo.getById(id);

            if (expense == null || expense.UserId != userID.Value)
                return NotFound("Expense not found.");

            User user = unitOfWork.UserRepo.getById(userID.Value);
            user.Balance += expense.Amount;
            unitOfWork.Save();

            // Update each item's Budget by increasing its CurrentLimit amount
            foreach (var item in expense.Items)
            {
                Category category = item.Category;
                Budget budget = category.Budget;

                if (budget != null && validBudget(budget, expense.Date))
                    budget.CurrentLimit += item.Price;

                unitOfWork.ItemRepo.Delete(item);
            }
            unitOfWork.ExpenseRepo.Delete(expense);
            unitOfWork.Save();

            return Ok("Expense deleted successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] RequestExpenseDTO expense)
        {
            var userID = getUserId();
            if (userID == null)
                return Unauthorized("User Unauthorized.");

            User user = unitOfWork.UserRepo.getById(userID.Value);

            Expense oldExpense = unitOfWork.ExpenseRepo.getById(id);

            if (oldExpense == null)
                return NotFound("Expense not found.");

            // Cancle the old expense effect on the user's balance
            user.Balance += oldExpense.Amount;

            decimal newAmount = expense.Items.Select(i => i.Price).Sum(); ;

            List<RequestItemDTO> newItems = expense.Items;

            List<Item> oldItems = unitOfWork.ExpenseRepo.getItemsByExpenseId(oldExpense.Id);

            // Revert the budget current limit amounts for old items
            foreach (var oldItem in oldItems)
            {
                Category category = unitOfWork.CategoryRepo.getById(oldItem.CategoryId);
                if (category.Budget != null && validBudget(category.Budget, oldExpense.Date))
                {
                    category.Budget.CurrentLimit += oldItem.Price;
                }
                unitOfWork.ItemRepo.Delete(oldItem);
                oldExpense.Items.Remove(oldItem); // Disassociate the item from the expense
            }

            // Add the new items to the database and associate them with the expense
            foreach (RequestItemDTO newItem in newItems)
            {
                Category category = unitOfWork.CategoryRepo.getById(newItem.CategoryId);
                if (category.Budget != null && validBudget(category.Budget, expense.Date)) //------ Notify the user ---------------             
                    category.Budget.CurrentLimit -= newItem.Price;

                Item item = new Item()
                {
                    Expense = oldExpense,
                    Price = newItem.Price,
                    Name = newItem.Name,
                    Category = category,
                };
                // Add the the database then to the list of items to be added to the expense
                oldExpense.Items.Add(item);
            }

            // update the DB
            oldExpense.Amount = newAmount;
            unitOfWork.ExpenseRepo.Update(oldExpense);

            // Update the user's income
            user.Balance -= newAmount;
            unitOfWork.Save();

            return Ok();
        }
                
        private List<ResponseExpenseDTO> converToDTO(List<Expense> expenses)
        {
            List<ResponseExpenseDTO> expensesDTO = new List<ResponseExpenseDTO>();
            foreach (var expense in expenses)
            {
                ResponseExpenseDTO expenseDTO = new ResponseExpenseDTO()
                {
                    amount = expense.Amount,
                    Date = expense.Date                    
                };

                foreach (var item in expense.Items)
                {
                    ResponseItemDTO itemDTO = new ResponseItemDTO()
                    {
                        Name = item.Name,
                        Price = item.Price,
                        CategoryName = item.Category.Name,
                    };
                    expenseDTO.items.Add(itemDTO);
                }

                expensesDTO.Add(expenseDTO);               
            }
            return expensesDTO;
        }


    }
}