using Microsoft.AspNetCore.Http;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// ProvincesRequest
    /// </summary>
    public class ProvincesRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
