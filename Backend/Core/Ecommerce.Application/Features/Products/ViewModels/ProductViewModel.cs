using Ecommerce.Application.Features.Images.ViewModels;
using Ecommerce.Application.Features.Reviews.ViewModels;
using Ecommerce.Application.Models.Product;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Application.Features.Products.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public string? Seller { get; set; }
    public int Stock { get; set; }
    public virtual ICollection<ReviewViewModel>? Reviews { get; set; }
    public virtual ICollection<ImageViewModel>? Images { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int ReviewNumbers { get; set; }
    public ProductStatus Status { get; set; }

    public string StatusLabel
    {
        get
        {
            switch (Status)
            {
                case ProductStatus.Active: { return ProductStatusLabel.ACTIVO; }
                case ProductStatus.Inactive:
                default:
                { return ProductStatusLabel.INACTIVO; }
            }
        }
    }
}