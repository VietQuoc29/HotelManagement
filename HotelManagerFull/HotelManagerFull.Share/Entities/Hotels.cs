using System;

namespace HotelManagerFull.Share.Entities
{
    /// <summary>
    /// Hotels
    /// </summary>
    public class Hotels
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Introduce { get; set; }
        public int? Star { get; set; }
        public string Note { get; set; }
        public long? ProvinceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
