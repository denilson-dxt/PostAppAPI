using Doiman;

namespace Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}