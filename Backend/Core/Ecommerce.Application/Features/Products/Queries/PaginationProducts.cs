using AutoMapper;
using Ecommerce.Application.Features.Products.ViewModels;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Persistences;
using Ecommerce.Application.Specifications.Products;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries;

#region Query
public class PaginationProductsQuery : PaginationQuery, IRequest<PaginationViewModel<ProductViewModel>>
{
    public int? CategoryId { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public int? Rating { get; set; }
    public ProductStatus Status { get; set; }
}
#endregion

#region QueryHandler
public class PaginationProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<PaginationProductsQuery, PaginationViewModel<ProductViewModel>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginationViewModel<ProductViewModel>> Handle(PaginationProductsQuery request, CancellationToken cancellationToken)
    {
        var productSpecificationParams = new ProductSpecificationParams()
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            CategoryId = request.CategoryId,
            MaxPrice = request.MaxPrice,
            MinPrice = request.MinPrice,
            Rating = request.Rating,
            Status = request.Status
        };
        
        var specification = new ProductSpecification(productSpecificationParams);
        var products = await _unitOfWork.Repository<Product>().GetAllWithSpecification(specification);
        
        var specificationCount = new ProductForCountingSpecification(productSpecificationParams);
        var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(specificationCount);
        
        var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);
        
        var data = _mapper.Map<IReadOnlyList<ProductViewModel>>(products);
        var producstByPage = products.Count;

        var pagination = new PaginationViewModel<ProductViewModel>
        {
            Count = totalProducts,
            Data = data,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            PageCount = totalPages,
            ResultByPage = producstByPage
        };
        return pagination;
    }
}
#endregion
