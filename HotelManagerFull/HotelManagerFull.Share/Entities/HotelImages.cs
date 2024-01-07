namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// HotelImages
    /// </summary>
    public class HotelImages
    {
        public long Id { get; set; }
        public string ImageLink { get; set; }
        public long? RoomId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
