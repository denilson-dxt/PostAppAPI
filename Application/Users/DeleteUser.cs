using System.Net;
using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users;

public class DeleteUser
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public string Id { get; set; }
    }
    public class DeleteUserHandler:IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DeleteUserHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound,"User not found");
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.ToString());
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}