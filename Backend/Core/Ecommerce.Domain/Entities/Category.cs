using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class Category : BaseDomain
{
    [Column(TypeName = "varchar(100)")]
    public string? Name { get; set; }
    public ICollection<Product>? Products { get; set; }
}