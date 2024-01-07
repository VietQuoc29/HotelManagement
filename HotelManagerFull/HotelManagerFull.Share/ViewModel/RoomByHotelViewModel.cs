using HotelManagerFull.Share.Entities;
using System.Collections.Generic;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RoomByHotelViewModel
    /// </summary>
    public class RoomByHotelViewModel
    {
        public string HotelName { get; set; }
        public List<RoomClientViewModel> ListRoom { get; set; }
        public List<RoomCategories> ListRoomCategories { get; set; }
        public List<RoomStatus> ListRoomStatus { get; set; }
        public long TotalRecords { get; set; }
    }
}
