using AutoMapper;
using JWTAppBackOffice.Core.Domain;
using JWTAppBackOffice.Core.DTOs;

namespace JWTAppBackOffice.Core.Application.Mappings
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
        }
    }
}
