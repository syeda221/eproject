dotnet tool install -g dotnet-aspnet-codegenerator
dotnet build

Write-Host "Scaffolding Galleries..."
dotnet aspnet-codegenerator controller -name GalleriesController -m Gallery -dc ApplicationDbContext --relativeFolderPath Areas/Admin/Controllers --useDefaultLayout -f

Write-Host "Scaffolding NGOs..."
dotnet aspnet-codegenerator controller -name NGOsController -m Ngo -dc ApplicationDbContext --relativeFolderPath Areas/Admin/Controllers --useDefaultLayout -f

Write-Host "Scaffolding Causes..."
dotnet aspnet-codegenerator controller -name CausesController -m Cause -dc ApplicationDbContext --relativeFolderPath Areas/Admin/Controllers --useDefaultLayout -f

Write-Host "Scaffolding Programmes..."
dotnet aspnet-codegenerator controller -name ProgrammesController -m Programme -dc ApplicationDbContext --relativeFolderPath Areas/Admin/Controllers --useDefaultLayout -f

Write-Host "Scaffolding Queries..."
dotnet aspnet-codegenerator controller -name QueriesController -m Query -dc ApplicationDbContext --relativeFolderPath Areas/Admin/Controllers --useDefaultLayout -f

Write-Host "Applying [Area] and [Authorize] attributes..."
Get-ChildItem -Path "Areas\Admin\Controllers" -Filter "*.cs" | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    if ($content -notmatch '\[Area\("Admin"\)\]') {
        $content = $content -replace 'public class', "[Microsoft.AspNetCore.Mvc.Area(`"Admin`")]`n    [Microsoft.AspNetCore.Authorization.Authorize]`n    public class"
        Set-Content -Path $_.FullName -Value $content
    }
}

Write-Host "Done formatting controllers!"
