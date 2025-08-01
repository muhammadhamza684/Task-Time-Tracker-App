using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task___Time_Tracker_App.DTO;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Services;

namespace Task___Time_Tracker_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollController : ControllerBase
    {
        private readonly IRollService _rollService;
        public RollController(IRollService rollService)
        {
            _rollService = rollService; 
        }
        [HttpPost]
        public async Task<IActionResult> CreateRule([FromBody] RollDto rollDto)
        {
            var result = await _rollService.CreatRullAsync(rollDto);
            return Ok("Rule Created Successfully");  
        }
    }
}
