using System;
using System.Collections.Generic;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// ReturnRoomViewModel
    /// </summary>
    public class ReturnRoomViewModel
    {
        public long Id { get; set; }
        public string ProvinceName { get; set; }
        public string HotelName { get; set; }
        public string Name { get; set; }
        public string FloorName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IDCard { get; set; }
        public long? Price { get; set; }
        public long? PromotionalPrice { get; set; }
        public long OrderRoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long TotalHour { get; set; }
        public long TotalMinutes { get; set; }
        public string CreatedBy { get; set; }
        public bool Status { get; set; }
        public decimal TotalPaymentTemp { get; set; }
        public List<TransactionsViewModel> ListTransactions { get; set; }
    }
}
