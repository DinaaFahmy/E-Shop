namespace Shop.ViewModels.Response
{
    public class LoginResponse
    {
        public ProfileResponse User { get; set; }
        public List<string> UserRoles { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
