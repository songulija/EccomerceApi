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
    public class ProductCategoriesController : ControllerBase
    {
        //initilize iunitofwork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCategoriesController> _logger;

        public ProductCategoriesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductCategoriesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCategories()
        {
            var productCategories = await _unitOfWork.ProductCategories.GetAll();
            var results = _mapper.Map<IList<ProductCategoryDTO>>(productCategories);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetProductCategory")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCategory(int id)
        {
            var productCategory = await _unitOfWork.ProductCategories.Get(p => p.Id == id);
            var results = _mapper.Map<ProductCategoryDTO>(productCategory);
            return Ok(results);
        }
        [HttpGet("product/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductCategoriesByProduct(int id)
        {
            var productCategories = await _unitOfWork.ProductCategories.GetAll(p => p.ProductId == id);
            var results = _mapper.Map<IList<ProductCategoryDTO>>(productCategories);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryDTO productCategoryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProductCategory)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
            await _unitOfWork.ProductCategories.Insert(productCategory);
            await _unitOfWork.Save();
            //call getProductCategory and provide id and obj
            return CreatedAtRoute("GetProductCategory", new { id = productCategory.Id }, productCategory);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to productCategory obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProductCategory(int id, [FromBody] UpdateProductCategoryDTO productCategoryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateProductCategory)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var productCategory = await _unitOfWork.ProductCategories.Get(b => b.Id == id);
            if (productCategory == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProductCategory)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add productCategoryDTO values to productCategory
            _mapper.Map(productCategoryDTO, productCategory);
            _unitOfWork.ProductCategories.Update(productCategory);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _unitOfWork.ProductCategories.Get(b => b.Id == id);
            if (productCategory == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProductCategory)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.ProductCategories.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }


    }
}
