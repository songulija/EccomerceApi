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
    public class ShipmentsController : ControllerBase
    {
        //initilize iunitofwork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ShipmentsController> _logger;

        public ShipmentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ShipmentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShipments()
        {
            var shipments = await _unitOfWork.Shipments.GetAll();
            var results = _mapper.Map<IList<ShipmentDTO>>(shipments);
            return Ok(results);
        }
        [HttpGet("{id:int}", Name = "GetShipment")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShipment(int id)
        {
            var shipment = await _unitOfWork.Shipments.Get(s => s.Id == id);
            var results = _mapper.Map<ShipmentDTO>(shipment);
            return Ok(results);
        }

        [HttpGet("order/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShipmentByOrder(int id)
        {
            var shipment = await _unitOfWork.Shipments.Get(s => s.OrderId == id);
            var results = _mapper.Map<ShipmentDTO>(shipment);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentDTO shipmentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateShipment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var shipment = _mapper.Map<Shipment>(shipmentDTO);
            await _unitOfWork.Shipments.Insert(shipment);
            await _unitOfWork.Save();
            //call getShipment and provide id and obj
            return CreatedAtRoute("GetShipment", new { id = shipment.Id }, shipment);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to shipment obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateShipment(int id, [FromBody] UpdateShipmentDTO shipmentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateShipment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var shipment = await _unitOfWork.Shipments.Get(b => b.Id == id);
            if (shipment == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateShipment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add shipmentDTO values to shipment
            _mapper.Map(shipmentDTO, shipment);
            _unitOfWork.Shipments.Update(shipment);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var shipment = await _unitOfWork.Shipments.Get(b => b.Id == id);
            if (shipment == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteShipment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Shipments.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
