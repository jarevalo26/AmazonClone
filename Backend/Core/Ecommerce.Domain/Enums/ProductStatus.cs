using System.Runtime.Serialization;

namespace Ecommerce.Domain.Enums;

public enum ProductStatus
{
    [EnumMember(Value = "Producto Inactivo")]
    Inactive,
    [EnumMember(Value = "Producto Activo")]
    Active,
}