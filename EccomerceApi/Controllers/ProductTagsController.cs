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
    public class ProductTagsController : ControllerBase
    {
        //initilize iunitofwork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductTagsController> _logger;

        public ProductTagsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductTagsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductTags()
        {
            var productTags = await _unitOfWork.ProductTags.GetAll();
            var results = _mapper.Map<IList<ProductTagDTO>>(productTags);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetProductTag")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductTag(int id)
        {
            var productTag = await _unitOfWork.ProductTags.Get(p => p.Id == id);
            var results = _mapper.Map<ProductTagDTO>(productTag);
            return Ok(results);
        }

        [HttpGet("product/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductTagsByProduct(int id)
        {
            var productTags = await _unitOfWork.ProductTags.GetAll(p => p.ProductId == id);
            var results = _mapper.Map<IList<ProductTagDTO>>(productTags);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductTag([FromBody] CreateProductTagDTO productTagDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProductTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var productTag = _mapper.Map<ProductTag>(productTagDTO);
            await _unitOfWork.ProductTags.Insert(productTag);
            await _unitOfWork.Save();
            //call getProductTag and provide id and obj
            return CreatedAtRoute("GetProductTag", new { id = productTag.Id }, productTag);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to productTag obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productReviewDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProductTag(int id, [FromBody] UpdateProductTagDTO productTagDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateProductTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var productTag = await _unitOfWork.ProductTags.Get(b => b.Id == id);
            if (productTag == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProductTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add productTagDTO values to productTag 
            _mapper.Map(productTagDTO, productTag);
            _unitOfWork.ProductTags.Update(productTag);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductTag(int id)
        {
            var productTag = await _unitOfWork.ProductTags.Get(b => b.Id == id);
            if (productTag == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProductTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.ProductTags.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
