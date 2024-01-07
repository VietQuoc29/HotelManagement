using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// CustomersViewModel
    /// </summary>
    public class CustomersViewModel : Customers
    {
        public long STT { get; set; }
        public long TotalRow { get; set; }
        public string SexName { get; set; }
    }
}
