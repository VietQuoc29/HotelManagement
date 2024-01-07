using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// Customers
    /// </summary>
    public class Customers
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string IDCard { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Note { get; set; }
        public long? SexId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
