using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// HotelsViewModel
    /// </summary>
    public class HotelsViewModel : Hotels
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string ProvinceName { get; set; }
    }
}
