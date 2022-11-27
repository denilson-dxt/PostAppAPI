using Domain;
using MediatR;
using Persistance;

namespace Application.Posts;

public class CreatePost
{
    public class CreatePostCommand : IRequest<Post>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }

    public class CreatePostHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly DataContext _context;

        public CreatePostHandler(DataContext context)
        {
            _context = context;
        }
        public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post()
            {
                Content = request.Content,
                Image = request.Image
            };
            await _context.Posts.AddAsync(post);
            var result = await _context.SaveChangesAsync(cancellationToken) < 0;
            if (result)
            {
                throw new Exception("An Error Occurred");
            }

            return post;
        }
    }
}