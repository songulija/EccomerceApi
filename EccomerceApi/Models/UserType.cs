using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EccomerceApi.Models
{
    public class UserType : BaseModel
    {
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
