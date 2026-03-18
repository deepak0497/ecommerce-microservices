namespace ProductService.Models
{
 public class Product
 {
 public Guid Id { get; set; }
 public string Name { get; set; } = null!;
 public decimal Price { get; set; }

 public int Stock { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
