namespace HotelManagerFull.Share.Response
{
    /// <summary>
    /// IResponseData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResponseData<T>
    {
        T Data { get; set; }
        string ResponseMessage { get; set; }
        int ResponseCode { get; set; }
    }

    /// <summary>
    /// ResponseData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseData<T> : IResponseData<T>
    {
        public T Data { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
}
