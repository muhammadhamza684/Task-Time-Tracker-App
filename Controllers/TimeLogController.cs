using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.DTo;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TimeLogController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimeLog()
        {
            var GetAll = await _dataContext.timeLogs.ToListAsync();
            return Ok(GetAll);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetTimeLogById(int id)
        {
            var GetByid = await _dataContext.timeLogs.FirstOrDefaultAsync(x=>x.Id==id);
            if (GetByid == null)
            {
                return BadRequest();
            }
            return Ok(GetByid);
            
        }

        [HttpPost]
        public  async Task<IActionResult> CreateTimeLog([FromBody] TimeLogDto timeDto)
        {
            var timeLog = new TimeLog {

                TaskId = timeDto.TaskId,
                UserId = timeDto.UserId,
                HoursSpent = timeDto.HoursSpent,
                LogDate = DateTime.Now

            };




            _dataContext.timeLogs.Add(timeLog);
            await _dataContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllTimeLog), new { id = timeLog.Id }, timeLog);
        }
    }
}
