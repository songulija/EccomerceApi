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
    [Route("api/[controller")]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandsController> _logger;

        public CartsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCarts()
        {
            var carts = await _unitOfWork.Carts.GetAll();
            var results = _mapper.Map<IList<CartDTO>>(carts);
            return Ok(results);
        }
        [HttpGet("{id:int}",Name = "GetCart")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _unitOfWork.Carts.Get(c => c.Id == id);
            var results = _mapper.Map<CartDTO>(cart);
            return Ok(results);
        }

        /// <summary>
        /// check if cart model is valid. then insert and save
        /// </summary>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCart)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var cart = _mapper.Map<Cart>(cartDTO);
            await _unitOfWork.Carts.Insert(cart);
            await _unitOfWork.Save();
            //call getCart and provide id and obj
            return CreatedAtRoute("GetCart", new { id = cart.Id }, cart);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to cart obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brandDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] UpdateCartDTO cartDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateCart)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var cart = await _unitOfWork.Carts.Get(b => b.Id == id);
            if (cart == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCart)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add brandDTO values to brand
            _mapper.Map(cartDTO, cart);
            _unitOfWork.Carts.Update(cart);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _unitOfWork.Carts.Get(b => b.Id == id);
            if (cart == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCart)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Carts.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
