using API.Extensions;
using API.Middlewares;
using Application.Helpers.MappingProfiles;
using Application.Interfaces;
using Application.Posts;
using Application.Users;
using Doiman;
using FluentValidation.AspNetCore;
using Infrastruture.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

//usamos para definir servico de DB
builder.Services.AddDbContext<DataContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

//So fazemos uma vez
builder.Services.AddMediatR(typeof(CreatePost.CreatePostCommand).Assembly);//configuracao do mediatr


// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<CreateUser.CreateUserCommand>());

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Load();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyCors");
app.MapControllers();
app.Run();


 void  Load()
{
    using (var scoped = app.Services.CreateScope())
    {
        var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
         SeedData.SeedAsync(userManager);
    }
}