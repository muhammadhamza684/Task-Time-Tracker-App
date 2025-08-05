using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Task___Time_Tracker_App.DAL;
using Task___Time_Tracker_App.DTO;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Repository;

namespace Task___Time_Tracker_App.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUserAsync(int pageNO, int pageSize);
        Task<User> GetByIdAsync(int id);
        Task<User> CreateUserAsync(User user);

        Task<object> LoginUserAsync(LoginDto loginDto );

        Task<User> DeleteUserAsync(int id);
        Task<User> UpdateUserAsync(int id, User user);

        Task<byte[]> GenerateTaskReportAsync();
    }
    public class UserService : IUserService
    {
        private readonly IuserRepository _userRepository;

        private readonly IConfiguration _config;
        public UserService(IuserRepository userRepository, IConfiguration config)
        {
           _userRepository = userRepository;   
            _config = config;
        }

        public async Task<List<User>> GetAllUserAsync(int pageNO, int pageSize)
        {
           var AllUser = await _userRepository.GetAllUserAsync(pageNO, pageSize);   
            return AllUser;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            // var SingleUser = await _userRepository.GetByIdAsync(id);
            //return SingleUser;  

            var SingleUser = await _userRepository.GetByIdAsync(id);    
            return SingleUser;  
        }

        public async Task<User> CreateUserAsync(User user)
        {
           await _userRepository.CreateUserAsync(user);
            return user;    
        }

        // login login  with Jwt
        public async Task<object> LoginUserAsync([FromBody] LoginDto loginDto)
        {
           var user = await _userRepository.LoginUserAsync(loginDto); 
            return user;    


        }



        public async Task<User> DeleteUserAsync(int id)
        {
            var DeleteUser = await _userRepository.DeleteUserAsync(id); 
            return DeleteUser;  
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            var UpdateResult = await _userRepository.UpdateUserAsync(id, user);  
            return UpdateResult;    
        }

        public Task<byte[]> GenerateTaskReportAsync()
        {
            return _userRepository.GenerateTaskReportAsync();
        }
    }
}
