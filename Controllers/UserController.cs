using ExpensesTracker.DTO;
using ExpensesTracker.DTO.UserDTOs;
using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using ExpensesTracker.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace ExpensesTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        AppUnitOfWork unitOfWork;
        public UserController(AppUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }      

        [HttpPut("change-income")]
        [Authorize]
        public IActionResult changeIncome([FromBody] decimal newIncome)
        {
            var userId = getUserId();
            if (userId == null)
                return Unauthorized("Unauthorized user");

            var user = unitOfWork.UserRepo.getById(userId.Value);
            
            decimal incomeDifference = Math.Abs(newIncome - user.Income);

            if (newIncome > user.Income)                         
                user.Balance += incomeDifference;
            
            else           
                user.Balance -= incomeDifference;

            user.Income = newIncome;

            unitOfWork.UserRepo.Update(user);
            unitOfWork.Save();

            return Ok();
        }

        [HttpPut("income")]
        [Authorize]
        public IActionResult addIncome([FromBody] decimal amount)
        {
            var userId = getUserId();
            if (userId == null)
                return Unauthorized("Unauthorized user");

            User user = unitOfWork.UserRepo.getById(userId.Value);

            user.Income = user.Income + amount;
            user.Balance = user.Balance + amount;

            unitOfWork.UserRepo.Update(user);
            unitOfWork.Save();

            return Ok();
        }

    }
}
