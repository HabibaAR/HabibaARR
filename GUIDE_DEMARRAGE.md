# Guide de Démarrage Rapide - HabibaARR

## Installation et configuration

### Prérequis système
- **.NET 7.0 SDK** ou supérieur
- **SQL Server 2019** ou supérieur (LocalDB accepté pour développement)
- **Visual Studio 2022** ou **VS Code**
- **Git**

### Installation locale

#### 1. Cloner le repository
```bash
git clone <repo-url>
cd HabibaARR
```

#### 2. Configurer la chaîne de connexion

Modifier `appsettings.json` avec votre SQL Server :

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=HabibaARR;Trusted_Connection=true;"
}
```

**Exemples :**
- **LocalDB** : `Server=(localdb)\mssqllocaldb;Database=HabibaARR;Trusted_Connection=true;`
- **SQL Server Express** : `Server=localhost\SQLEXPRESS;Database=HabibaARR;Trusted_Connection=true;`
- **Azure SQL** : `Server=tcp:server.database.windows.net,1433;Initial Catalog=HabibaARR;...`

#### 3. Restaurer les dépendances
```bash
dotnet restore
```

#### 4. Créer et initialiser la base de données
```bash
# Appliquer les migrations
dotnet ef database update

# Ou avec le CLI EF
dotnet ef database update --project HabibaARR.csproj
```

Les migrations créeront automatiquement :
- Tables des entités métier
- Tables Identity ASP.NET
- Rôles par défaut (ADMIN, DIRECTEUR, GESTIONNAIRE, RESPONSABLE)
- Utilisateurs de test

#### 5. Démarrer l'application
```bash
dotnet run
```

L'app sera accessible à : **https://localhost:5001** ou **http://localhost:5000**

## Identifiants de test

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| Admin | admin@novec.fr | Test@12345 |
| Directeur | directeur@novec.fr | Test@12345 |
| Gestionnaire | gestionnaire@novec.fr | Test@12345 |
| Responsable | responsable@novec.fr | Test@12345 |

## Première connexion et test

### 1. Accéder à l'accueil
Ouvrir : https://localhost:5001/

### 2. Se connecter
Cliquer "Se connecter" et utiliser les identifiants ci-dessus

### 3. Tester le workflow complet

**En tant que GESTIONNAIRE** :
1. Aller à "Plans d'action"
2. Créer un nouveau plan
3. Créer une action dans le plan
4. Assigner à un responsable
5. Transmettre le plan

**En tant que RESPONSABLE** :
1. Aller à "Actions"
2. Voir l'action assignée
3. Consulter les détails
4. Accepter l'action
5. Soumettre une preuve (avec fichier)

**Retour en tant que GESTIONNAIRE** :
1. Aller aux détails de l'action
2. Valider ou rejeter la preuve
3. L'action est clôturée

**Tous** :
1. Consulter le Dashboard
2. Voir les KPI et statistiques
3. Exporter un rapport Excel

## Développement local

### Ouvrir avec Visual Studio 2022
1. Fichier → Ouvrir → Projet/Solution
2. Sélectionner `HabibaARR.csproj`
3. Restaurer les dépendances NuGet (F5 pour lancer)

### Ouvrir avec VS Code
```bash
code .
```
Puis dans le terminal intégré :
```bash
dotnet run
```

### Hot Reload (développement)
Modifier un fichier C# et sauvegarder :
```bash
dotnet watch run
```

### Gestion des migrations EF Core

**Créer une migration** (après modifier un modèle)
```bash
dotnet ef migrations add NomMigration
```

**Appliquer les migrations**
```bash
dotnet ef database update
```

**Voir l'historique**
```bash
dotnet ef migrations list
```

## Structure du projet

```
HabibaARR/
├── Controllers/          # Contrôleurs HTTP
├── Models/              # Entités métier
├── ViewModels/          # Objets pour les vues
├── Services/            # Logique métier
├── Data/                # Context EF et migrations
├── Views/               # Vues Razor
├── wwwroot/             # Assets statiques
│   ├── css/
│   ├── js/
│   └── uploads/         # Fichiers uploadés
├── Program.cs           # Configuration app
├── appsettings.json     # Configuration
├── ARCHITECTURE.md      # Doc technique
└── HabibaARR.csproj     # Définition projet
```

## Configuration Production (TBC - À confirmer)

### Avant le déploiement

1. **Configurer HTTPS** (certificat SSL)
2. **Modifier appsettings.Production.json**
3. **Configurer la base de données** (serveur production)
4. **Configurer les variables d'environnement**
5. **Désactiver les identifiants de test**
6. **Activer le logging avancé**

### Variables d'environnement
```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=<prod-connection-string>
AppSettings__SessionTimeoutMinutes=30
```

### Déploiement sur IIS
```bash
# Publier pour production
dotnet publish -c Release -o ./publish

# Copier le dossier ./publish sur le serveur IIS
# Configurer l'application pool .NET Core
# Vérifier les permissions du dossier uploads/
```

## Dépannage

### Erreur : "Cannot connect to SQL Server"
- Vérifier que SQL Server est running
- Vérifier la chaîne de connexion dans appsettings.json
- Vérifier les permissions d'authentification

### Erreur : "Migrations not applied"
```bash
dotnet ef database update
```

### Erreur : "Port 5001 already in use"
```bash
dotnet run --urls "https://localhost:5002"
```

### Migrations échouées
```bash
# Voir l'état
dotnet ef migrations list

# Revenir à une migration antérieure
dotnet ef database update NomMigration
```

## Logs et debugging

### Voir les logs EF Core
Dans `Program.cs`, activer le logging :
```csharp
.AddLogging(options => options.AddConsole())
```

### Lancer le debugger
Mettre des points d'arrêt dans VS et appuyer sur **F5**

### Logs d'application
Vérifier `/logs` ou la console (Serilog configuré)

## Ressources complémentaires

- [Documentation ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [Identity ASP.NET Core](https://docs.microsoft.com/aspnet/core/security/authentication/identity)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs)

## Support et questions

- Voir `ARCHITECTURE.md` pour la doc technique complète
- Consulter les commentaires de code
- Vérifier les logs de l'application

---

**Bonne utilisation ! 🚀**

Pour toute question ou retour, consulter la documentation technique ou contacter l'équipe de développement.
