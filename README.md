# RecipeMaker2.0

// TODO write a proper readme!!!

// Notes

dotnet ef migrations add OpMigration -c PersistedGrantDbContext -o Migrations/OpDb

dotnet ef migrations add ConfigMigration -c ConfigurationDbContext -o Migrations/ConfigDb

dotnet ef migrations add DbContextMigration -c ApplicationDbContext -o Migrations/AppDb

dotnet ef database update --context PersistedGrantDbContext
dotnet ef database update --context ConfigurationDbContext
dotnet ef database update --context ApplicationDbContext
