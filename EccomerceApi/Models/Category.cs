using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Models
{
    public class Category : BaseModel
    {
        //id of parent category
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
