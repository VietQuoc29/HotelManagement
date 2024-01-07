using HotelManagerFull.Share.Entities;
using System.Collections.Generic;

namespace HotelManagerFull.Share.Request
{
    /// <summary>
    /// TransactionsRequest
    /// </summary>
    public class TransactionsRequest
    {
        public List<Transactions> ListTransactions { get; set; }
    }
}
