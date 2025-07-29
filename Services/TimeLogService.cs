using Task___Time_Tracker_App.DTo;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Repository;

namespace Task___Time_Tracker_App.Services
{
    public interface ITimeLogService
    {
        Task<List<TimeLog>> GetAllAsync(int pageNO, int pageSize);
        Task<TimeLog> GetByIdAsync(int id);
        Task<TimeLogDto> CreateTimeLogAsync(TimeLogDto timeLog);

        Task<byte[]> GenerateTaskReportAsync();
        //  Task CreateTimeLogAsync(TimeLog timeLog);
        //Task CreateTimeLogAsync(TimeLogDto timeLogDto);
        //Task GetAllAsync();
    }
    public class TimeLogService : ITimeLogService   
    {
        private readonly ITimelogRepository _timelogRepository; 
        public TimeLogService(ITimelogRepository timelogRepository)
        {
            _timelogRepository = timelogRepository; 
        }

        public async Task<List<TimeLog>> GetAllAsync(int pageNO, int pageSize)
        {
            var TimeLogResult = await _timelogRepository.GetAllAsync( pageNO,  pageSize);
            return TimeLogResult;   
        }

        public async Task<TimeLog> GetByIdAsync(int id)
        {
            var TimeLogResult = await _timelogRepository.GetByIdAsync(id);
            return TimeLogResult;   
        }

        public async Task<TimeLogDto> CreateTimeLogAsync(TimeLogDto timeLog)
        {
            var TimelogResult = await _timelogRepository.CreateTimeLogAsync(timeLog);
            return TimelogResult;
        }

        public Task<byte[]> GenerateTaskReportAsync()
        {
            return _timelogRepository.GenerateTaskReportAsync();
        }
    }
}
