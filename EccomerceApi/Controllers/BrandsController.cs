using AutoMapper;
using EcommerceCore.DTOs;
using EcommerceCore.IRepository;
using EcommerceData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EccomerceApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _unitOfWork.Brands.GetAll();
            var results = _mapper.Map<IList<BrandDTO>>(brands);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetBrand")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _unitOfWork.Brands.Get(b => b.Id == id);
            var results = _mapper.Map<BrandDTO>(brand);
            return Ok(results);
        }
        /// <summary>
        /// check if brand model is valid. then insert and save
        /// </summary>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDTO brandDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateBrand)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var brand = _mapper.Map<Brand>(brandDTO);
            await _unitOfWork.Brands.Insert(brand);
            await _unitOfWork.Save();
            //call getBrand and provide id and obj
            return CreatedAtRoute("GetBrand", new { id = brand.Id }, brand);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to brand obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody] UpdateBrandDTO brandDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateBrand)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var brand = await _unitOfWork.Brands.Get(b => b.Id == id);
            if (brand == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateBrand)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add brandDTO values to brand
            _mapper.Map(brandDTO, brand);
            _unitOfWork.Brands.Update(brand);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.Brands.Get(b => b.Id == id);
            if (brand == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteBrand)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Brands.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
