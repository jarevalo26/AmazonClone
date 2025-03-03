using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities;

public class Order : BaseDomain
{

    public Order() { }

    public Order(
        string buyerName, 
        string buyerUsername,
        OrderAddress orderAddress,
        decimal subtotal,
        decimal total,
        decimal tax,
        decimal shippingPrice
        )
    {
        BuyerName = buyerName;
        BuyerUsername = buyerUsername;
        OrderAddress = orderAddress;
        Subtotal = subtotal;
        Total = total;
        Tax = tax;
        ShippingPrice = shippingPrice;
    }
    
    public string? BuyerName { get; set; }
    public string? BuyerUsername { get; set; }
    public OrderAddress? OrderAddress { get; set; }
    public IReadOnlyList<OrderItem>? OrderItems { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Tax { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal ShippingPrice { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public string? StripeApiKey { get; set; }
}