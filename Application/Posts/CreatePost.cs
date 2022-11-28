//vai ser usada para create novos posts

using System.Net;
using Application.Interfaces;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Doiman;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class CreatePost
    {
        public class CreatePostCommand :IRequest<PostDto>
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Image { get; set; }
        }

        public class CreatePostValidator :AbstractValidator<CreatePostCommand>
        {
            public CreatePostValidator()
            {
                RuleFor(x=>x.Image).NotEmpty();
                RuleFor(x=>x.Title).NotEmpty();
            }
        }
        public  class CreatePostCommandHandle :IRequestHandler<CreatePostCommand,PostDto>
        {
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IPostRepository _postRepository;
            private readonly IUserAccessor _userAccessor;
            

            public CreatePostCommandHandle(IMapper mapper, UserManager<User> userManager, IPostRepository postRepository, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _userManager = userManager;
                _postRepository = postRepository;
                _userAccessor = userAccessor;
            }
            public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(_userAccessor.GetCurrentUserId());
                if (user is null) throw new RestException(HttpStatusCode.NotFound, "User not found");

                var postFound=_postRepository.FilterOne(post1 => post1.Title == request.Title);//se nao existe vai retornar null

                if (postFound != null)
                {
                    throw new RestException(HttpStatusCode.Conflict, "The post already exists");
                }

                var post = new Post()
                {
                    Creationdate = DateTimeOffset.UtcNow,
                    Image = request.Image,
                    Title = request.Title,
                    Content = request.Content,
                    User = user
                };
                
               
               _postRepository.Add(post);
               var result = await _postRepository.Complete() < 0;
               if (result)
               {
                   throw new Exception("AN ERROR OCCURRED");
               }

               return _mapper.Map<Post, PostDto>(post);
            }
        }
    }
}