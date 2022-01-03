using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceData.Models
{
    /// <summary>
    /// has many to one relationship with Orders
    /// </summary>
    public class Shipment : BaseModel
    {
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order {get;set;}
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
