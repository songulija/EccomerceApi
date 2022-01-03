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
    public class CartItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CartItemsController> _logger;

        public CartItemsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CartItemsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _unitOfWork.CartItems.GetAll();
            var results = _mapper.Map<IList<CartItemDTO>>(cartItems);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetCartItem")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItem(int id)
        {
            var cartItem = await _unitOfWork.CartItems.Get(c => c.Id == id);
            var results = _mapper.Map<CartItemDTO>(cartItem);
            return Ok(results);
        }

        [HttpGet("cart/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCartItemsByCart(int id)
        {
            var cartItem = await _unitOfWork.CartItems.GetAll(c => c.CartId == id);
            var results = _mapper.Map<CartItemDTO>(cartItem);
            return Ok(results);
        }

        /// <summary>
        /// check if cartItems model is valid. then insert and save
        /// </summary>
        /// <param name="CreateCartItemDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCartItem([FromBody] CreateCartItemDTO cartItemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateCartItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var cartItem = _mapper.Map<CartItem>(cartItemDTO);
            await _unitOfWork.CartItems.Insert(cartItem);
            await _unitOfWork.Save();
            //call getCartItem and provide id and obj
            return CreatedAtRoute("GetCartItem", new { id = cartItem.Id }, cartItem);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to cart obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartItemDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] UpdateCartItemDTO cartItemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateCartItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var cartItem = await _unitOfWork.CartItems.Get(b => b.Id == id);
            if (cartItem == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCartItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add brandDTO values to brand
            _mapper.Map(cartItemDTO, cartItem);
            _unitOfWork.CartItems.Update(cartItem);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _unitOfWork.CartItems.Get(b => b.Id == id);
            if (cartItem == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCartItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.CartItems.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
