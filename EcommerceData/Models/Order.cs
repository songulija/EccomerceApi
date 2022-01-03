using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceData.Models
{
    public class Order : BaseModel
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public double Tax { get; set; }
        public double ShippingPrice { get; set; }
        //without vat
        public double SubTotal { get; set; }
        //with vat
        public double Total { get; set; }
        // paypal,stripe etc.
        public string PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public bool IsDelivered { get; set; }
        //it can be delivered later on
        public DateTime? DeliveredAt { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
