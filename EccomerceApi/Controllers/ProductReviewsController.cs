using AutoMapper;
using EccomerceApi.IRepository;
using EccomerceApi.Models;
using EccomerceApi.ModelsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductReviewsController : ControllerBase
    {
        //initilize iunitofwork 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductReviewsController> _logger;

        public ProductReviewsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductReviewsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductReviews()
        {
            var productReviews = await _unitOfWork.ProductReviews.GetAll();
            var results = _mapper.Map<IList<ProductReviewDTO>>(productReviews);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetProductReview")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductReview(int id)
        {
            var productReview = await _unitOfWork.ProductReviews.Get(p => p.Id == id);
            var results = _mapper.Map<ProductReviewDTO>(productReview);
            return Ok(results);
        }

        [HttpGet("product/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductReviewByProduct(int id)
        {
            var productReviews = await _unitOfWork.ProductReviews.GetAll(p => p.ProductId == id);
            var results = _mapper.Map<IList<ProductReviewDTO>>(productReviews);
            return Ok(results);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProductReview([FromBody] CreateProductReviewDTO productReviewDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateProductReview)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var productReview = _mapper.Map<ProductReview>(productReviewDTO);
            await _unitOfWork.ProductReviews.Insert(productReview);
            await _unitOfWork.Save();
            //call getProduct and provide id and obj
            return CreatedAtRoute("GetProductReview", new { id = productReview.Id }, productReview);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to productReview obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productReviewDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProductReview(int id, [FromBody] UpdateProductReviewDTO productReviewDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateProductReview)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var productReview = await _unitOfWork.ProductReviews.Get(b => b.Id == id);
            if (productReview == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProductReview)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add productReviewDTO values to productReview
            _mapper.Map(productReviewDTO, productReview);
            _unitOfWork.ProductReviews.Update(productReview);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductReview(int id)
        {
            var productReview = await _unitOfWork.ProductsReview.Get(b => b.Id == id);
            if (productReview == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteProductReview)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.ProductReviews.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
