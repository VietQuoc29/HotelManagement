using System;

namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// DbException
    /// </summary>
    public class DbException : Exception
    {
        /// <summary>
        /// DbException
        /// </summary>
        public DbException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DbException(string message)
            : base("In Db: " + message)
        {
        }

        /// <summary>
        /// DbException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DbException(string message, Exception innerException)
            : base("In Db: " + message, innerException)
        {
        }

        /// <summary>
        /// DbException
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <param name="innerException"></param>
        public DbException(string sql, object[] parms, Exception innerException)
            : base("In Db: " + string.Format("Sql: {0}  Parms: {1}", (sql ?? "--"),
                    (parms != null ? string.Join(",", Array.ConvertAll<object, string>(parms, o => (o ?? "null").ToString())) : "--")),
            innerException)
        {
        }
    }
}
