namespace BlindBoxSystem.Common.Constants
{
    public static class ProjectConstant
    {
        public static string STAFF = "Staff";
        public static string USER = "User";
        public static string ADMIN = "Admin";
        public static string VnPay = "VnPay";
        public static string COD = "COD";

        public enum OrderStatus
        {
            Pending = 1,
            Processing = 2,
            Shipping = 3,
            Cancelled = 4,
            Arrived = 5
        }
    }
}
