# Plateforme de Gestion des Plans d'Action - NOVEC Group

## Vue d'ensemble

Application web ASP.NET Core MVC pour la gestion et le suivi professionnel des plans d'action de NOVEC Group.

## Technologies

- **Framework**: ASP.NET Core 7.0+
- **Language**: C#
- **Base de données**: SQL Server
- **ORM**: Entity Framework Core
- **Frontend**: HTML5, CSS3, Bootstrap 5, JavaScript
- **Views**: Razor

## Structure du projet

```
├── Controllers/           # Contrôleurs ASP.NET MVC
├── Models/               # Entités métier
├── ViewModels/           # Objets de transfert de données pour les vues
├── Services/             # Logique métier
├── Data/                 # Context et migrations EF Core
├── Views/                # Vues Razor
├── wwwroot/              # Assets statiques
│   ├── css/
│   ├── js/
│   └── images/
├── appsettings.json      # Configuration
└── Program.cs            # Configuration de l'application
```

## Installation et configuration

### Prérequis

- .NET 7.0 SDK
- SQL Server 2019+
- Visual Studio 2022 ou VS Code

### Configuration

1. Cloner le repository
2. Modifier `appsettings.json` avec la chaîne de connexion SQL Server
3. Exécuter les migrations: `dotnet ef database update`
4. Démarrer l'application: `dotnet run`

## Modèle de sécurité

- **Authentification**: ASP.NET Core Identity
- **Autorisation**: Rôles (Admin, Directeur, Gestionnaire, Responsable)
- **Validation**: Côté serveur et client
- **Protection CSRF**: Enabled par défaut

## Architecture des rôles

- **ADMIN**: Accès complet, gestion des utilisateurs
- **DIRECTEUR**: Dashboard global, exports
- **GESTIONNAIRE**: Création des plans, gestion des actions
- **RESPONSABLE**: Traitement des actions assignées

## Démarrage rapide

```bash
# Restaurer les dépendances
dotnet restore

# Appliquer les migrations
dotnet ef database update

# Démarrer l'application
dotnet run
```

## Documentation

Voir `ARCHITECTURE.md` pour la documentation technique complète.

---

**Version**: 1.0.0  
**Développé avec Claude Code**  
**Date**: 2026-09-09
