using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RegisterRoomsViewModel
    /// </summary>
    public class RegisterRoomsViewModel : RegisterRooms
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string RoomName { get; set; }
        public long? PromotionalPrice { get; set; }
        public string StatusName { get; set; }
    }
}
