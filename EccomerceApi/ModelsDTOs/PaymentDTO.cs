using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
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
        public virtual OrderDTO Order { get; set; }
    }
}
