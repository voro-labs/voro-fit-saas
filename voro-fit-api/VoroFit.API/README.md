dotnet ef migrations add Initial --project VoroFit.Infrastructure --startup-project VoroFit.API --output-dir Migrations
dotnet ef migrations remove --project VoroFit.Infrastructure --startup-project VoroFit.API
dotnet ef database update --project VoroFit.Infrastructure --startup-project VoroFit.API