using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateCartDTO
    {
        /*[Required]
        public int UserId { get; set; }*/
    }
    public class UpdateCartDTO : CreateCartDTO
    {
    }
    public class CartDTO : CreateCartDTO
    {
        public int Id { get; set; }
       /* public UserDTO User { get; set; }
        public IList<CartItemDTO> CartItems { get; set; }*/
    }

}
