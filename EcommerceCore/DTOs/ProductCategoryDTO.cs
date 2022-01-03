using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateProductCategoryDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
    public class UpdateProductCategoryDTO : CreateProductCategoryDTO
    {
    }
    public class ProductCategoryDTO : CreateProductCategoryDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
