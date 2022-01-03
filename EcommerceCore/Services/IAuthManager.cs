
using EcommerceCore.DTOs;
using System.Threading.Tasks;

namespace EcommerceCore.Services
{
    /// <summary>
    /// defining methods for interface
    /// </summary>
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();

    }
}
