using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Service.Dtos.ProductDtos;
using Service.Exceptions;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IBrandRepository brandRepository,IProductRepository productRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(ProductDto productDto)
        {
            if (!await _brandRepository.IsExistAsync(x => x.Id == productDto.BrandId))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "BrandId", "Brand not found");

            if(await _productRepository.IsExistAsync(x=>x.Name == productDto.Name))
                throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name already taken");


            Product product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}
