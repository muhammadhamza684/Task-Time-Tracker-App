using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Repository
{
    public interface ITaskTypeRepository {


        Task<TaskType> PostTaskAsync(TaskType tasks);
        Task<TaskType> UpdateTaskAsync(TaskType task,int id);

    }

    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly DataContext _dataContext;
        public TaskTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }

        public async Task<TaskType> PostTaskAsync(TaskType tasks )
        {
            _dataContext.TaskType.Add(tasks);
            await _dataContext.SaveChangesAsync();      
            return tasks;
        }

        public async Task<TaskType> UpdateTaskAsync(TaskType task,  int Id)
        {
            var result = await _dataContext.TaskType.FirstOrDefaultAsync(x => x.Id == Id);
            
            _dataContext.Entry(result).CurrentValues.SetValues(task);
            await _dataContext.SaveChangesAsync();
            return result;

        }
    }
}
