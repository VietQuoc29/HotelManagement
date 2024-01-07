using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// ServiceCategoriesViewModel
    /// </summary>
    public class ServiceCategoriesViewModel : ServiceCategories
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
    }
}
