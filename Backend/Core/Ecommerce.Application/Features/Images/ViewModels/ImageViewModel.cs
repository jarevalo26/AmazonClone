namespace Ecommerce.Application.Features.Images.ViewModels;

public class ImageViewModel
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public int ProductId { get; set; }
    public string? PublicCode { get; set; }
}