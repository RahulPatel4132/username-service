namespace UsernameService.Api.Endpoints;

using Microsoft.EntityFrameworkCore;
using UsernameService.Api.Data;    
using UsernameService.Api.Models;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        // GET /api/username/{name}
        app.MapGet("/api/username/{name}", async (string name, AppDbContext db) =>
        {
            bool syntaxOk = System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-zA-Z0-9]{6,30}$");
            if (!syntaxOk) return Results.BadRequest("Invalid format");

            bool exists = await db.Users.AnyAsync(u => u.Username == name);
            return exists ? Results.Conflict("Username already taken")
                          : Results.Ok("Username available");
        });

        // POST /api/username
        app.MapPost("/api/username", async (UserDto dto, AppDbContext db) =>
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.Username, "^[a-zA-Z0-9]{6,30}$"))
                return Results.BadRequest("Invalid username format");

            using var tx = await db.Database.BeginTransactionAsync();

            var existing = await db.Users.SingleOrDefaultAsync(u => u.AccountId == dto.AccountId);
            if (existing != null) db.Users.Remove(existing);

            if (await db.Users.AnyAsync(u => u.Username == dto.Username))
                return Results.Conflict("Username already taken");

            db.Users.Add(new User { AccountId = dto.AccountId, Username = dto.Username });
            await db.SaveChangesAsync();
            await tx.CommitAsync();

            return Results.Ok("Username stored");
        });
    }

    public record UserDto(Guid AccountId, string Username);
}
