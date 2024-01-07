namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// RoomByHotelRequest
    /// </summary>
    public class RoomByHotelRequest : PagingRequest
    {
        public long HotelId { get; set; }
        public long? RoomStatusId { get; set; }
        public long? RoomCategoriesId { get; set; }
        public int? Star { get; set; }
    }
}
