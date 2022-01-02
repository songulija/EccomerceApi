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
    public class UserTypesController : ControllerBase
    {
        //inilize IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserTypesController> _logger;

        public UserTypesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserTypesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserTypes()
        {
            var userTypes = await _unitOfWork.UserTypes.GetAll();
            var results = _mapper.Map<IList<UserTypeDTO>>(userTypes);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetUserType")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserType(int id)
        {
            var userType = await _unitOfWork.UserTypes.Get(u => u.Id == id);
            var results = _mapper.Map<UserTypeDTO>(userType);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserType([FromBody] CreateUserTypeDTO userTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateUserType)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var userType = _mapper.Map<UserType>(userTypeDTO);
            await _unitOfWork.UserTypes.Insert(userType);
            await _unitOfWork.Save();
            //call getTag and provide id and obj
            return CreatedAtRoute("GetUserType", new { id = userType.Id }, userType);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to tag obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userTypeDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUserType(int id, [FromBody] UpdateUserTypeDTO userTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateUserType)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var userType = await _unitOfWork.UserTypes.Get(b => b.Id == id);
            if (userType == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUserType)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add tagDTO values to tag
            _mapper.Map(userTypeDTO, userType);
            _unitOfWork.UserTypes.Update(userType);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var userType = await _unitOfWork.UserTypes.Get(b => b.Id == id);
            if (userType == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteUserType)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.UserTypes.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
