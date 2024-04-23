Param(
   [string] $Server = "(localdb)\MSSQLLocalDb",
   [string] $Database = "MovieDB"
)

# This script requires the SQL Server module for PowerShell.
# The below commands may be required.

# To check whether the module is installed.
# Get-Module -ListAvailable -Name SqlServer;

# Install the SQL Server Module
# Install-Module -Name SqlServer -Scope CurrentUser

$CurrentDrive = (Get-Location).Drive.Name + ":"

Write-Host ""
Write-Host "Rebuilding database $Database on $Server..."

<#
   If on your local machine, you can drop and re-create the database.
#>
& ".\Scripts\DropDatabase.ps1" -Database $Database
& ".\Scripts\CreateDatabase.ps1" -Database $Database

<#
   If on the department's server, you don't have permissions to drop or create databases.
   In this case, maintain a script to drop all tables.
#>
#Write-Host "Dropping tables..."
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Tables\DropTables.sql"

#Write-Host "Creating schema..."
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\Tables.sql"

Write-Host "Creating tables..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\Tables.sql"

Write-Host "Stored procedures..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\AggQueries\GetGenreRanks.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\AggQueries\GetSalesPerHourOfTheDay.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\AggQueries\GetTopTheaters.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\AggQueries\MovieStatisticsOverGivenPeriod.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\CheckLoginStatus.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\FilterMovies.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\FilterShowtimes.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\GetTicket.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\LoginProcedure.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\LogoutProcedure.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\LogoutAllProcedure.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\RetrieveActors.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\RetrieveDirectors.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\RetrieveGenres.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\RetrieveMovies.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\SQLProcedures\RetrieveTheaters.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\TableManagementProcedures\ManageActor.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\TableManagementProcedures\ManageDirector.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\TableManagementProcedures\ManageMovie.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\TableManagementProcedures\ManageMovieShowtime.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\TableManagementProcedures\ManageTheater.sql"

Write-Host "Inserting data..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\allInsertStatements.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "MovieDBSqlData\UserAndTicketPurchaseInserts.sql"

Write-Host "Rebuild completed."
Write-Host ""

Set-Location $CurrentDrive
