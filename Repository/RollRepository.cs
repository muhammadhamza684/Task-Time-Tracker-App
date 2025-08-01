using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Repository
{
    public interface IRollRepository
    {
        Task<Role> CreatRullAsync(Role roll);
    }
    public class RollRepository : IRollRepository   
    {
        private readonly DataContext _dataContext;

        public RollRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }
        public async Task<Role> CreatRullAsync(Role roll)
        {
            _dataContext.UserRule.Add(roll);
            await _dataContext.SaveChangesAsync();
            return roll;    

        }
    }
}
