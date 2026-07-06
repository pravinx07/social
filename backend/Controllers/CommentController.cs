using backend.Data;
using backend.Dtos.Comment;
using backend.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _context.Comments.ToListAsync();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _context.Comments.FindAsync(id);

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
            await _context.Comments.AddAsync(commentModel);

            await _context.SaveChangesAsync();

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
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
            {
                return NotFound();
            }

            commentModel.Title = updateDto.Title;
            commentModel.Content = updateDto.Content;

            await _context.SaveChangesAsync();
            return Ok(commentModel.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {

                return NotFound();
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
