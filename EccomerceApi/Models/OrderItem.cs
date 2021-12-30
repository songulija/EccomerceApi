using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Models
{
    public class OrderItem : BaseModel
    {
       [ForeignKey(nameof(Product))]
       public int ProductId { get; set; }
       public virtual Product Product { get; set; }
       [ForeignKey(nameof(Order))]
       public int OrderId { get; set; }
       public virtual Order Order { get; set; }
    }
}
