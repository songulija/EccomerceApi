using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
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
