using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddKeyedSingleton<IPeopleServices, PeopleServices>("peopleServices");
builder.Services.AddScoped<IPostsService, PostsService>();

//HttpClient for consume REST.
builder.Services.AddHttpClient<IPostsService, PostsService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});

//entity framework
builder.Services.AddDbContext<StoreContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")); });

//validators
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting(); // Habilitar el enrutamiento para controladores.

// Mapear controladores.
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Busca y registra los controladores.
});

app.Run();
