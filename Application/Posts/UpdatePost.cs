using System.Net;
using Application.Interfaces;
using Application.Dtos;
using Application.Errors;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class UpdatePost
{
    public class UpdatePostCommand : IRequest<PostDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
    }
    public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<User> _userManager;

        public UpdatePostHandler(IPostRepository postRepository,DataContext context, IMapper mapper, IUserAccessor userAccessor, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
            _userManager = userManager;
        }
        public async Task<PostDto> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = _postRepository.FilterOne(x => x.Id == request.Id);

            if (post is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Post Not Found");
            }
            var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());
            if(user.Id != post.User.Id) throw new RestException(System.Net.HttpStatusCode.Forbidden, "You are not allowed to update this post");

            post.Image = request.Image;
            post.Title = request.Title;
            post.Content = request.Content;

            _postRepository.Update(post);

            var result = await _postRepository.Complete() < 0;
            if (result)
            {
                throw new Exception("An Error Occurred while updating");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}
