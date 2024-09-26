using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Medals_Api.Hubs;

// Connection info stored in appsettings.json
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build(); var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "Hubs",
        builder =>
        {
          builder
              .AllowAnyHeader()
              .AllowAnyMethod()
              // Anonymous origins NOT allowed for web sockets
              .WithOrigins("http://localhost:5173", "https://jgrissom.github.io")
              .AllowCredentials();
        });
});
builder.Services.AddSignalR();
// Register the DataContext service
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(configuration["ConnectionStrings:DefaultSQLiteConnection"]));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "Medals API",
    Version = "v1",
    Description = "Olympic Medals API",
  });
  c.TagActionsBy(api => [api.HttpMethod]);
  c.EnableAnnotations();
});

var app = builder.Build();

app.UseRouting();
app.UseCors("Hubs");

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MedalsHub>("/medalsHub");

app.Run();
