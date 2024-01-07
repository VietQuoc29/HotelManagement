using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// Provinces
    /// </summary>
    public class Provinces
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
