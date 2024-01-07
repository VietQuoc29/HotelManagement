using System;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// CustomersRequest
    /// </summary>
    public class CustomersRequest
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string IDCard { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Note { get; set; }
        public long? SexId { get; set; }
    }
}
