using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
{
    public class CreateProductReviewDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Rating { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
    public class UpdateProductReviewDTO : CreateProductReviewDTO
    {

    }
    public class ProductReviewDTO : CreateProductReviewDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
    }
}
