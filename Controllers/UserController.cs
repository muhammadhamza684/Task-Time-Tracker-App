using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Globalization;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Services;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUser(int pageNO=1, int pageSize=2)
        {
            var Result = await _userService.GetAllUserAsync(pageNO, pageSize);
            return Ok(Result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var SingleUser = await _userService.GetByIdAsync(id);
            return Ok(SingleUser);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user )
        {
            var CreateUser = await _userService.CreateUserAsync(user);
            return Ok(CreateUser);  
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var Result = await _userService.DeleteUserAsync(id);
            return Ok("User Deleted Successfully");  
        }

        [HttpPut]
        
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var updateUserResult = await _userService.UpdateUserAsync(id, user);

            return Ok("User Updated Successfully");
        }

        [HttpGet("export")]
       
        public async Task<FileResult> ExportCsv()
        {
            var csvData = await _userService.GenerateTaskReportAsync();
            return File(csvData, "text/csv", "Tasks.csv");
        }

    }
}
