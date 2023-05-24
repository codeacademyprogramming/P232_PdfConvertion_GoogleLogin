using AutoMapper;
using Core.Entities;
using Service.Dtos.BrandDtos;

namespace Api.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Brand, BrandGetAllItemDto>();

            CreateMap<Brand, BrandGetDto>();

            CreateMap<BrandDto, Brand>();
            CreateMap<Brand, BrandCreateResponseDto>();
        }
    }
}
