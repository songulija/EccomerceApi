using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateCommentDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class UpdateCommentDTO : CreateCommentDTO
    {
    }
    public class CommentDTO : CreateCommentDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public UserDTO User { get; set; }
    }
}
