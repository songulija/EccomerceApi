using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.ModelsDTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class UserDTO : LoginUserDTO
    {
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserTypeDTO UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual IList<CartDTO> Carts { get; set; }
        public virtual IList<CommentDTO> Comments { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }

    public class DisplayUserDTO
    {
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserTypeDTO UserType { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual IList<CartDTO> Carts { get; set; }
        public virtual IList<CommentDTO> Comments { get; set; }
        public virtual IList<OrderDTO> Orders { get; set; }
    }
}
