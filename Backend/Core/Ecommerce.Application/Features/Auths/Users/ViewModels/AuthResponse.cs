using Ecommerce.Application.Features.Addresses.ViewModels;
using Stripe;

namespace Ecommerce.Application.Features.Auths.Users.ViewModels;

public class AuthResponse
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? Avatar { get; set; }
    public AddressViewModel? Address { get; set; }
    public ICollection<string>? Roles { get; set; }
}