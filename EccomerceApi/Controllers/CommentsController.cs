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
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CommentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _unitOfWork.Comments.GetAll();
            var results = _mapper.Map<IList<CommentDTO>>(comments);
            return Ok(results);
        }
        [HttpGet("product/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductComments(int id)
        {
            var comments = await _unitOfWork.Comments.GetAll(c => c.ProductId == id);
            var results = _mapper.Map<IList<CommentDTO>>(comments);
            return Ok(results);
        }

        [HttpGet("{id:int}",Name = "GetComment")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _unitOfWork.Comments.Get(c => c.Id == id);
            var results = _mapper.Map<CommentDTO>(comment);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(CreateComment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var comment = _mapper.Map<Comment>(commentDTO);
            await _unitOfWork.Comments.Insert(comment);
            await _unitOfWork.Save();
            //call getCartItem and provide id and obj
            return CreatedAtRoute("GetComment", new { id = comment.Id }, comment);
        }
        /// <summary>
        /// Check if valid, check if exist. Then add dto values to comment obj
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commentDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] UpdateCommentDTO commentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(UpdateComment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            var comment = await _unitOfWork.Comments.Get(b => b.Id == id);
            if (comment == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateComment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            // add commentDTO values to comment
            _mapper.Map(commentDTO, comment);
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _unitOfWork.Comments.Get(b => b.Id == id);
            if (comment == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteComment)}");
                return BadRequest("Įvesti neteisingi duomenis");
            }
            await _unitOfWork.Comments.Delete(id);
            await _unitOfWork.Save();
            return NoContent();
        }
    }
}
