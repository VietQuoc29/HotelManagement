namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// RoomSearchRequest
    /// </summary>
    public class RoomSearchRequest : PagingRequest
    {
        public long HotelId { get; set; }
    }
}
