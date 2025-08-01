using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.Repository
{

    public interface IuserRepository
    {
        Task<List<User>> GetAllUserAsync(int pageNO, int pageSize);
        Task<User> GetByIdAsync(int id);
        Task<User> CreateUserAsync(User user);

        Task<User> DeleteUserAsync(int id);
        Task<User> UpdateUserAsync(int id, User user);

        // Task<byte[]> GenerateTaskReportAsync();

        Task<byte[]> GenerateTaskReportAsync();

    }
    public class UserRepository : IuserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Get All User
        public async Task<List<User>> GetAllUserAsync(int pageNO, int pageSize)
        {
            var AllUsers = await _dataContext.users.Skip((pageNO - 1) * pageSize).Take(pageSize).ToListAsync();
            return AllUsers;    
        }

        // Get Single user BY using Id
        public async Task<User> GetByIdAsync(int id)
        {
            var SingleUser =  await _dataContext.users.FirstOrDefaultAsync(x => x.Id == id);
            
            return SingleUser;
        }

        // Create New User
        public async Task<User> CreateUserAsync(User user)
        {
            _dataContext.users.Add(user);   
           await _dataContext.SaveChangesAsync();   
            return user;    
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var SigleUserDelte = await _dataContext.users.FirstOrDefaultAsync(x => x.Id == id);
             _dataContext.users.Remove(SigleUserDelte);
            await _dataContext.SaveChangesAsync();
            return SigleUserDelte;
        }

        public async Task<User> UpdateUserAsync(int id,User user)
        {
            var SingleUserUpdate = await _dataContext.users.FirstOrDefaultAsync(x => x.Id == id);

            if (SingleUserUpdate == null)
                return null;

           // tasks.Id = Result.Id;

            _dataContext.Entry(SingleUserUpdate).CurrentValues.SetValues(user);
            await _dataContext.SaveChangesAsync();
            return SingleUserUpdate;
        }
        [HttpGet("File Export")]
        // public async Task<byte[]> GenerateTaskReportAsync()
        public async Task<byte[]> GenerateTaskReportAsync()
        {
            var logs = await _dataContext.users.ToListAsync();

            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecords(logs);
            streamWriter.Flush();

            return memoryStream.ToArray();
        }
    }
}
