using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
{    
    public class CreateCartDTO
    {
        [Required]
        public int UserId { get; set; }
    }
    public class UpdateCartDTO : CreateCartDTO
    {
    }
    public class CartDTO : CreateCartDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public IList<CartItemDTO> CartItems { get; set; }
    }

}
