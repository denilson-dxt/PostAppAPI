using System.Threading.Tasks;
using Aplication.Posts;
using Application.Dtos;
using Application.Posts;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//vai interragir com a camada de aplicaao
namespace API.Controllers
{
    [Authorize]
    public class PostsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //estamos a criar o andpoint para post
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePosts(CreatePost.CreatePostCommand command)
        {
           return  await _mediator.Send(command);//retorna um objecto com todos os posts
        }
        
        //and point para listar as posts
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<PostDto>>> GetAllPost()
        {
            //ira buscar a informacao a base de dados
            return await _mediator.Send(new ListPosts.ListPostsQuery());
        }
        
        
        //andPoint para buscar um post de acordo com ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDto>> GetPostById(int id)
        {
            //ira buscar a informacao a base de dados
            return await _mediator.Send(new ListPostId.ListPostIdQuery{Id=id});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> UpdatePost(UpdatePost.UpdatePostCommand command, int id)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostDto>> DeletePost(DeletePost.DeletePostCommand command, int id)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
    }
}