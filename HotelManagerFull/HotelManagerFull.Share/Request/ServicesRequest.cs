using Microsoft.AspNetCore.Http;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// ServicesRequest
    /// </summary>
    public class ServicesRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public string Unit { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
        public long? ServiceCategoriesId { get; set; }
        public IFormFile File { get; set; }
    }
}
