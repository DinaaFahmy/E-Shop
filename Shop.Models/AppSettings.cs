namespace Shop.Models
{
    public class AppSettings
    {
        public string MySQLConnectionString { get; set; }
        public string FrontendOrigin { get; set; }
        public JWTTokenDuration JWTTokenDuration { get; set; }
        public string JWTSecretKey { get; set; }
    }
    public class JWTTokenDuration
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
