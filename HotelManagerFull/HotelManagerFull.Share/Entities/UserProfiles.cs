using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// UserProfiles
    /// </summary>
    public class UserProfiles
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PassHash { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Zalo { get; set; }
        public bool Active { get; set; }
        public long? SexId { get; set; }
        public long? RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
