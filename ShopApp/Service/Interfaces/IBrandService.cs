using Core.Entities;
using Service.Dtos.BrandDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBrandService
    {
        public Task<List<BrandGetAllItemDto>> GetAllAsync();
        public Task<BrandGetDto> GetById(int id);
        public Task<BrandCreateResponseDto> CreateAsync(BrandDto brandDto);
        public Task UpdateAsync(int id,BrandDto brandDto);
        public Task DeleteAsync(int id);

    }
}
