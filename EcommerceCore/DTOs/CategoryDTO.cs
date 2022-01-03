using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        public int ParentId { get; set; }
        [Required]
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
    }
    public class UpdateCategoryDTO : CreateCategoryDTO
    {

    }
    public class CategoryDTO : CreateCategoryDTO
    {
        public int Id { get; set; }
        public virtual IList<ProductCategoryDTO> ProductCategories { get; set; }
    }
}
