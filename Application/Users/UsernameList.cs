using Application.Interfaces;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users;

public class UsernameList
{
    public class UsernameListQuery : IRequest<List<string>>
    {
        
    }

    public class UsernameListHandler : IRequestHandler<UsernameListQuery, List<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserNamesList _userNamesList;

        public UsernameListHandler(UserManager<User> userManager, IUserNamesList userNamesList)
        {
            _userManager = userManager;
            _userNamesList = userNamesList;
        }
        public async Task<List<string>> Handle(UsernameListQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _userManager.Users.ToListAsync(cancellationToken);
            return _userNamesList.GetUserNames(usersList);
        }
    }
}