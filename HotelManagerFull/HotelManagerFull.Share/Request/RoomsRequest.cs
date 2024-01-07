using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// RoomsRequest
    /// </summary>
    public class RoomsRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public long? PromotionalPrice { get; set; }
        public int? Star { get; set; }
        public bool IsActive { get; set; }
        public long? RoomStatusId { get; set; }
        public long? RoomCategoriesId { get; set; }
        public long? HotelId { get; set; }
        public long? FloorId { get; set; }
        public List<IFormFile> ListFile { get; set; }
    }
}
