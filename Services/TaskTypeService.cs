using Microsoft.AspNetCore.Http.HttpResults;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Repository;

namespace Task___Time_Tracker_App.Services
{
    public interface ITaskTypeService
    {
        Task<TaskType> PostTaskAsync(TaskType tasks);

        Task<TaskType> UpdateTaskAsync(TaskType tasks, int Id);
    }
    public class TaskTypeService : ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        public TaskTypeService(ITaskTypeRepository taskTypeRepository)
        {
            _taskTypeRepository = taskTypeRepository;   
        }
        public async Task<TaskType> PostTaskAsync(TaskType tasks)
        {
            var restult = await  _taskTypeRepository.PostTaskAsync(tasks);  
            return restult; 
        }

        public async Task<TaskType> UpdateTaskAsync(TaskType tasks, int Id)
        {
            var result = await _taskTypeRepository.UpdateTaskAsync(tasks, Id);
            if (result == null)
            {
                Console.WriteLine("Invalid Id , Please enter Valid Id");

            }
            return result;  
    }
}}


