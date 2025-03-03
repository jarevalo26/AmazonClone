using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class Shoppingcart : BaseDomain
{
    public Guid ShoppingCartMasterId { get; set; }
    public ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }
}