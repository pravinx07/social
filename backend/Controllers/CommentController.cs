using backend.Data;
using backend.Dtos.Comment;
using backend.Interface;
using backend.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo ;
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create(int stockId, CreateCommentRequestDto commentDto)
        {
            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            /*

  When you create a resource (POST), best practice dictates you return a 201 Created status code, along with a Location header telling the client where to find the new resource.nameof(GetById): "Point the URL to my GetById method."
new { id = commentModel.Id }: "Pass the new ID to the GetById method."
commentModel.ToCommentDto(): "Send the newly created comment back as JSON."*/

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            // 1. Convert DTO to a temporary Model to pass to the repository
            var commentToUpdate = new backend.Models.Comment 
            { 
                Title = updateDto.Title, 
                Content = updateDto.Content 
            };

            // 2. The Repository handles finding the record, updating it, and saving it!
            var updatedComment = await _commentRepo.UpdateAsync(id, commentToUpdate);

            if (updatedComment == null)
            {
                return NotFound();
            }

            // 3. Map the updated result back to a DTO for the user
            return Ok(updatedComment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null)
            {

                return NotFound();
            }

            // _commentRepo.Remove(commentModel);
            // await _commentRepo.SaveChangesAsync();
            return NoContent();
        }

    }
}
