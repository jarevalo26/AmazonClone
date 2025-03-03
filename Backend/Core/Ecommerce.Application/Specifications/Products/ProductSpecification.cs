using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Specifications.Products;

public class ProductSpecification : Specification<Product>
{
    public ProductSpecification(ProductSpecificationParams productParams) : base(p => 
        (string.IsNullOrEmpty(productParams.Search) || 
         p.Name!.Contains(productParams.Search) || 
         p.Description!.Contains(productParams.Search)) &&
        (!productParams.CategoryId.HasValue || p.CategoryId == productParams.CategoryId) &&
        (!productParams.MinPrice.HasValue || p.Price >= productParams.MinPrice) &&
        (!productParams.MaxPrice.HasValue || p.Price <= productParams.MaxPrice) &&
        (!productParams.Status.HasValue || p.Status == productParams.Status)
    )
    {
        AddInclude(p => p.Reviews!);
        AddInclude(p => p.Images!);
        
        ApplyPaging(productParams.PageSize * (productParams.PageIndex-1), productParams.PageSize);
        
        if (!string.IsNullOrEmpty(productParams.Sort))
            switch (productParams.Sort)
            {
                case "nameAsc":
                    AddOrderBy(p => p.Name!);
                    break;
                case "nameDesc":
                    AddOrderByDescending(p => p.Name!);
                    break;
                case "priceAsc":
                    AddOrderBy(p => p.Price!);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price!);
                    break;
                case "ratingAsc":
                    AddOrderBy(p => p.Rating!);
                    break;
                case "ratingDesc":
                    AddOrderByDescending(p => p.Rating!);
                    break;
                default:
                    AddOrderBy(p => p.CreatedDate!);
                    break;
            }
        else
            AddOrderByDescending(p => p.CreatedDate!);
    }
}