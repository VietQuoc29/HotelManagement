using HotelManagerFull.Share.Entities;
using System.Collections.Generic;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RoomsViewModel
    /// </summary>
    public class RoomsViewModel : Rooms
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string RoomStatusName { get; set; }
        public string RoomCategoriesName { get; set; }
        public string HotelName { get; set; }
        public string FloorName { get; set; }
        public List<HotelImages> ListHotelImages { get; set; }
    }
}
