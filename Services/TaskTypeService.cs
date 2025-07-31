using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Repository;

namespace Task___Time_Tracker_App.Services
{
    public interface ITaskTypeService
    {
        Task<TaskType> PostTaskAsync(TaskType tasks);
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
    }
}


