using System;
namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// OrderRoom
    /// </summary>
    public class OrderRoom
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public long? RoomId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long? TotalPayment { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
