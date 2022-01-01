using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
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
