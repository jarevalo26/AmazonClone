using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities;

public class Product : BaseDomain
{
    [Column(TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }    
    [Column(TypeName = "nvarchar(4000)")]
    public string? Description { get; set; }    
    public int Rating { get; set; }    
    [Column(TypeName = "nvarchar(100)")]
    public string? Seller { get; set; }    
    public int Stock { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Active;    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Image>? Images { get; set; }
}