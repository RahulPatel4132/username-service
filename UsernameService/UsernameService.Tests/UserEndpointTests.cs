using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsernameService.Api.Data;
using UsernameService.Api.Models;
using Xunit;

namespace UsernameService.Tests;

public class UserEndpointTests
{
    private static AppDbContext InMemoryDb()
    {
        var opts = new DbContextOptionsBuilder<AppDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .Options;
        return new AppDbContext(opts);
    }

    [Fact]
    public async Task Rejects_Duplicate_Usernames()
    {
        using var db = InMemoryDb();
        db.Users.Add(new User { AccountId = Guid.NewGuid(), Username = "Rahul Patel" });
        await db.SaveChangesAsync();

        bool exists = await db.Users.AnyAsync(u => u.Username == "Rahul Patel");
        Assert.True(exists);
    }
}
