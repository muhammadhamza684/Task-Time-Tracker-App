using Task___Time_Tracker_App.DTO;
using Task___Time_Tracker_App.Models;
using Task___Time_Tracker_App.Repository;

namespace Task___Time_Tracker_App.Services
{

    public interface IRollService
    {
        Task<Role> CreatRullAsync(RollDto rollDto);
    }
    public class RollService : IRollService
    {
        private readonly IRollRepository _rollRepository;
        public RollService(IRollRepository rollRepository)
        {
            _rollRepository = rollRepository;   
        }
        public async Task<Role> CreatRullAsync(RollDto rollDto)

        {
            var CreateDtoData = new Role
            {
               Id = rollDto.Id, 
               TeamName = rollDto.TeamName,
            };
            var reslt = await _rollRepository.CreatRullAsync(CreateDtoData);
            return reslt;       
        }
    }
}
