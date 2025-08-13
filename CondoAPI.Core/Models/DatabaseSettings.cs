namespace CondoAPI.Core.Models
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseProvider { get; set; } = "SqlServer";
    }
}