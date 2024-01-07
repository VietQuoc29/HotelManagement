using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// RoomCategoriesViewModel
    /// </summary>
    public class RoomCategoriesViewModel : RoomCategories
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
    }
}
