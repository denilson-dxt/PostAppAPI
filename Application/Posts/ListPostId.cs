using System.Net;
using Application.Dtos;
using Application.Errors;
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
        private readonly IMapper _mapper;

        public ListPostByIdHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDto> Handle(ListPostIdQuery request, CancellationToken cancellationToken)
        {
            var post =
                await _context.Post.Include(p=>p.User).FirstOrDefaultAsync(post1 =>
                    post1.Id == request.Id); //se nao existe vai retornar null

            if (post == null)
            {
                throw new RestException(HttpStatusCode.NotFound,"Error, Post doesnt exist!!");
            }

            return _mapper.Map<PostDto>(post);
        }
    }
}