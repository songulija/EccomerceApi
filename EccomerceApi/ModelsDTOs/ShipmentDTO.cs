using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
{
    public class CreateShipmentDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Country { get; set; }
    }
    public class UpdateShipmentDTO : CreateShipmentDTO
    {
    }
    public class ShipmentDTO : CreateShipmentDTO
    {
        public int Id { get; set; }
        public virtual OrderDTO Order { get; set; }
    }
}
