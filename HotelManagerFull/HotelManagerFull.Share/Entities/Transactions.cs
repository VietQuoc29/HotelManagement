using System;
namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// Transactions
    /// </summary>
    public class Transactions
    {
        public long Id { get; set; }
        public long? OrderRoomId { get; set; }
        public long? ServiceId { get; set; }
        public long? Quantity { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
