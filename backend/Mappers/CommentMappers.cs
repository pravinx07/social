using backend.Dtos.Comment;
using backend.Models;
namespace backend.Mappers
{
    public static class CommentMappers
    {
        // this method is use to send comment to the user 
        public static CommentDto ToCommentDto(this Comment commentModet)
        {

            return new CommentDto
            {
                Id = commentModet.Id,
                StockId = commentModet.StockId,
                Title = commentModet.Title,
                Content = commentModet.Content,
                CreatedOn = commentModet.CreatedOn
            };
        }
        // this method is use to send data from user input to database
// CreateCommentRequestDto dto  => bcos the source data is the DTO and we also need to pass in stockId bcos dto doesnt have it 
        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
    }
}