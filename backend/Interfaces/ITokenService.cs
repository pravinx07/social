using Microsoft.AspNetCore.Identity;


namespace backend.Interface
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user);

    }
}