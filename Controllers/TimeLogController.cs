using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.DTo;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Services;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly ITimeLogService  _timeLogService;
        public TimeLogController(ITimeLogService timeLogService)
        {
            _timeLogService = timeLogService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllTimeLog()
        //{
        //    var GetAll = await _dataContext.timeLogs.ToListAsync();
        //    return Ok(GetAll);
        //}
        //[HttpGet("{id}")]

        //public async Task<IActionResult> GetTimeLogById(int id)
        //{
        //    var GetByid = await _dataContext.timeLogs.FirstOrDefaultAsync(x=>x.Id==id);
        //    if (GetByid == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(GetByid);

        //}

        //[HttpPost]
        //public  async Task<IActionResult> CreateTimeLog([FromBody] TimeLogDto timeDto)
        //{
        //    var timeLog = new TimeLog {

        //        TaskId = timeDto.TaskId,
        //        UserId = timeDto.UserId,
        //        HoursSpent = timeDto.HoursSpent,
        //        LogDate = DateTime.Now

        //    };

        //    _dataContext.timeLogs.Add(timeLog);
        //    await _dataContext.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetAllTimeLog), new { id = timeLog.Id }, timeLog);
        //}


      


        [HttpGet]
        public async Task<IActionResult> GetAllTimeLog(int pageNO = 1, int pageSize = 2)
        {
            var Result = await _timeLogService.GetAllAsync(pageNO, pageSize);   
            return Ok(Result);  
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var Result = await _timeLogService.GetByIdAsync(id);
            return Ok(Result);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeLog([FromBody] TimeLogDto timeLog)
        {
            var result = await _timeLogService.CreateTimeLogAsync(timeLog);
            return Ok(result);
        }


        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[Produces("text/csv")]
        //public async Task<FileResult> Get()
        //{

        //    var data = await _timeLogService.timeLogs.ToListAsync();

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var streamWriter = new StreamWriter(memoryStream))
        //        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        //        {
        //            csvWriter.WriteRecords(data);
        //        }

        //        return File(memoryStream.ToArray(), "text/csv", $"Export-{DateTime.Now.ToString("s")}.csv");
        //    }
        //}

        [HttpGet("export")]
        [Produces("text/csv")]
        public async Task<FileResult> ExportCsv()
        {
            var csvData = await _timeLogService.GenerateTaskReportAsync();
            return File(csvData, "text/csv", "Tasks.csv");
        }
    }
}
