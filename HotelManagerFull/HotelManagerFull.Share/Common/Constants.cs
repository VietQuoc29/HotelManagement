namespace HotelManagerFull.Share.Common
{
    /// <summary>
    /// Constants
    /// </summary>
    public class Constants
    {
        public static readonly string ConnectionStrings = "ConnectionStrings";
        public static readonly string MainConnectionString = "MainConnectionString";
        public static readonly string Underlined = "_";
        public static string SortDesc = "desc";
        public static string SortId = "Id";
        public static string Page = "@Page";
        public static string PageSize = "@PageSize";
        public static string Key = "@Key";
        public static string Source = "/";

        /// <summary>
        /// Message
        /// </summary>
        public static class Message
        {
            public static readonly string Successfully = "Successfully";
            public static readonly string InternalServer = "Internal Server Error";
            public static readonly string ModelStateMessage = "Model State Invalid";
            public static readonly string IdNotFound = "Không tìm thấy Id";
            public static readonly string RecordNotFoundMessage = "Không tìm thấy bản ghi";
            public static readonly string DeleteSuccess = "Xóa thông tin thành công";
            public static readonly string DeleteFail = "Đã có lỗi xảy ra trong quá trình xoá thông tin. Vui lòng kiểm tra lại";
            public static readonly string SaveSuccess = "Lưu thông tin thành công";
            public static readonly string SaveFail = "Đã có lỗi trong quá trình lưu thông tin. Vui lòng kiểm tra lại";
            public static readonly string UserRoleMessage = "Tài khoản chưa được phân quyền. Vui lòng thử lại";
            public static readonly string UserInActiveMessage = "Tài khoản hoặc mật khẩu không đúng. Vui lòng thử lại";
            public static readonly string RoleMessage = "Bảng phân quyền chưa có dữ liệu. Vui lòng thử lại";
            public static readonly string GenerateDataSuccess = "Tạo dữ liệu chung thành công";
            public static readonly string GenerateDataFail = "Đã xảy ra lỗi trong quá trình tạo dữ liệu chung";
            public static readonly string DuplicateMessage = "Thông tin đã tồn tại. Vui lòng nhập thông tin khác";
            public static readonly string PaymentSuccess = "Thanh toán thành công";
            public static readonly string PaymentFail = "Đã có lỗi trong quá trình thanh toán. Vui lòng kiểm tra lại";
            public static readonly string ActiveMessage = "Bạn đã kích hoạt tài khoản thành công";
            public static readonly string InActiveMessage = "Bạn đã khóa tài khoản thành công";
        }

        /// <summary>
        /// JwtClaimIdentifiers
        /// </summary>
        public static class JwtClaimIdentifiers
        {
            public const string Rol = "rol", Id = "id";
        }

        /// <summary>
        /// JwtClaims
        /// </summary>
        public static class JwtClaims
        {
            public const string ApiAccess = "api_access";
        }

        /// <summary>
        /// FileType
        /// </summary>
        public class FileType
        {
            public const string PROVINCES = "PROVINCES";
            public const string HOTELS = "HOTELS";
            public const string ROOMS = "ROOMS";
            public const string SERVICES = "SERVICES";
        }

        /// <summary>
        /// UploadUrl
        /// </summary>
        public static class UploadUrl
        {
            public const string UPLOAD_PROVINCES = "/UPLOAD_PROVINCES";
            public const string UPLOAD_HOTELS = "/UPLOAD_HOTELS";
            public const string UPLOAD_ROOMS = "/UPLOAD_ROOMS";
            public const string UPLOAD_SERVICES = "/UPLOAD_SERVICES";
        }
    }
}
