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
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.Products.GetAll(includeProperties: "Brand");
            var results = _mapper.Map<IList<ProductDTO>>(products);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(p => p.Id == id, includeProperties: "Brand");
            var results = _mapper.Map<ProductDTO>(product);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProduct)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var product = _mapper.Map<Product>(productDTO);
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();
            //call getProduct and provide id and obj
            var createdProduct = await _unitOfWork.Products.Get(p => p.Id == product.Id);
            var result = _mapper.Map<ProductDTO>(createdProduct);
            return Ok(result);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to product obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateProduct)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var product = await _unitOfWork.Products.Get(b => b.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProduct)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add productDTO values to product
            _mapper.Map(productDTO, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.Get(b => b.Id == id);
            if (product == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProduct)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
