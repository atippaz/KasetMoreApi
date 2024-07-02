
namespace KasetMore.ApplicationCore.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }

        public string Province { get; set; }

        public double Rating { get; set; }
        public string Category { get; set; }

        public int Amount { get; set; }

        public string UserEmail { get; set; }

        public decimal Price { get; set; }

        //public virtual List<IFormFile> ProductImages { get; set; }
    }
}
