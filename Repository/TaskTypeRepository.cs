using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Repository
{
    public interface ITaskTypeRepository {
        Task<TaskType> PostTaskAsync(TaskType tasks);

    }

    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly DataContext _dataContext;
        public TaskTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }

        public async Task<TaskType> PostTaskAsync(TaskType tasks)
        {
            _dataContext.TaskType.Add(tasks);
            await _dataContext.SaveChangesAsync();      
            return tasks;
        }
    }
}
