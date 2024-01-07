namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RoomClientViewModel
    /// </summary>
    public class RoomClientViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public long? PromotionalPrice { get; set; }
        public int? Star { get; set; }
        public bool IsActive { get; set; }
        public long? RoomStatusId { get; set; }
        public string RoomStatusName { get; set; }
        public long? RoomCategoriesId { get; set; }
        public string RoomCategoriesName { get; set; }
        public long? HotelId { get; set; }
        public string HotelName { get; set; }
        public long? FloorId { get; set; }
        public string FloorName { get; set; }
        public long TotalRow { get; set; }
        public string Image { get; set; }
    }
}
