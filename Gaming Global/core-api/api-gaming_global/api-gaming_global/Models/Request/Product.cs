namespace api_gaming_global.Models.Request
{
    public class Product
    {
        public int? ProductID {get; set;}
        public int? CategoryID {get; set;}
        public string? ProductName {get; set;}
        public string? Description {get; set;}
        public float? Price {get; set;}
        public string? ImageURL { get; set; }

    }
}
