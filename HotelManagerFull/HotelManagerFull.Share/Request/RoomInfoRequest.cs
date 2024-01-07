namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// RoomInfoRequest
    /// </summary>
    public class RoomInfoRequest : PagingRequest
    {
        public long HotelId { get; set; }
        public long? RoomStatusId { get; set; }
    }
}