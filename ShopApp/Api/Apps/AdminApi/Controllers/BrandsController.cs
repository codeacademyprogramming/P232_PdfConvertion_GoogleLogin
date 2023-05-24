using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Dtos.BrandDtos;
using Service.Exceptions;
using Service.Interfaces;

namespace Api.Apps.AdminApi.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin")]
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("admin/api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _brandService.GetById(id));
        }


        [HttpPost("")]
        public async Task<IActionResult> Create(BrandDto brandDto)
        {
            var data = await _brandService.CreateAsync(brandDto);
            return StatusCode(StatusCodes.Status201Created, data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,BrandDto brandDto)
        {
            await _brandService.UpdateAsync(id, brandDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);
            return NoContent();
        }

    }
}
