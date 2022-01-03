using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceCore.DTOs
{
    public class CreateUserTypeDTO
    {
        [Required]
        public string Title { get; set; }
    }
    public class UpdateUserTypeDTO : CreateUserTypeDTO
    {
    }
    public class UserTypeDTO : CreateUserTypeDTO
    {
        public int Id { get; set; }
        public virtual IList<UserDTO> Users { get; set; }
    }
}
