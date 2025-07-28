using Microsoft.EntityFrameworkCore;
using UsernameService.Api.Data;  
using UsernameService.Api.Endpoints; 
var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=users.db"));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Adding this to auto-migrate DB at startup 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
}
app.UseSwagger();
app.UseSwaggerUI();

app.MapUserEndpoints();   // still call our extension
app.Run();
