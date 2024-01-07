using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// RoomCategories
    /// </summary>
    public class RoomCategories
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
