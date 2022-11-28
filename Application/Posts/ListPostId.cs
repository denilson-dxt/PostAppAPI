using System.Net;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Doiman;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts;

public class ListPostId
{
    public class ListPostIdQuery :IRequest<PostDto>
    {
        public int Id { get; set; }
       
    }

    public class ListPostByIdHandler : IRequestHandler<ListPostIdQuery, PostDto>
    {
        private readonly DataContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public ListPostByIdHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(ListPostIdQuery request, CancellationToken cancellationToken)
        {
            var post = _postRepository.ListById(request.Id);

            if (post == null)
            {
                throw new RestException(HttpStatusCode.NotFound,"Error, Post doesnt exist!!");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}