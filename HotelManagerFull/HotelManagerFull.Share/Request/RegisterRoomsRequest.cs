using System;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// RegisterRoomsRequest
    /// </summary>
    public class RegisterRoomsRequest
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? RoomId { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }
        public string Note { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
