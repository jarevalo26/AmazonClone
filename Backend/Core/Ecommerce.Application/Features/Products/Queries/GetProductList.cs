using System.Linq.Expressions;
using AutoMapper;
using Ecommerce.Application.Features.Products.ViewModels;
using Ecommerce.Application.Persistences;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries;

#region Query
public record GetProductListQuery : IRequest<IReadOnlyList<ProductViewModel>>;
#endregion

#region QueryHandler
public class GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductViewModel>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<ProductViewModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        List<Expression<Func<Product, object>>>? includes = [
            p => p.Images!,
            p => p.Reviews!
        ]!;
        
        var products = await _unitOfWork
            .Repository<Product>()
            .GetAsync(
                null, 
                x => x.OrderBy(y => y.Name), 
                includes, 
                true
            );
        
        var productsViewModel = _mapper.Map<IReadOnlyList<ProductViewModel>>(products);
        return productsViewModel;
    }
}
#endregion