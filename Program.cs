using ApiRest.Data;
using ApiRest.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conection = builder.Configuration.GetConnectionString("Conection");
builder.Services.AddDbContext<Context>(
    options => options.UseMySql(conection,
                        ServerVersion.AutoDetect(conection))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();
