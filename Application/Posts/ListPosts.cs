using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class ListPosts
{
    public class ListPostsQuery : IRequest<List<PostDto>>
    {
    }

    public class ListPostsQueryHabdler : IRequestHandler<ListPostsQuery, List<PostDto>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public ListPostsQueryHabdler(IPostRepository postRepository, IMapper mapper)
        
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> Handle(ListPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = _postRepository.ListAll();
            return _mapper.Map<List<PostDto>>(posts);
        }
    }
}