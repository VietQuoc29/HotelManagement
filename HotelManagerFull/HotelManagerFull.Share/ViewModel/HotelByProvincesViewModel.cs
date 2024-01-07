using HotelManagerFull.Share.Entities;
using System.Collections.Generic;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// HotelByProvincesViewModel
    /// </summary>
    public class HotelByProvincesViewModel
    {
        public string ProvinceName { get; set; }
        public List<Hotels> ListHotel { get; set; }
    }
}
