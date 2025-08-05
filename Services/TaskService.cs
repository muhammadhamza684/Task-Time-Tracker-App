using CsvHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Task___Time_Tracker_App.DTO;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task___Time_Tracker_App.Services
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllTaskAsync(int pageNO, int pageSize);
        Task<Models.Tasks> GetTaskByIdAsync(int id);

        Task<Tasks> PostTaskAsync(CreateTaskDto dto);

        Task<Tasks> UpdateTaskAsync(int id,  UpdateTaskDto dto);

        Task<Tasks> FilterTaskByNameAsync(string taskName);

        Task<Tasks> ChangeTaskStatus(int id, string status);

        Task<byte[]> GenerateTaskReportAsync();
        //Task<byte[]> ExportCsvAsync();
    }
    public class TaskService : ITaskService
    {
        private readonly ITasksRepositry _tasksRepositry;
        public TaskService(ITasksRepositry tasksRepositry)

        {
            _tasksRepositry = tasksRepositry;
        }


        public async Task<List<TaskDTO>> GetAllTaskAsync(int pageNO, int pageSize)
        {
            var Results = await _tasksRepositry.GetAllTaskAsync(pageNO, pageSize);
            return Results;
        }

        public async Task<Tasks> GetTaskByIdAsync(int id)
        {
            var Result = await _tasksRepositry.GetTaskByIdAsync(id);
            return Result;
        }

        public async Task<Tasks> PostTaskAsync(CreateTaskDto dto)
        {
            var CreateDtoData = new Tasks
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
                AssignedUserId = dto.AssignedUserId,
                TaskTypeId = dto.TaskTypeId,
                UserRollId = dto.UserRollId,
               // UserRuleId = dto.UserRuleId
                
            };

            var Result = await _tasksRepositry.PostTaskAsync(CreateDtoData);
            return Result;
        }
        public async Task<Tasks> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var UpdateDataDto = new Tasks
            {
                Title = dto.Title,
                Description= dto.Description,
                Status = dto.Status,    
                CreatedDate = dto.CreatedDate,  
                AssignedUserId = dto.AssignedUserId,    
                TaskTypeId = dto.TaskTypeId,    
            //  UserRollId = dto.UserRollId,    
             //   UserRuleId = dto.UserRuleId 
            
            };
            var Result = await _tasksRepositry.UpdateTaskAsync(id, UpdateDataDto);
            if (Result == null)
            {
                return null;
            }

            return Result;
        }
        public async Task<Tasks> FilterTaskByNameAsync(string taskName)
        {
            var Result = await _tasksRepositry.FilterTaskByNameAsync(taskName);
            return Result;
        }

        public async Task<Tasks> ChangeTaskStatus(int id, string status)
        {
            var Result = await _tasksRepositry.ChangeTaskStatus(id, status);
            return Result;
        }

        public Task<byte[]> GenerateTaskReportAsync()
        {
            return _tasksRepositry.GenerateTaskReportAsync();
        }

    }
}
