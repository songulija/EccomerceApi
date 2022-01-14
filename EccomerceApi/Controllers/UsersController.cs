using AutoMapper;
using EcommerceCore.DTOs;
using EcommerceCore.IRepository;
using EcommerceCore.Services;
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
    public class UsersController : ControllerBase
    {
        //initilize IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IAuthManager _authManager;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsersController> logger, IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _authManager = authManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAll();
            var results = _mapper.Map<IList<DisplayUserDTO>>(users);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _unitOfWork.Users.Get(u => u.Id == id);
            var results = _mapper.Map<DisplayUserDTO>(user);
            return Ok(results);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid REGISTER attempt in {nameof(RegisterUser)}");
                return BadRequest("Submited invalid data");
            }
            var user = _mapper.Map<User>(userDTO);
            //create user
            await _unitOfWork.Users.Insert(user);
            await _unitOfWork.Save();
            return Accepted(user);
        }
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check if user is invalid
            var isValid = await _authManager.ValidateUser(userDTO);
            if (isValid == false)
            {
                return Unauthorized();
            }
            var token = await _authManager.CreateToken();
            //return anything in 200 range. means it was succesful
            // return new object iwth an expression called Token. It'lll equal to
            // authManager method CrateToken which will return Token
            return Accepted(new { Token = token });
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id,[FromBody]UpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUser)}");
                return BadRequest("Submited invalid data");
            }
            var user = await _unitOfWork.Users.Get(u => u.Id == id);
            if(user == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUser)}");
                return BadRequest("Submited invalid data");
            }
            _mapper.Map(userDTO, user);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _unitOfWork.Users.Get(o => o.Id == id);
            if(user == null)
            {
                _logger.LogError($"Invalid DELETE attemptin {nameof(DeleteUser)}");
                return BadRequest("Submited invalid data");
            }
            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
