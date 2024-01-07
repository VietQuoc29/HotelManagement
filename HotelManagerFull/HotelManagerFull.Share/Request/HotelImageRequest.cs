using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// HotelImageRequest
    /// </summary>
    public class HotelImageRequest
    {
        public long? RoomId { get; set; }
        public List<IFormFile> ListFile { get; set; }
    }
}
