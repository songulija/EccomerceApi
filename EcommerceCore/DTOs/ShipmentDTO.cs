using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
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
