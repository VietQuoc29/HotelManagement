using Microsoft.AspNetCore.Http;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// HotelsRequest
    /// </summary>
    public class HotelsRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Introduce { get; set; }
        public int? Star { get; set; }
        public string Note { get; set; }
        public long? ProvinceId { get; set; }
        public IFormFile File { get; set; }
    }
}
