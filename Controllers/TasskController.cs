using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasskController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TasskController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllTask(int id)
        {
            var AllTask = await _dataContext.tasks.ToListAsync();
            return Ok(AllTask);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Task = await _dataContext.tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (Task == null)
            {
                return BadRequest();
            }
            return Ok(Task);
        }
        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] Tassk TK)
        {
            TK.AssignedUser = null;
            _dataContext.tasks.Add(TK);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = TK.Id }, TK);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tassk TK)
        {
            var Result = await _dataContext.tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (Result == null)
            {
                return BadRequest("Task Not found");
            }
            //  _schoolContext.Entry(Student).CurrentValues.SetValues(std
            TK.Id = id;
            _dataContext.Entry(Result).CurrentValues.SetValues(TK);
            await _dataContext.SaveChangesAsync();
            return Ok("Task Updated Successfully");
        }


        // Filter or Search by Name 
        [HttpGet]
        public async Task<IActionResult> FilterByName([FromQuery] string name)
        {
       
            var tasks = await _dataContext.tasks
       .Where(t => t.Title.Contains(name))
       .ToListAsync();
            return Ok(tasks);
        }

        //Status Change Update
        [HttpPut]

        public async Task<IActionResult> ChangeStatus(int id, string TK)
        {
            var ChangeResult = await _dataContext.tasks
     .FirstOrDefaultAsync(x=> x.Id == id);
            if (ChangeResult == null) {

                return BadRequest();
            }
            ChangeResult.Status = TK;
            await _dataContext.SaveChangesAsync();
            return Ok("Status Changes SuccessFully");
        }
    }
}
