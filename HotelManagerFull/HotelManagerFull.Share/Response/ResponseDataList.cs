using System.Collections.Generic;

namespace HotelManagerFull.Share.Response
{
    /// <summary>
    /// IResponseDataList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseDataList<T>
    {
        IEnumerable<T> Data { get; set; }
        int ResponseCode { get; set; }
        string ResponseMessage { get; set; }
        long TotalRecords { get; set; }
    }

    /// <summary>
    /// ResponseDataList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDataList<T> : IResponseDataList<T>
    {
        public ResponseDataList() { }

        /// <summary>
        /// ResponseDataList
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        public ResponseDataList(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalRecords = totalCount;
        }

        public IEnumerable<T> Data { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public long TotalRecords { get; set; }
    }
}
