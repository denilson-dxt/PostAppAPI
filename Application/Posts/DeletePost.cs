using Application.Interfaces;
using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Aplication.Posts;
public class DeletePost
{
    public class DeletePostCommand: IRequest<PostDto>
    {
        public int Id{get;set;}
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, PostDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<User> _userManager;

        public DeletePostCommandHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _userManager = userManager;
        }
        public async Task<PostDto> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = _context.Post.Include(p=>p.User).First(p => p.Id == request.Id);
            if(post == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Post not found");
            var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());
            if(user.Id != post.User.Id) throw new RestException(System.Net.HttpStatusCode.Forbidden, "You are not allowed to delete this post");

           _context.Post.Remove(post);
           await _context.SaveChangesAsync();

           return _mapper.Map<PostDto>(post);
           
        }
    }
}