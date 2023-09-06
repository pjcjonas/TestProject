namespace api_gaming_global.Models.Request
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public string? DateAdded { get; set; }
        public int? ProductID { get; set; }
        public int? CategoryID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
        public string? ImageURL { get; set; }
    }
}
