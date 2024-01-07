using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// OrderRoomViewModel
    /// </summary>
    public class OrderRoomViewModel : OrderRoom
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IDCard { get; set; }
        public string RoomName { get; set; }
        public string HotelName { get; set; }
        public string ProvincesName { get; set; }
        public string StatusName { get; set; }
    }
}
