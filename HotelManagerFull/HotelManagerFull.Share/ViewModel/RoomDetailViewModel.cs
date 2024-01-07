using HotelManagerFull.Share.Entities;
using System.Collections.Generic;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RoomDetailViewModel
    /// </summary>
    public class RoomDetailViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? Star { get; set; }
        public long? HotelId { get; set; }
        public string HotelName { get; set; }
        public string Introduce { get; set; }
        public long? FloorId { get; set; }
        public string FloorName { get; set; }
        public List<HotelImages> ListHotelImage { get; set; }
    }
}
