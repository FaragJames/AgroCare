using AgroCare.Data.DTOs;

namespace AgroCare.ViewModels
{
    public class UserInfoViewModel
    {
        public List<StoreTypeDto> StoreTypes { get; set; }
        public List<EngineerTypeDto> EngineerTypes { get; set; }
        public List<LandTypeDto> LandTypes { get; set; }
        public List<SoilTypeDto> SoilTypes { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }


        public UserInfoViewModel(List<StoreTypeDto> storeTypes, List<EngineerTypeDto> engineerTypes,
            List<LandTypeDto> landTypes, List<SoilTypeDto> soils,
            string role, string userName)
        {
            StoreTypes = storeTypes;
            EngineerTypes = engineerTypes;
            LandTypes = landTypes;
            SoilTypes = soils;
            Role = role;
            UserName = userName;
        }
    }
}
