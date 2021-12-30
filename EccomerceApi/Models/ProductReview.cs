using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Models
{
    public class ProductReview : BaseModel
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
