using Application.Interfaces;
using Doiman;

namespace Infrastruture.Services;

public class UserNamesList : IUserNamesList
{
    public List<string> GetUserNames(List<User> users)
    {
        return users.Select(x => x.UserName).ToList();
    }
}