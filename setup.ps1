# ========================================
# GPA NOVEC - Setup Script (Windows PowerShell)
# Exécuter: powershell -ExecutionPolicy Bypass -File setup.ps1
# ========================================

Write-Host "🚀 GPA NOVEC - Configuration Automatique" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Vérifier .NET
Write-Host "✓ Vérification de .NET 8.0..." -ForegroundColor Yellow
$dotnetVersion = dotnet --version 2>$null
if ($dotnetVersion -match "8\.") {
    Write-Host "  ✅ .NET 8.0 trouvé: $dotnetVersion" -ForegroundColor Green
} else {
    Write-Host "  ❌ .NET 8.0 non trouvé!" -ForegroundColor Red
    Write-Host "  📥 Téléchargez depuis: https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Yellow
    exit 1
}

# Vérifier SQL Server LocalDB
Write-Host ""
Write-Host "✓ Vérification de SQL Server LocalDB..." -ForegroundColor Yellow
$sqlCheck = sqllocaldb info mssqllocaldb 2>$null
if ($sqlCheck) {
    Write-Host "  ✅ SQL Server LocalDB trouvé" -ForegroundColor Green
} else {
    Write-Host "  ⚠️  SQL Server LocalDB non trouvé (optionnel)" -ForegroundColor Yellow
    Write-Host "  💡 Vous pouvez utiliser SQL Server Express" -ForegroundColor Yellow
}

# Restaurer les dépendances
Write-Host ""
Write-Host "✓ Restauration des dépendances NuGet..." -ForegroundColor Yellow
dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "  ❌ Erreur lors de la restauration!" -ForegroundColor Red
    exit 1
}
Write-Host "  ✅ Dépendances restaurées" -ForegroundColor Green

# Créer la base de données
Write-Host ""
Write-Host "✓ Création de la base de données..." -ForegroundColor Yellow
dotnet ef database update
if ($LASTEXITCODE -ne 0) {
    Write-Host "  ⚠️  Erreur lors de la création de la base" -ForegroundColor Yellow
    Write-Host "  🔧 Essayez: dotnet ef database drop -f; dotnet ef database update" -ForegroundColor Yellow
} else {
    Write-Host "  ✅ Base de données créée avec succès" -ForegroundColor Green
}

# Générer le certificat HTTPS
Write-Host ""
Write-Host "✓ Configuration du certificat HTTPS..." -ForegroundColor Yellow
dotnet dev-certs https --clean 2>$null
dotnet dev-certs https --trust 2>$null
Write-Host "  ✅ Certificat HTTPS configuré" -ForegroundColor Green

# Résumé
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "✅ CONFIGURATION TERMINÉE" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "📌 Prochaines étapes:" -ForegroundColor Cyan
Write-Host "  1. Ouvrir VSCode: code ." -ForegroundColor White
Write-Host "  2. Démarrer l'app: dotnet run" -ForegroundColor White
Write-Host "  3. Naviguer vers: https://localhost:5001" -ForegroundColor White
Write-Host ""
Write-Host "👤 Identifiants de test:" -ForegroundColor Cyan
Write-Host "  Email: admin@novec.fr" -ForegroundColor White
Write-Host "  Mot de passe: Test@12345" -ForegroundColor White
Write-Host ""
Write-Host "📖 Documentation: Voir GUIDE_VSCODE.md" -ForegroundColor Cyan
Write-Host ""
