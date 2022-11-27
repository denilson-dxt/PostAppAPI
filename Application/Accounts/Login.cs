using System.Net;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts;

public class Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public LoginHandler(SignInManager<User> signInManager, UserManager<User> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, "User not found");
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new RestException(HttpStatusCode.Unauthorized, "The given credentials dont match with any of our data");
            return new LoginResponseDto()
            {
                Username = user.UserName,
                Fullname = user.FullName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
    
}