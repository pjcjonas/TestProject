namespace api_gaming_global.Models.Request
{
    public class UserRegistration
    {
        public int UserID { get; set; }
        public string? ProviderID {get; set; }
        public string? ProviderName {get; set; }
        public string? Email {get; set; }
        public string? DisplayName {get; set; }
        public string? ProfilePictureURL { get; set; }
        public bool? NewRegistration { get; set; }
    }
}
