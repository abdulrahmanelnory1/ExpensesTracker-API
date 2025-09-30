using ExpensesTracker.DTO.UserDTOs;
using ExpensesTracker.Models;
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
    public class AuthController : BaseController
    {
        AppUnitOfWork unitOfWork;
        public AuthController(AppUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] LoginDTO userData)
        {
            var user = unitOfWork.UserRepo.getByEmail(userData.Email);

            if (!ModelState.IsValid)
                return BadRequest();

            if (user == null || user.Password != userData.Password)
            {
                return Unauthorized("Invalid Email or Password");
            }
            // Generate JWT Token
            string userToken = GenerateJwtToken(user.Email, user.Name, user.Id, user.Income);

            return Ok(userToken);
        }

        [HttpPost("register")]
        public IActionResult signUp([FromBody] SignUpDTO userDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User newUser = new User()
            {
                Name = userDate.Name,
                Email = userDate.Email,
                Password = userDate.Password,
                Balance = 0,
                Income = 0
            };

            unitOfWork.UserRepo.Add(newUser);
            unitOfWork.Save();

            // Generate JWT Token
            string userToken = GenerateJwtToken(newUser.Email, newUser.Name, newUser.Id, newUser.Income);

            return Ok(userToken);
        }

        [HttpPut("email")]
        public IActionResult changeEmail([FromBody] ChangeEmailDTO data)
        {
            var user = unitOfWork.UserRepo.getByEmail(data.OldEmail);

            if (user == null)
            {
                return NotFound("User Not Found");
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (user.Password != data.Password)
            {
                return Unauthorized("Invalid Email or Password");
            }

            user.Email = data.NewEmail;
            unitOfWork.Save();

            return Ok();
        }

        [HttpPut("pssword")]
        [Authorize]
        public IActionResult changePassword([FromBody]ChangePasswordDTO userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = getUserId();
            if (userId == null)
                return Unauthorized("User is not authorized");

            User user = unitOfWork.UserRepo.getById(userId.Value);

            if (user.Email != userData.Email || user.Password != userData.OldPassword)
            {
                return Unauthorized("User or passwprd is not valid");
            }

            if (userData.NewPassword != userData.ConfirmPassword)
                return BadRequest("con not confirm the new password");

            user.Password = userData.NewPassword;

            unitOfWork.UserRepo.Update(user);
            unitOfWork.Save();

            return Ok();
        }

        private string GenerateJwtToken(string email, string name, int id, decimal income)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier , id.ToString()),
                new Claim("Income", income.ToString())
            };
            SecurityKey secKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is my key, Abdulrahman Elnory"));
            var signCre = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: signCre
            );
            var stringtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return stringtoken;
        }

    }
}
