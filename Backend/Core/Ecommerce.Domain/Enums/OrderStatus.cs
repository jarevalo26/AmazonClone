using System.Runtime.Serialization;

namespace Ecommerce.Domain.Enums;

public enum OrderStatus
{
    [EnumMember(Value = "Order pending")]
    Pending,
    [EnumMember(Value = "Payment was received")]
    Completed,
    [EnumMember(Value = "The product was shipped")]
    Sent,
    [EnumMember(Value = "The payment had errors")]
    Error
}