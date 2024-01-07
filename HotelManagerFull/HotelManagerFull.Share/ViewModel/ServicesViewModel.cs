using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// ServicesViewModel
    /// </summary>
    public class ServicesViewModel : Services
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string ServiceCategoriesName { get; set; }
    }
}
