using Application.Interfaces;
using Doiman;
using Persistence;

namespace Infrastruture.Services;

public class PostRepository:IPostRepository
{
    private readonly DataContext _context;

    public PostRepository(DataContext context)
    {
        _context = context;
    }
    public void Add(Post post)
    {
        _context.Set<Post>().Add(post);
    }

    public Post Update(Post post)
    {
        throw new NotImplementedException();
    }

    public Post Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Post> ListAll()
    {
        throw new NotImplementedException();
    }

    public Post ListById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
}