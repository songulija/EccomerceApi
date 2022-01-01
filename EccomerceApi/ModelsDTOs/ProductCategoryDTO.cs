using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
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
