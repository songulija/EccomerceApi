using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public double Tax { get; set; }
        [Required]
        public double ShippingPrice { get; set; }
        public double Subtotal { get; set; }
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
    }
    public class UpdateOrderDTO : CreateOrderDTO
    {
    }
    public class OrderDTO : CreateOrderDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public virtual IList<OrderItemDTO> OrderItems { get; set; }
        public virtual IList<PaymentDTO> Payments { get; set; }
        public virtual IList<ShipmentDTO> Shipments { get; set; }
    }
}
