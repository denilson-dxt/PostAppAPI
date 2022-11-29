using System.Linq.Expressions;
using Application.Interfaces;
using Doiman;
using Microsoft.EntityFrameworkCore;
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

    public void Update(Post post)
    {
        _context.Set<Post>().Update(post);
    }

    public void Delete(Post post)
    {
        _context.Set<Post>().Remove(post);
    }

    public List<Post> ListAll()
    {
        return _context.Set<Post>().Include(p=>p.User).ToList();
    }

    public Post ListById(int id)
    {
       return _context.Set<Post>().Find(id);
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public Post FilterOne(Expression<Func<Post, bool>> query = null)
    {
        return _context.Post.FirstOrDefault(query);
    }
}