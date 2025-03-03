using AutoMapper;
using Ecommerce.Application.Features.Images.ViewModels;
using Ecommerce.Application.Features.Products.ViewModels;
using Ecommerce.Application.Features.Reviews.ViewModels;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductViewModel>()
            .ForMember(
                dest => dest.CategoryName, 
                opt => opt.MapFrom(src => src.Category!.Name)
            )
            .ForMember(
                dest => dest.ReviewNumbers, 
                opt => opt.MapFrom(src => src.Reviews == null ? 0 : src.Reviews.Count)
            );

        CreateMap<Image, ImageViewModel>();
        CreateMap<Review, ReviewViewModel>();
    }
}