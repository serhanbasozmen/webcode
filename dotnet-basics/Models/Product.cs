namespace dotnet_basics.Models;
    public class Product
    {
        public int Id { get; set; }
        public string? productTitle { get; set; }
        public string? productExplation { get; set; }
        public double productPrice { get; set; }
        public string? productImage { get; set; }
        public bool productStock { get; set; }

        public int HowMuchStock { get; set; }
        public bool IsHome { get; set; }
    }