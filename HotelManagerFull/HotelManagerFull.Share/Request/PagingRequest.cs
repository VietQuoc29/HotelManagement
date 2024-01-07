using HotelManagerFull.Share.Common;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// PagingRequest
    /// </summary>
    public class PagingRequest
    {
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string SortField { get; set; } = Constants.SortId;
        public string SortDir { get; set; } = Constants.SortDesc;
        public string SearchText { get; set; } = string.Empty;
    }
}
