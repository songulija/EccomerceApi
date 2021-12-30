using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Models
{
    /// <summary>
    /// One to many relationship with Products. 
    /// </summary>
    public class Brand : BaseModel
    {
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
