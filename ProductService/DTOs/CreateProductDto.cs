using System.ComponentModel.DataAnnotations;

namespace ProductService.DTOs
{
 public class CreateProductDto
 {
 [Required]
 public string Name { get; set; } = null!;

 [Range(0, double.MaxValue)]
 public decimal Price { get; set; }

 [Range(0, int.MaxValue)]
 public int Stock { get; set; }

 public string Description { get; set; } = string.Empty;
 }
}
