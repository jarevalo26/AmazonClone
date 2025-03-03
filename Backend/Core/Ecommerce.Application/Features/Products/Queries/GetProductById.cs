using System.Linq.Expressions;
using AutoMapper;
using Ecommerce.Application.Features.Products.ViewModels;
using Ecommerce.Application.Persistences;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries;

#region Query
public class GetProductByIdQuery(int productId) : IRequest<ProductViewModel>
{
    public int ProductId { get; set; } = productId  == 0 ? throw new ArgumentNullException(nameof(productId)) : productId;
}
#endregion

#region QueryHandler
public class GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductViewModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        List<Expression<Func<Product, object>>> includes =
        [
            p => p.Images!,
            p => p.Reviews!.OrderByDescending(r => r.CreatedDate)
        ];
        
        var product = await _unitOfWork
            .Repository<Product>()
            .GetEntityAsync( p => 
                p.Id == request.ProductId, 
                includes, 
                true
            );
        
        return _mapper.Map<ProductViewModel>(product);
    }
}
#endregion