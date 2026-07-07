
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(280)]
        public string Title { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public string Content { get; set; }


    }
}