using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Specifications.Products;

public class ProductForCountingSpecification(ProductSpecificationParams productParams) : Specification<Product>(p =>
    (string.IsNullOrEmpty(productParams.Search) || 
     p.Name!.Contains(productParams.Search) || 
     p.Description!.Contains(productParams.Search)) &&
    (!productParams.CategoryId.HasValue || p.CategoryId == productParams.CategoryId) &&
    (!productParams.MinPrice.HasValue || p.Price >= productParams.MinPrice) &&
    (!productParams.MaxPrice.HasValue || p.Price <= productParams.MaxPrice) &&
    (!productParams.Status.HasValue || p.Status == productParams.Status));