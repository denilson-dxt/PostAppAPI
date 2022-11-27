using Application.Posts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PostsController : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(CreatePost.CreatePostCommand command)
    {
        return await Mediator.Send(command);
    }
}