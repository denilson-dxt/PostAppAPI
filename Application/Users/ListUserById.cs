using System.Net;
using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users;

public class ListUserById
{
    public class ListUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; set; }
    }

    public class ListUserByIdHandler : IRequestHandler<ListUserByIdQuery, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ListUserByIdHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(ListUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user is null)
                throw new RestException(HttpStatusCode.NotFound,"User not found");
            return _mapper.Map<UserDto>(user);
        }
        
    }
    
}