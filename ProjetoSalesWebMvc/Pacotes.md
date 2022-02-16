
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add "name"
dotnet ef database update (criarBanco)
dotnet ef migrations remove
dotnet ef migrations script -o./script.sql(gera um script das migrations)
