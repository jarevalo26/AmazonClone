using System.Security.Principal;

namespace Ecommerce.Application.Models.ImageManagement;

public class CloudinarySettings
{
    public string? CloudName { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
}