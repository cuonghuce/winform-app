namespace ShopSimpleClassic.Library
{
    public class Temp
    {
        private static string _username;
        private static bool isAdmin;

        public static string Username { get => _username; set => _username = value; }
        public static bool IsAdmin { get => isAdmin; set => isAdmin = value; }
    }
}