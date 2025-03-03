using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class OrderAddress : BaseDomain
{
    public string? Direction { get; set; }    
    public string? City { get; set; }    
    public string? Department { get; set; }    
    public string? Zipcode { get; set; }    
    public string? Username { get; set; }    
    public string? Country { get; set; }
}