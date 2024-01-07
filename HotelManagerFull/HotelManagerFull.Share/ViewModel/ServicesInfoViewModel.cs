using HotelManagerFull.Share.Entities;
using System.Collections.Generic;

namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// ServicesInfoViewModel
    /// </summary>
    public class ServicesInfoViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Services> ListService { get; set; }
    }
}
