using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateBrandDTO
    {
        [Required]
        public string Title { get; set; }
        public string MetaTitle { get; set; }
    }
    public class UpdateBrandDTO : CreateBrandDTO
    {

    }
    public class BrandDTO : CreateBrandDTO
    {
        public int Id { get; set; }
        public virtual IList<ProductDTO> Products { get; set; }
    }
}
