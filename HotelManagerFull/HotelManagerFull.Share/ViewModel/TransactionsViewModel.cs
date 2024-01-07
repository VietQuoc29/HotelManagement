namespace HotelManagerFull.Share.ViewModel
{
    /// <summary>
    /// TransactionsViewModel
    /// </summary>
    public class TransactionsViewModel
    {
        public string ServiceName { get; set; }
        public long? Price { get; set; }
        public string Quantity { get; set; }
        public decimal IntoMoney { get; set; }
        public string Unit { get; set; }
    }
}
