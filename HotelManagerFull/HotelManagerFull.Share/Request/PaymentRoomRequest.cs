using System;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// PaymentRoomRequest
    /// </summary>
    public class PaymentRoomRequest
    {
        public long OrderRoomId { get; set; }
        public DateTime EndTime { get; set; }
        public long TotalPayment { get; set; }
    }
}
