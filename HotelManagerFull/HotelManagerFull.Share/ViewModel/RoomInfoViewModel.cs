using System;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RoomInfoViewModel
    /// </summary>
    public class RoomInfoViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public long? PromotionalPrice { get; set; }
        public int? Star { get; set; }
        public bool? IsActive { get; set; }
        public long? RoomStatusId { get; set; }
        public long? RoomCategoriesId { get; set; }
        public long? HotelId { get; set; }
        public long? FloorId { get; set; }
        public long TotalRow { get; set; }
        public string RoomStatusName { get; set; }
        public string RoomCategoriesName { get; set; }
        public string HotelName { get; set; }
        public string FloorName { get; set; }
        public string Image { get; set; }
        public long OrderRoomId { get; set; }
        public DateTime? StartTime { get; set; }
        public string FullName { get; set; }
        public string IDCard { get; set; }
        public string PhoneNumber { get; set; }
    }
}
