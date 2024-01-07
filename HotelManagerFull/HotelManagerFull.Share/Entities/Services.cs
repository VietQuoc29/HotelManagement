using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// Services
    /// </summary>
    public class Services
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public string Unit { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
        public long? ServiceCategoriesId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}