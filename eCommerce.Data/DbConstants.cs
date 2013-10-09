
namespace eCommerce.Data
{
    public static class DbConstants
    {
        public static readonly string QueryTableNames = "SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'";
    }
}
