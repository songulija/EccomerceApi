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
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PaymentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _unitOfWork.Payments.GetAll();
            var results = _mapper.Map<IList<PaymentDTO>>(payments);
            return Ok(results);
        }
        [HttpGet("{id:int}",Name = "GetPayment")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _unitOfWork.Payments.Get(p => p.Id == id);
            var results = _mapper.Map<PaymentDTO>(payment);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreatePayment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var payment = _mapper.Map<Payment>(paymentDTO);
            await _unitOfWork.Payments.Insert(payment);
            await _unitOfWork.Save();
            //call getCartItem and provide id and obj
            return CreatedAtRoute("GetPayment", new { id = payment.Id }, payment);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to payment obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdatePayment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var payment = await _unitOfWork.Payments.Get(b => b.Id == id);
            if (payment == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePayment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add paymentDTO values to payment
            _mapper.Map(paymentDTO, payment);
            _unitOfWork.Payments.Update(payment);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _unitOfWork.Payments.Get(b => b.Id == id);
            if (payment== null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePayment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Payments.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
