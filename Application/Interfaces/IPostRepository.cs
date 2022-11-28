using Doiman;

namespace Application.Interfaces;

public interface IPostRepository
{
    void Add(Post post);
    void Update(Post post);
    void Delete(Post post);
    List<Post> ListAll();
    Post ListById(int id);
    Task<int> Complete();

}