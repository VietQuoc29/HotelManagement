using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// UserProfilesViewModel
    /// </summary>
    public class UserProfilesViewModel : UserProfiles
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string SexName { get; set; }
        public string RoleName { get; set; }
    }
}
