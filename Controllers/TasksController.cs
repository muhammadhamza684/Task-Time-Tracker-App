using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTask(int pageNO = 1, int pageSize = 2)
        {
            var Results = await _taskService.GetAllTaskAsync(pageNO, pageSize);
            return Ok(Results);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var Result = await _taskService.GetTaskByIdAsync(id);
            return Ok(Result);
        }

        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] Tasks tasks)
        {
            await _taskService.PostTaskAsync(tasks);
            return Ok("Task Assign Successfully");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tasks tasks)
        {
            var result = await _taskService.UpdateTaskAsync(id, tasks);

            if (result == null)
                return NotFound("Task not found");

            return Ok("Task Updated Successfully");
        }

        [HttpGet("{TaskByName}")]

        public async Task<IActionResult> FilterTaskByName(string taskName)
        {
            var Result = await _taskService.FilterTaskByNameAsync(taskName);
            return Ok(Result);
        }

        [HttpPut("{TaskStatusChange}")]

        public async Task<IActionResult> TaskStatusChange(int id, string status)
        {
            var Result = await _taskService.ChangeTaskStatus(id, status);
            return Ok(Result);
        }
        [HttpGet("export")]
        [Produces("text/csv")]
        public async Task<FileResult> ExportCsv()
        {
            var csvData = await _taskService.GenerateTaskReportAsync();
            return File(csvData, "text/csv", "Tasks.csv");
        }
    }
    }
