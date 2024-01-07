using Dapper;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Connection;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HotelManagerFull.Repository
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Connection
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// DbContext
        /// </summary>
        DatabaseContext DbContext { get; }

        IDbTransaction Transaction { get; }

        /// <summary>
        /// ConnectionString
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// OpenTransaction
        /// </summary>
        /// <returns></returns>
        Task<IDbTransaction> OpenTransaction();

        /// <summary>
        /// OpenTransaction
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<IDbTransaction> OpenTransaction(IsolationLevel level);

        /// <summary>
        /// CommitTransaction
        /// </summary>
        /// <param name="disposeTrans"></param>
        void CommitTransaction(bool disposeTrans = true);

        /// <summary>
        /// RollbackTransaction
        /// </summary>
        /// <param name="disposeTrans"></param>
        void RollbackTransaction(bool disposeTrans = true);

        /// <summary>
        /// BeginTransaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Commit
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback
        /// </summary>
        void Rollback();

        /// <summary>
        /// SaveChangesAsync
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        ICustomersRepository CustomersRepository { get; }
        IFloorsRepository FloorsRepository { get; }
        IHotelImagesRepository HotelImagesRepository { get; }
        IHotelsRepository HotelsRepository { get; }
        IProvincesRepository ProvincesRepository { get; }
        IRolesRepository RolesRepository { get; }
        IRoomCategoriesRepository RoomCategoriesRepository { get; }
        IRoomsRepository RoomsRepository { get; }
        IRoomStatusRepository RoomStatusRepository { get; }
        IServiceCategoriesRepository ServiceCategoriesRepository { get; }
        IServicesRepository ServicesRepository { get; }
        ISexRepository SexRepository { get; }
        IUserProfilesRepository UserProfilesRepository { get; }
        IOrderRoomRepository OrderRoomRepository { get; }
        ITransactionsRepository TransactionsRepository { get; }
        IRegisterRoomsRepository RegisterRoomsRepository { get; }
    }

    /// <summary>
    /// UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Dapper Transaction

        protected readonly IConfiguration Config;
        protected string CnStr = Configuration.Instance.GetConfig<string>(Constants.ConnectionStrings, Constants.MainConnectionString);

        public string ConnectionString
        {
            get => CnStr;
        }
        protected IDbConnection Cn = null;
        public IDbConnection Connection
        {
            get => Cn;
        }

        protected DatabaseContext Context = null;
        public DatabaseContext DbContext
        {
            get => Context;
        }
        protected IDbTransaction Trans = null;
        public IDbTransaction Transaction
        {
            get => Trans;
        }

        /// <summary>
        /// UnitOfWork
        /// </summary>
        /// <param name="config"></param>
        /// <param name="context"></param>
        public UnitOfWork(IConfiguration config, DatabaseContext context)
        {
            Config = config;
            CnStr = Config.GetConnectionString(ConnectionHelper.DatabaseName.MainConnectionString.ToString());
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            Cn = new SqlConnection(CnStr);
            Context = context;
        }

        /// <summary>
        /// Open a transaction
        /// </summary>
        public async Task<IDbTransaction> OpenTransaction()
        {
            if (Trans != null)
                throw new Exception("A transaction is already open, you need to use a new DbContext for parallel job.");

            if (Cn.State == ConnectionState.Closed)
            {
                if (!(Cn is DbConnection))
                    throw new Exception("Connection object does not support OpenAsync.");

                await (Cn as DbConnection)?.OpenAsync();
            }

            Trans = Cn.BeginTransaction();

            return Trans;
        }

        /// <summary>
        /// OpenTransaction
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<IDbTransaction> OpenTransaction(IsolationLevel level)
        {
            if (Trans != null)
                throw new Exception("A transaction is already open, you need to use a new DbContext for parallel job.");

            if (Cn.State == ConnectionState.Closed)
            {
                if (!(Cn is DbConnection))
                    throw new Exception("Connection object does not support OpenAsync.");

                await (Cn as DbConnection).OpenAsync();
            }

            Trans = Cn.BeginTransaction(level);

            return Trans;
        }

        /// <summary>
        /// CommitTransaction
        /// </summary>
        /// <param name="disposeTrans"></param>
        public void CommitTransaction(bool disposeTrans = true)
        {
            if (Trans == null)
                throw new Exception("DB Transaction is not present.");

            Trans.Commit();
            if (disposeTrans) Trans.Dispose();
            if (disposeTrans) Trans = null;
        }

        /// <summary>
        /// RollbackTransaction
        /// </summary>
        /// <param name="disposeTrans"></param>
        public void RollbackTransaction(bool disposeTrans = true)
        {
            if (Trans == null)
                throw new Exception("DB Transaction is not present.");

            Trans.Rollback();
            if (disposeTrans) Trans.Dispose();
            if (disposeTrans) Trans = null;
        }

        /// <summary>
        /// Will be call at the end of the service (ex : transient service in api net core)
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
            Trans?.Dispose();
            Cn?.Close();
            Cn?.Dispose();
        }

        #endregion

        #region Transaction

        public virtual void BeginTransaction() => Context.Database.BeginTransaction();

        public virtual void Commit() => Context.Database.CommitTransaction();

        public virtual void Rollback() => Context.Database.RollbackTransaction();

        public virtual int SaveChanges() => Context.SaveChanges();
        public virtual Task<int> SaveChangesAsync() => Context.SaveChangesAsync();
        #endregion

        private ICustomersRepository _CustomersRepository;
        public ICustomersRepository CustomersRepository
        {
            get
            {
                return _CustomersRepository ??= new CustomersRepository(this);
            }
        }

        private IFloorsRepository _FloorsRepository;
        public IFloorsRepository FloorsRepository
        {
            get
            {
                return _FloorsRepository ??= new FloorsRepository(this);
            }
        }

        private IHotelImagesRepository _HotelImagesRepository;
        public IHotelImagesRepository HotelImagesRepository
        {
            get
            {
                return _HotelImagesRepository ??= new HotelImagesRepository(this);
            }
        }

        private IHotelsRepository _HotelsRepository;
        public IHotelsRepository HotelsRepository
        {
            get
            {
                return _HotelsRepository ??= new HotelsRepository(this);
            }
        }

        private IProvincesRepository _ProvincesRepository;
        public IProvincesRepository ProvincesRepository
        {
            get
            {
                return _ProvincesRepository ??= new ProvincesRepository(this);
            }
        }

        private IRolesRepository _RolesRepository;
        public IRolesRepository RolesRepository
        {
            get
            {
                return _RolesRepository ??= new RolesRepository(this);
            }
        }

        private IRoomCategoriesRepository _RoomCategoriesRepository;
        public IRoomCategoriesRepository RoomCategoriesRepository
        {
            get
            {
                return _RoomCategoriesRepository ??= new RoomCategoriesRepository(this);
            }
        }

        private IRoomsRepository _RoomsRepository;
        public IRoomsRepository RoomsRepository
        {
            get
            {
                return _RoomsRepository ??= new RoomsRepository(this);
            }
        }

        private IRoomStatusRepository _RoomStatusRepository;
        public IRoomStatusRepository RoomStatusRepository
        {
            get
            {
                return _RoomStatusRepository ??= new RoomStatusRepository(this);
            }
        }

        private IServiceCategoriesRepository _ServiceCategoriesRepository;
        public IServiceCategoriesRepository ServiceCategoriesRepository
        {
            get
            {
                return _ServiceCategoriesRepository ??= new ServiceCategoriesRepository(this);
            }
        }

        private IServicesRepository _ServicesRepository;
        public IServicesRepository ServicesRepository
        {
            get
            {
                return _ServicesRepository ??= new ServicesRepository(this);
            }
        }

        private ISexRepository _SexRepository;
        public ISexRepository SexRepository
        {
            get
            {
                return _SexRepository ??= new SexRepository(this);
            }
        }

        private IUserProfilesRepository _UserProfilesRepository;
        public IUserProfilesRepository UserProfilesRepository
        {
            get
            {
                return _UserProfilesRepository ??= new UserProfilesRepository(this);
            }
        }

        private IOrderRoomRepository _OrderRoomRepository;
        public IOrderRoomRepository OrderRoomRepository
        {
            get
            {
                return _OrderRoomRepository ??= new OrderRoomRepository(this);
            }
        }

        private ITransactionsRepository _TransactionsRepository;
        public ITransactionsRepository TransactionsRepository
        {
            get
            {
                return _TransactionsRepository ??= new TransactionsRepository(this);
            }
        }

        private IRegisterRoomsRepository _RegisterRoomsRepository;
        public IRegisterRoomsRepository RegisterRoomsRepository
        {
            get
            {
                return _RegisterRoomsRepository ??= new RegisterRoomsRepository(this);
            }
        }
    }
}
