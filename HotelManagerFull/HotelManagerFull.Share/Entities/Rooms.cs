using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// Rooms
    /// </summary>
    public class Rooms
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public long? PromotionalPrice { get; set; }
        public int? Star { get; set; }
        public bool IsActive { get; set; }
        public long? RoomStatusId { get; set; }
        public long? RoomCategoriesId { get; set; }
        public long? HotelId { get; set; }
        public long? FloorId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
