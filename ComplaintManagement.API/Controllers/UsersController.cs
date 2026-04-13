using ComplaintManagement.API.Models;
using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
            => Ok(await _userService.GetUsersAsync());

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            // 1️⃣ Create Identity user
            var identityUser = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(identityUser, dto.Role);

            // 2️⃣ Create business user (Dapper + SP)
            await _userService.CreateBusinessUserAsync(new CreateBusinessUserDto
            {
                UserId = identityUser.UserName,
                Employee_Code = dto.Employee_Code,
                Email = dto.Email,
                Full_Name = dto.Full_Name,
                Department = dto.Department,

                Salutation = dto.Salutation,
                Job_title = dto.Job_title,
                Vip = dto.Vip,

                Phone = dto.Phone,
                Phone_Ext = dto.Phone_Ext,
                Mobile = dto.Mobile,

                Manager = dto.Manager,
                Access_Type = dto.Access_Type,
                Time_Zone = dto.Time_Zone,

                Division = dto.Division,
                Region = dto.Region,
                Branch = dto.Branch,
                Location = dto.Location,

                City = dto.City,
                State = dto.State,
                Country = dto.Country,
                Zip = dto.Zip,
                Address = dto.Address
            });

            return Ok();
        }
    }
}
