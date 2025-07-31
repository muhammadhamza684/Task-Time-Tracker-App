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


        [HttpGet("export")]
        [Produces("text/csv")]
        public async Task<FileResult> ExportCsv()
        {
            var csvData = await _timeLogService.GenerateTaskReportAsync();
            return File(csvData, "text/csv", "Tasks.csv");
        }
    }
}
