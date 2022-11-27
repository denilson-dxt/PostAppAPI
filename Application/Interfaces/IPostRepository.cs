using Doiman;

namespace Application.Interfaces;

public interface IPostRepository
{
    void Add(Post post);
    /*void Update(Post post);
    void Delete(int id);
    void ListAll();
    Post ListById(int id);*/
    Task<int> Complete();

}