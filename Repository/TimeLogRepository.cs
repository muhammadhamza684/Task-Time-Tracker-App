using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.DTo;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Repository
{
    public interface ITimelogRepository
    {
        Task<List<TimeLog>> GetAllAsync(int pageNO, int pageSize);
        Task<TimeLog> GetByIdAsync(int id);
        Task<TimeLogDto> CreateTimeLogAsync(TimeLogDto timeLog);

        Task<byte[]> GenerateTaskReportAsync();
        // Task<List<TimeLog>> GetAllAsync();
    }
    public class TimeLogRepository  : ITimelogRepository    
    {
        private readonly  DataContext _dataContext;
        public TimeLogRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }

        //public async Task<List<TimeLog>> GetAllAsync()
        //{
        //    var Result = await _dataContext.timeLogs.ToListAsync();
        //    return Result;
        //}

        public async Task<TimeLog> GetByIdAsync(int id)
        {
            var Result = await _dataContext.timeLogs.FirstOrDefaultAsync(x=>x.Id==id);
            return Result;
        }

        //public async Task<TimeLogDto> CreateTimeLogAsync(TimeLog timeLog)
        //{
        //     _dataContext.timeLogs.Add(timeLog);
        //    await _dataContext.SaveChangesAsync();

        //    return new TimeLogDto();    

        //}

        public async Task<List<TimeLog>> GetAllAsync(int pageNO, int pageSize)
        {
            var Result = await _dataContext.timeLogs
                .Skip((pageNO - 1) * pageSize).Take(pageSize).ToListAsync();
            
            return Result;  
        }

        public async Task<TimeLogDto> CreateTimeLogAsync(TimeLogDto timeLogDto)
        {
            var timeLog = new TimeLog
            {
               // Id = timeLogDto.Id, // Optional: EF will auto-generate if Identity column
                TaskId = timeLogDto.TaskId,
                UserId = timeLogDto.UserId,
                HoursSpent = timeLogDto.HoursSpent,
               LogDate = timeLogDto.LogDate
                // Add any other properties
            };

            _dataContext.timeLogs.Add(timeLog);
            await _dataContext.SaveChangesAsync();

            // Optionally map back to DTO with generated ID
          //  timeLogDto.Id = timeLog.Id;

            return timeLogDto;
        }

        public async Task<byte[]> GenerateTaskReportAsync()
        {
            var logs = await _dataContext.timeLogs.ToListAsync();

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(logs);
            streamWriter.Flush();

            return memoryStream.ToArray();
        }

    }
    }
