> ❔ Need a local sql server environment, [see create an instance of the mssql container locally in docker](LocalMsSqlDocker.md)

### Variables
```powershell
$conn = "Server=127.0.0.1;Database=<dbname>;User Id=sa;Password=P@ssword!;"
$project = ""
$env:DESIGN_CONTEXT = $conn
```

### Creating the db
  1. Open a terminal
  2. Run the following commands
```powershell
dotnet ef database drop -f -v --project $project
dotnet ef database update -v --project $project
dotnet run -p $project
```

### Creating a new migration
  1. Create the necessary model in the domain layer
  2. Add DbSet<YourNewEntity> in the DbContext
  3. Any static data should be set up in the OnModelCreating method found in the DbContext
  4. Any dev seed data should be set up in ProteusDevSeed
  5. Open PMC
  6. Run the following commands
```powershell
  dotnet ef migrations add <migration_name> --project $project
```

  - _Note: migration_name should succinctly describe what the migration is responsible for ie: add_tbl_Users or mod_tbl_Users_AddAddressData._  
  - **Critical: Review the migration after creation to ensure it is doing what you intended**
  a. If the migration is acceptable, run the following commands to recreate your local db
```powershell
dotnet ef database drop -f -v --project $project
dotnet ef database update -v --project $project
dotnet run -p $project
```
  b. If the migration is not acceptable, run the following commands to remove the new migration, make the appropriate changes, and run the commands to create a new migration
```powershell
dotnet ef migrations remove --project $project
```

### Remove an existing migration
If you run into an issue with an existing migration and it has already been applied...
  1. Open Package Manager Console
  2. Run the following commands
```powershell
  dotnet ef database update <target> --project $project
```
  - Note: <target> is the name of the last migration you want run. For example, if the migrations folder has the following migrations:
```powershell
  2020010112345_initial.cs
  2020020402342_secondMigration.cs
  2020020520393_badMigration.cs
```
< target > would be '2020020402342_secondMigration' to remove '2020020520393_badMigration'

  3. Run `dotnet ef migrations remove --project $project` to delete the unwanted migration
