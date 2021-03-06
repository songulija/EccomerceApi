using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreatePaymentDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Status { get; set; }
        public string Code { get; set; }
        public string EmailAddress { get; set; }
        public DateTime UpdateTime { get; set; }
    }
    public class UpdatePaymentDTO : CreatePaymentDTO
    {
    }
    public class PaymentDTO : CreatePaymentDTO
    {
        public int Id { get; set; }
        public OrderDTO Order { get; set; }
    }
}
