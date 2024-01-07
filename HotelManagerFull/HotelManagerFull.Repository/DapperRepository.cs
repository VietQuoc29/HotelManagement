using Dapper;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagerFull.Repository
{
    /// <summary>
    /// IDapperRepository
    /// </summary>
    public interface IDapperRepository
    {
        /// <summary>
        /// QueryMultipleWithParam
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="prm"></param>
        /// <returns></returns>
        IEnumerable<T> QueryMultipleWithParam<T>(string queryString, object prm);

        /// <summary>
        /// QueryMultiple
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        IEnumerable<T> QueryMultiple<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// QueryMultipleUsingStoreProcAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryMultipleUsingStoreProcAsync<T>(string spName, object parms = null, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// GetBySql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> GetBySql<T>(string sql);

        /// <summary>
        /// QueryFirstOrDefault
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// QueryFirstOrDefault
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// QueryFirstOrDefaultUsingStoreProc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        T QueryFirstOrDefaultUsingStoreProc<T>(string spName, object parms);

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// ExecuteScalarUsingStoreProc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        T ExecuteScalarUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// InsertOrUpdateUsingStoreProc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        T InsertOrUpdateUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// UpdateMultiple
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="executeString"></param>
        /// <param name="listDataUpDate"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        Task UpdateMultiple(string connectionString, string executeString, List<dynamic> listDataUpDate, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut);

        /// <summary>
        /// ExecuteToDataTable
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        DataTable ExecuteToDataTable(string queryString, int commandTimeOut = 30);

        /// <summary>
        /// ExecuteToDataTable
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        DataTable ExecuteToDataTable(string connectionString, string queryString, int commandTimeOut = 30);

        /// <summary>
        /// DataTableBulkInsert
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dataTable"></param>
        /// <param name="bulkCopyTimeOut"></param>
        /// <param name="batchSize"></param>
        /// <param name="isKeepIdentity"></param>
        /// <returns></returns>
        Task DataTableBulkInsert(string connectionString, DataTable dataTable,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize,
            bool isKeepIdentity = false);

        /// <summary>
        /// DataReaderBulkInsert
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dataReader"></param>
        /// <param name="bulkCopyTimeOut"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        Task DataReaderBulkInsert(string connectionString,
            IDataReader dataReader,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize);

        /// <summary>
        /// UpdateUsingQuery
        /// </summary>
        /// <param name="executeString"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        Task UpdateUsingQuery(string executeString, object parms);
    }

    /// <summary>
    /// DapperRepository
    /// </summary>
    public class DapperRepository : IDapperRepository
    {
        public static string ConnectionString = Configuration.Instance.GetConfig<string>(Constants.ConnectionStrings, Constants.MainConnectionString);

        #region Command
        #region Query Multiple and exact mapping
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="prm"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryMultipleWithParam<T>(string queryString, object prm)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var multi = connection.QueryMultiple(queryString, prm))
                    {
                        var result = multi.Read<T>();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryMultiple<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var multi = connection.QueryMultiple(queryString, commandTimeout: commandTimeOut))
                    {
                        var result = multi.Read<T>();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryMultipleUsingStoreProcAsync<T>(string spName, object parms = null, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(spName, parms, commandTimeout: commandTimeOut, commandType: CommandType.StoredProcedure))
                    {
                        var result = multi.Read<T>();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> GetBySql<T>(string sql)
        {
            using IDbConnection db = new SqlConnection(ConnectionString);
            return db.Query<T>(sql).ToList();
        }
        #endregion

        #region Query FirstOrDefault
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    return connection.QueryFirstOrDefault<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.QueryFirstOrDefault<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public T QueryFirstOrDefaultUsingStoreProc<T>(string spName, object parms)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.QueryFirstOrDefault<T>(spName, parms, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }
        #endregion

        #region Execute Scalar
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.ExecuteScalar<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string connectionString, string queryString, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    return connection.ExecuteScalar<T>(queryString, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public T ExecuteScalarUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    return connection.ExecuteScalar<T>(spName, parms, commandTimeout: commandTimeOut, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }
        #endregion

        #region Insert/Update
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parms"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public T InsertOrUpdateUsingStoreProc<T>(string spName, object parms, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return connection.Query<T>(spName, parms, commandTimeout: commandTimeOut, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="executeString"></param>
        /// <param name="listDataUpDate"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public async Task UpdateMultiple(string connectionString, string executeString, List<dynamic> listDataUpDate, int commandTimeOut = (int)CommandHelperEnum.CommainTimeOut)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.ExecuteAsync(executeString, listDataUpDate, commandTimeout: commandTimeOut);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }

        }
        #endregion

        #region Execute To DataTable
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public DataTable ExecuteToDataTable(string queryString, int commandTimeOut = 30)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    var reader = connection.ExecuteReader(queryString, commandTimeout: commandTimeOut);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;

                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="queryString"></param>
        /// <param name="commandTimeOut"></param>
        /// <returns></returns>
        public DataTable ExecuteToDataTable(string connectionString, string queryString, int commandTimeOut = 30)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var reader = connection.ExecuteReader(queryString, commandTimeout: commandTimeOut);
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;

                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }
        #endregion
        #endregion

        #region Bulk Copy
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dataTable"></param>
        /// <param name="bulkCopyTimeOut"></param>
        /// <param name="batchSize"></param>
        /// <param name="isKeepIdentity"></param>
        /// <returns></returns>
        public async Task DataTableBulkInsert(string connectionString, DataTable dataTable,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize,
            bool isKeepIdentity = false)
        {
            try
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString, isKeepIdentity == true ? SqlBulkCopyOptions.KeepIdentity : SqlBulkCopyOptions.Default))
                {
                    sqlBulkCopy.DestinationTableName = dataTable.TableName;
                    sqlBulkCopy.BulkCopyTimeout = bulkCopyTimeOut;
                    sqlBulkCopy.BatchSize = batchSize;
                    MapColumns(dataTable, sqlBulkCopy);
                    await sqlBulkCopy.WriteToServerAsync(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dataReader"></param>
        /// <param name="bulkCopyTimeOut"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public async Task DataReaderBulkInsert(string connectionString,
            IDataReader dataReader,
            int bulkCopyTimeOut = (int)SqlBulkCopyEnum.TimeOut,
            int batchSize = (int)SqlBulkCopyEnum.BatchSize)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection))
                    {
                        sqlBulkCopy.DestinationTableName = dataReader.GetSchemaTable().TableName;
                        sqlBulkCopy.BulkCopyTimeout = bulkCopyTimeOut;
                        sqlBulkCopy.BatchSize = batchSize;
                        await sqlBulkCopy.WriteToServerAsync(dataReader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executeString"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task UpdateUsingQuery(string executeString, object parms)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.ExecuteAsync(executeString, parms, commandTimeout: (int)CommandHelperEnum.CommainTimeOut).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw new DbException(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoTable"></param>
        /// <param name="bulkCopy"></param>
        /// <returns></returns>
        private SqlBulkCopy MapColumns(DataTable infoTable, SqlBulkCopy bulkCopy)
        {

            foreach (DataColumn dc in infoTable.Columns)
            {
                bulkCopy.ColumnMappings.Add(dc.ColumnName,
                  dc.ColumnName);
            }

            return bulkCopy;
        }
        #endregion
    }
}
