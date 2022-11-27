using Application.Dtos;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users;

public class ListUserName
{
    public class ListUserByNameQuery : IRequest<List<UserDto>>
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }

    public class ListUserByNameHandler : IRequestHandler<ListUserByNameQuery, List<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ListUserByNameHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(ListUserByNameQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Where(u => u.UserName == request.Username).ToListAsync();
            
            return _mapper.Map<List<UserDto>>(users);
        }
        
    }
    
}