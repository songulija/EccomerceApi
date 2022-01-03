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
    public class OrderItemsController : ControllerBase
    {
        //initilizing IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderItemsController> _logger;

        public OrderItemsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderItemsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderItems()
        {
            var orderItems = await _unitOfWork.OrderItems.GetAll();
            var results = _mapper.Map<IList<OrderItemDTO>>(orderItems);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetOrderItem")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.Get(o => o.Id == id);
            var results = _mapper.Map<OrderItemDTO>(orderItem);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDTO orderItemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateOrderItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var orderItem = _mapper.Map<OrderItem>(orderItemDTO);
            await _unitOfWork.OrderItems.Insert(orderItem);
            await _unitOfWork.Save();
            //call getCartItem and provide id and obj
            return CreatedAtRoute("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to orderItem obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] UpdateOrderItemDTO orderItemDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateOrderItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var orderItem = await _unitOfWork.OrderItems.Get(b => b.Id == id);
            if (orderItem == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrderItem)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add orderITemDTO values to orderItem
            _mapper.Map(orderItemDTO, orderItem);
            _unitOfWork.OrderItems.Update(orderItem);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.Get(b => b.Id == id);
            if (orderItem == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(OrderItemDTO)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.OrderItems.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }

    }
}
