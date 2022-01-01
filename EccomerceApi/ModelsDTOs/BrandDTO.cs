using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
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
