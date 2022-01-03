using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateProductTagDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int TagId { get; set; }
    }
    public class UpdateProductTagDTO : CreateProductTagDTO
    {
    }
    public class ProductTagDTO : CreateProductTagDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public TagDTO Tag { get; set; }
    }
}
