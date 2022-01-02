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
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrdersController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            var results = _mapper.Map<IList<OrderDTO>>(orders);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _unitOfWork.Orders.Get(c => c.Id == id);
            var results = _mapper.Map<OrderDTO>(order);
            return Ok(results);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateOrder)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var order = _mapper.Map<Order>(orderDTO);
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save();
            //call getCartItem and provide id and obj
            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to order obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateOrder)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var order = await _unitOfWork.Orders.Get(b => b.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateOrder)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add commentDTO values to comment
            _mapper.Map(orderDTO, order);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _unitOfWork.Orders.Get(b => b.Id == id);
            if (order == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteOrder)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
