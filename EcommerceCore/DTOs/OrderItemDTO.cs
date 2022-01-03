using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateOrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
    public class UpdateOrderItemDTO : CreateOrderItemDTO
    {
    }
    public class OrderItemDTO : CreateOrderItemDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public OrderDTO Order { get; set; }
    }
}
