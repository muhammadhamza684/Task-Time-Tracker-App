using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.DTO;
using Task___Time_Tracker_App.Models;


namespace Task___Time_Tracker_App.Repository
{
    public interface ITasksRepositry
    {
        Task<List<TaskDTO>> GetAllTaskAsync(int pageNO, int pageSize);
        Task<Tasks> GetTaskByIdAsync(int id);
        Task<Tasks> PostTaskAsync(Tasks tasks);
        Task<Tasks> UpdateTaskAsync(int id, Tasks tasks);
        Task<Tasks> FilterTaskByNameAsync(string taskName);
        Task<Tasks> ChangeTaskStatus(int id, string status);

        // Fix return type to match implementation
        Task<byte[]> GenerateTaskReportAsync();
       
    }
    public class TasksRepository : ITasksRepositry
    {
        private readonly DataContext _dataContext;
        public TasksRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        

        public async Task<List<TaskDTO>> GetAllTaskAsync(int pageNO, int pageSize)
        {
            var result = await _dataContext.tasks
                .Include(x => x.AssignedUser)
                .Include(x => x.TaskType)
                .Include(x=>x.UserRule)
                .Skip((pageNO - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TaskDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Status = x.Status,
                    CreatedDate = x.CreatedDate,
                    AssignedUser = x.AssignedUser.Name,
                    taskType = x.TaskType.Type,
                    // UserRollId = x.UserRollId,

                    // UserRollId = x.UserRollId,
                    UserRollId = x.UserRollId,
                    UserRule = x.UserRule.TeamName
                })
                .ToListAsync();

            return result;
        }

        //

        public async Task<Tasks> GetTaskByIdAsync(int id)
        {
            var Result = await _dataContext.tasks.FirstOrDefaultAsync(x => x.Id == id);
            return Result;
        }

        public async Task<Tasks> PostTaskAsync(Tasks tasks)
        {
            
                _dataContext.tasks.Add(tasks);
                await _dataContext.SaveChangesAsync();

            return tasks;
        }
        //public async Task<TaskDTO> PostTaskAsync(TaskDTO tasks)


        //{
        //    var taskEntity = new Tasks
        //    {
        //        Title = tasks.Title,
        //        Description = tasks.Description,
        //        Status = tasks.Status,
        //        AssignedUserId = tasks.AssignedUserId,
        //        TaskTypeId = tasks.TaskTypeId,
        //        CreatedDate = DateTime.Now
        //    };
        //    _dataContext.tasks.Add(taskEntity);
        //    await _dataContext.SaveChangesAsync();

        //    return tasks;
        //}
        public async Task<Tasks> FilterTaskByNameAsync(string taskName)
        {

            var task = await _dataContext.tasks
                    .Where(x => x.Title.Contains(taskName)).FirstOrDefaultAsync(x => x.Title == taskName);
            return task;

        }

        public async Task<Tasks> ChangeTaskStatus(int id, string status)
        {
            var ChangeResult = await _dataContext.tasks
              .FirstOrDefaultAsync(x => x.Id == id);

            ChangeResult.Status = status;
            await _dataContext.SaveChangesAsync();
            return ChangeResult;
        }

        public async Task<Tasks> UpdateTaskAsync(int id, Tasks tasks)
        {
            var Result = await _dataContext.tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (Result == null)
                return null;
            tasks.Id = Result.Id;

            _dataContext.Entry(Result).CurrentValues.SetValues(tasks);
            await _dataContext.SaveChangesAsync();

            return Result;
        }
        public async Task<byte[]> GenerateTaskReportAsync()
        {
            var logs = await GenerateTaskReportAsync();

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(logs); 
            streamWriter.Flush();

            return memoryStream.ToArray();
        }

      

       
    }
    }



