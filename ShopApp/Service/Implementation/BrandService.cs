using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos.BrandDtos;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BrandService(IBrandRepository brandRepository, IMapper mapper,IWebHostEnvironment env)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _env = env;
        }
        public async Task<BrandCreateResponseDto> CreateAsync(BrandDto brandDto)
        {
            if(await _brandRepository.IsExistAsync(x=>x.Name == brandDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest,"Name","Name already taken");
                
            Brand brand = _mapper.Map<Brand>(brandDto);

            await _brandRepository.AddAsync(brand);
            await _brandRepository.SaveChangesAsync();

            return _mapper.Map<BrandCreateResponseDto>(brand);
        }

        public async Task DeleteAsync(int id)
        {
            Brand brand = await _brandRepository.GetAsync(x => x.Id == id);

            if (brand == null)
                throw new RestException(System.Net.HttpStatusCode.NotFound,"Brand not found");

            _brandRepository.Remove(brand);
            await _brandRepository.SaveChangesAsync();
        }

        public async Task<List<BrandGetAllItemDto>> GetAllAsync()
        {
            var data = await _brandRepository.GetAllAsync();

            List<BrandGetAllItemDto> items = _mapper.Map<List<BrandGetAllItemDto>>(data);

            return items;
        }

        public async Task<BrandGetDto> GetById(int id)
        {
            var data = await _brandRepository.GetAsync(x => x.Id == id, "Products");

            if (data == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Brand not found");

            BrandGetDto dto = _mapper.Map<BrandGetDto>(data);

            dto.Name = Directory.GetCurrentDirectory();
            return dto;
        }

        public async Task UpdateAsync(int id, BrandDto brandDto)
        {
            var existData = await _brandRepository.GetAsync(x => x.Id == id);

            if (existData == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Brand not found");

            if (existData.Name != brandDto.Name && await _brandRepository.IsExistAsync(x => x.Name == brandDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name already taken");

            existData.Name = brandDto.Name;
            await _brandRepository.SaveChangesAsync();
        }
    }
}
