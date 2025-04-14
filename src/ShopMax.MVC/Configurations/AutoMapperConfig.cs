using AutoMapper;
using ShopMax.Business.Models;
using ShopMax.MVC.Models;

namespace ShopMax.MVC.Configurations;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{
		CreateMap<Category, CategoryViewModel>().ReverseMap();
		CreateMap<Product, ProductViewModel>().ReverseMap();
	}
}
