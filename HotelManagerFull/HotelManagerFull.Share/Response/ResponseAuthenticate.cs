namespace HotelManagerFull.Share.Response
{
    /// <summary>
    /// ResponseAuthenticate
    /// </summary>
    public class ResponseAuthenticate
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public int ExpireTime { get; set; }
    }
}
