using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateTagDTO
    {
        [Required]
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
    }
    public class UpdateTagDTO : CreateTagDTO
    {
        public int Id { get; set; }
    }
    public class TagDTO : CreateTagDTO
    {
        public int Id { get; set; }
        public virtual IList<ProductTagDTO> ProductTags { get; set; }
    }
}
