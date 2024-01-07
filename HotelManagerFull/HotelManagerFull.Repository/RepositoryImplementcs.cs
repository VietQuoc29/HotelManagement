using HotelManagerFull.Share.Entities;

namespace HotelManagerFull.Repository
{
    /// <summary>
    /// ICustomersRepository
    /// </summary>
    public interface ICustomersRepository : IRepository<Customers>
    {
    }

    /// <summary>
    /// CustomersRepository
    /// </summary>
    public class CustomersRepository : Repository<Customers>, ICustomersRepository
    {
        public CustomersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IFloorsRepository
    /// </summary>
    public interface IFloorsRepository : IRepository<Floors>
    {
    }

    /// <summary>
    /// FloorsRepository
    /// </summary>
    public class FloorsRepository : Repository<Floors>, IFloorsRepository
    {
        public FloorsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IHotelImagesRepository
    /// </summary>
    public interface IHotelImagesRepository : IRepository<HotelImages>
    {
    }

    /// <summary>
    /// HotelImagesRepository
    /// </summary>
    public class HotelImagesRepository : Repository<HotelImages>, IHotelImagesRepository
    {
        public HotelImagesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IHotelsRepository
    /// </summary>
    public interface IHotelsRepository : IRepository<Hotels>
    {
    }

    /// <summary>
    /// HotelsRepository
    /// </summary>
    public class HotelsRepository : Repository<Hotels>, IHotelsRepository
    {
        public HotelsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IProvincesRepository
    /// </summary>
    public interface IProvincesRepository : IRepository<Provinces>
    {
    }

    /// <summary>
    /// ProvincesRepository
    /// </summary>
    public class ProvincesRepository : Repository<Provinces>, IProvincesRepository
    {
        public ProvincesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IRolesRepository
    /// </summary>
    public interface IRolesRepository : IRepository<Roles>
    {
    }

    /// <summary>
    /// RolesRepository
    /// </summary>
    public class RolesRepository : Repository<Roles>, IRolesRepository
    {
        public RolesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IRoomCategoriesRepository
    /// </summary>
    public interface IRoomCategoriesRepository : IRepository<RoomCategories>
    {
    }

    /// <summary>
    /// RoomCategoriesRepository
    /// </summary>
    public class RoomCategoriesRepository : Repository<RoomCategories>, IRoomCategoriesRepository
    {
        public RoomCategoriesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IRoomsRepository
    /// </summary>
    public interface IRoomsRepository : IRepository<Rooms>
    {
    }

    /// <summary>
    /// RoomsRepository
    /// </summary>
    public class RoomsRepository : Repository<Rooms>, IRoomsRepository
    {
        public RoomsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IRoomStatusRepository
    /// </summary>
    public interface IRoomStatusRepository : IRepository<RoomStatus>
    {
    }

    /// <summary>
    /// RoomStatusRepository
    /// </summary>
    public class RoomStatusRepository : Repository<RoomStatus>, IRoomStatusRepository
    {
        public RoomStatusRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IServiceCategoriesRepository
    /// </summary>
    public interface IServiceCategoriesRepository : IRepository<ServiceCategories>
    {
    }

    /// <summary>
    /// ServiceCategoriesRepository
    /// </summary>
    public class ServiceCategoriesRepository : Repository<ServiceCategories>, IServiceCategoriesRepository
    {
        public ServiceCategoriesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IServicesRepository
    /// </summary>
    public interface IServicesRepository : IRepository<Services>
    {
    }

    /// <summary>
    /// ServicesRepository
    /// </summary>
    public class ServicesRepository : Repository<Services>, IServicesRepository
    {
        public ServicesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// ISexRepository
    /// </summary>
    public interface ISexRepository : IRepository<Sex>
    {
    }

    /// <summary>
    /// SexRepository
    /// </summary>
    public class SexRepository : Repository<Sex>, ISexRepository
    {
        public SexRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IUserProfilesRepository
    /// </summary>
    public interface IUserProfilesRepository : IRepository<UserProfiles>
    {
    }

    /// <summary>
    /// UserProfilesRepository
    /// </summary>
    public class UserProfilesRepository : Repository<UserProfiles>, IUserProfilesRepository
    {
        public UserProfilesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IOrderRoomRepository
    /// </summary>
    public interface IOrderRoomRepository : IRepository<OrderRoom>
    {
    }

    /// <summary>
    /// OrderRoomRepository
    /// </summary>
    public class OrderRoomRepository : Repository<OrderRoom>, IOrderRoomRepository
    {
        public OrderRoomRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// ITransactionsRepository
    /// </summary>
    public interface ITransactionsRepository : IRepository<Transactions>
    {
    }

    /// <summary>
    /// TransactionsRepository
    /// </summary>
    public class TransactionsRepository : Repository<Transactions>, ITransactionsRepository
    {
        public TransactionsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

    /// <summary>
    /// IRegisterRoomsRepository
    /// </summary>
    public interface IRegisterRoomsRepository : IRepository<RegisterRooms>
    {
    }

    /// <summary>
    /// RegisterRoomsRepository
    /// </summary>
    public class RegisterRoomsRepository : Repository<RegisterRooms>, IRegisterRoomsRepository
    {
        public RegisterRoomsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
