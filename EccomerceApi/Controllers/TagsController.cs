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
    public class TagsController : ControllerBase
    {
        //initilize IUnitofwork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TagsController> _logger;

        public TagsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TagsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _unitOfWork.Tags.GetAll();
            var results = _mapper.Map<IList<TagDTO>>(tags);
            return Ok(results);
        }

        [HttpGet("{id:int}", Name = "GetTag")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _unitOfWork.Tags.Get(t => t.Id == id);
            var results = _mapper.Map<TagDTO>(tag);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO tagDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var tag = _mapper.Map<Tag>(tagDTO);
            await _unitOfWork.Tags.Insert(tag);
            await _unitOfWork.Save();
            //call getTag and provide id and obj
            return CreatedAtRoute("GetTag", new { id = tag.Id }, tag);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to tag obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tagDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagDTO tagDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var tag = await _unitOfWork.Tags.Get(b => b.Id == id);
            if (tag == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add tagDTO values to tag
            _mapper.Map(tagDTO, tag);
            _unitOfWork.Tags.Update(tag);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _unitOfWork.Tags.Get(b => b.Id == id);
            if (tag == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteTag)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Tags.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
