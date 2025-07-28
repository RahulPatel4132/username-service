# UsernameService

A minimal **.NET 6** microservice that ensures every player in a game world has a **unique** `username ⇄ accountId (Guid)` pair.  
Built with:

| Layer | Tech |
|-------|------|
| API   | ASP.NET Core 6 (minimal-API) |
| ORM   | Entity Framework Core 6 |
| DB    | SQLite (file‐based) |
| Docs  | Swagger / OpenAPI (Swashbuckle) |
| Tests | xUnit + EF-Core In-Memory |     


## 🚀 Quick Start

```bash
git clone https://github.com/RahulPatel4132/username-service.git

# solution folder
cd username-service/UsernameService     

# pull NuGet packages
dotnet restore                               

# create users.db
dotnet ef database update --project UsernameService.Api  

#Run Project
dotnet run --project UsernameService.Api

# Run Tests
dotnet test


Browse **[https://localhost:7001/swagger](https://localhost:7001/swagger)** (or the HTTP port displayed in the console).  

You’ll see two endpoints:

| `GET`  | `/api/username/{name}`| Validate username syntax **and** check whether the name is already taken.   |
| `POST` | `/api/username`       | Persist a new `username + accountId`, replacing any prior username for that account. |

