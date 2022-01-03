using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceData.Models
{
    /// <summary>
    /// many to one relationship with Orders. These atributes
    /// are basically for paypal. becouse paypal returns status,code,emailAdress of paypal account
    /// </summary>
    public class Payment : BaseModel
    {
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
