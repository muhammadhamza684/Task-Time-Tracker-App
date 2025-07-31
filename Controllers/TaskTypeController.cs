using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task___Time_Tracker_App.DTO;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Services;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskTypeController : Controller
    {
        private readonly ITaskTypeService _taskTypeService;
        public TaskTypeController(ITaskTypeService taskTypeService)
        {
           _taskTypeService = taskTypeService;      
        }

        [HttpPost]

        public async Task<IActionResult> CreateTypeOfTask([FromBody ] TaskType task)
        {
            var result = await _taskTypeService.PostTaskAsync(task);
            return Ok(result);  
        }
    }
}
