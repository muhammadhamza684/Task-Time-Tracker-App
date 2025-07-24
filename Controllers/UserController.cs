using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllUser()
        {
            var AllUsers = await _dataContext.users.ToListAsync();
            return Ok(AllUsers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var User = await _dataContext.users.FirstOrDefaultAsync(x=>x.Id== id); 
            if (User == null) 
                {
            
            return NotFound();  
            }
            return Ok(User);    
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] User users)
        {
            
            _dataContext.users.Add(users);
            await _dataContext.SaveChangesAsync();  
            return CreatedAtAction(nameof (GetUserById), new { id = users.Id }, users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var DeleteUser = await _dataContext.users.FirstOrDefaultAsync(x => x.Id == id);
            if (DeleteUser == null)
                return NotFound("User not found.");
            _dataContext.users.Remove(DeleteUser);
            await _dataContext.SaveChangesAsync();
            return Ok("User Detleted successfully");
        }


    }
}
