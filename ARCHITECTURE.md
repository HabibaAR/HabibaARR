# Architecture de la Plateforme de Gestion des Plans d'Action

## Vue d'ensemble

Cette plateforme ASP.NET Core MVC est conçue pour gérer et suivre les plans d'action de NOVEC Group de manière professionnelle et sécurisée.

## Architecture générale

### Technologie Stack
- **Framework**: ASP.NET Core 7.0
- **Language**: C#
- **Base de données**: SQL Server
- **ORM**: Entity Framework Core
- **Frontend**: HTML5, CSS3, Bootstrap 5, JavaScript
- **Template Engine**: Razor Views

## Architecture MVC

```
├── Controllers/          # Contrôleurs gerant les requêtes HTTP
├── Models/              # Entités métier (domaine)
├── ViewModels/          # Objets de transfert de données pour les vues
├── Views/               # Vues Razor (présentation)
├── Services/            # Couche métier (logique applicative)
├── Data/                # Contexte et migrations Entity Framework
└── wwwroot/             # Assets statiques (CSS, JS, images)
```

## Modèle de données

### Entités principales

#### ApplicationUser
- Extension du modèle Identity ASP.NET
- Propriétés métier: FirstName, LastName, Position, Department
- Relations: CreatedActionPlans, AssignedActions, ActionLogs

#### ActionPlan
- Contient une ou plusieurs Actions
- Statuts: Draft, Active, Closed, Archived
- Propriétaires: Un gestionnaire (Manager)

#### Action
- Représente une tâche à accomplir
- Statuts: Draft, New, Accepted, Rejected, InProgress, EvidenceSubmitted, EvidenceRejected, Completed
- Priorités: Low, Medium, High, Critical
- Assignée à: Un responsable (Responsible)
- Gérée par: Un gestionnaire (Manager)

#### Evidence
- Preuve de complétude d'une action
- Statuts: Submitted, Approved, Rejected
- Peut contenir plusieurs Attachments
- Soumise et revue par des utilisateurs

#### Attachment
- Pièces jointes aux preuves
- Gestion sécurisée des fichiers
- Métadonnées: FileName, FileType, FileSizeBytes, FilePath

#### ActionLog
- Journal d'audit pour traçabilité
- Enregistre tous les événements d'une action
- EventTypes: Created, Submitted, Accepted, Rejected, etc.

## Flux d'authentification et autorisation

### Authentification
- **Méthode**: ASP.NET Core Identity
- **Stockage**: Base de données SQL Server
- **Sécurité**: Hashage bcrypt des mots de passe

### Autorisation basée sur les rôles

#### Rôles système
1. **ADMIN**: Accès complet à tous les plans et actions
2. **DIRECTEUR**: Accès lecture/consultations des rapports
3. **GESTIONNAIRE**: Création et gestion des plans/actions
4. **RESPONSABLE**: Traitement des actions assignées

### Contrôles d'accès
- Tous les contrôleurs sont protégés par `[Authorize]`
- Vérifications supplémentaires au niveau métier
- Impossibilité de contourner via URL

## Workflow métier

### Cycle de vie d'une action

```
Brouillon (Draft)
    ↓
Nouveau (New) - Transmise par gestionnaire
    ↓
Acceptée (Accepted) - Acceptée par responsable
    ↓ (ou Rejetée - Rejected)
En cours (InProgress)
    ↓
Preuve soumise (EvidenceSubmitted)
    ↓
    ├→ Validée → Complétée
    └→ Rejetée (EvidenceRejected) → Preuve soumise (recommence)
```

## Services métier

### IActionPlanService
- Gestion complète des plans d'action
- Créer, modifier, consulter
- Transition de statut

### IActionService
- Gestion des actions individuelles
- Workflow complet: création, acceptation, rejet, complétude
- Journal d'historique

### IEvidenceService
- Gestion des preuves et pièces jointes
- Upload sécurisé de fichiers
- Validation par gestionnaire

### IDashboardService
- Calcul des métriques KPI
- Analyse par statut et responsable
- Tendances de complétude

### IUserService
- Gestion des utilisateurs
- Vérification des permissions par rôle
- Récupération des utilisateurs par rôle

### IReportService
- Export Excel des actions et plans
- Export PDF des rapports
- Filtrage avant export

## Sécurité

### Mesures implémentées
1. **Authentification forte**: ASP.NET Identity + hachage bcrypt
2. **Autorisation multi-niveaux**: Rôles + vérifications métier
3. **Protection CSRF**: Token anti-forgery automatique
4. **Validation**: Côté serveur et client
5. **Upload sécurisé**: Vérification types/tailles de fichiers
6. **Logging**: Enregistrement de toutes les actions

### Pratiques de sécurité
- Pas de confiance en l'extension des fichiers (vérification MIME)
- Limitations de taille d'upload
- Chemins de fichier abstraits
- Pas d'exposition de chemins système

## Structure des vues

### Layouts
- `_Layout.cshtml`: Layout principal avec sidebar
- `_ViewStart.cshtml`: Configuration par défaut

### Pages publiques
- `Home/Index.cshtml`: Page d'accueil
- `Account/Login.cshtml`: Connexion

### Pages authentifiées
- `Dashboard/Index.cshtml`: Tableau de bord
- `ActionPlans/Index.cshtml`: Liste des plans
- `ActionPlans/Create.cshtml`: Création de plan
- `Actions/Index.cshtml`: Liste des actions
- `Actions/Create.cshtml`: Création d'action
- `Actions/Details.cshtml`: Détail avec journal et preuves

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "chaîne de connexion SQL Server"
  },
  "AppSettings": {
    "MaxUploadSizeMB": 10,
    "AllowedFileExtensions": ["pdf", "docx", "xlsx", ...],
    "SessionTimeoutMinutes": 30,
    "PasswordMinLength": 8,
    "RequireUppercase": true,
    "RequireNumbers": true
  }
}
```

## Migrations Entity Framework

Les migrations sont appliquées automatiquement au démarrage dans `Program.cs`:
```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    DbInitializer.Initialize(db);
}
```

## Performance et optimisations

1. **Include() explicites**: Chargement des relations EF Core
2. **Index sur les clés étrangères**: Amélioration des requêtes
3. **Pagination**: Sur les listes longues
4. **Caching**: Possible pour les données statiques
5. **Async/Await**: Opérations asynchrones

## Extensibilité

### Points d'extension
1. **Services**: Ajouter facilement de nouveaux services
2. **Controllers**: Ajouter de nouveaux contrôleurs pour nouvelles entités
3. **ViewModels**: Créer des mappings spécialisés
4. **Reports**: Ajouter de nouveaux types d'exports

### Propositions techniques futures
- Intégration Audit/Log avancée
- Notifications par email
- Workflows configurables par l'admin
- API REST pour intégration externe
- Mobile app avec WebAPI
- Analytics avancées
- Cache distribué Redis

## Déploiement

### Prérequis
- .NET 7.0 Runtime
- SQL Server 2019+
- IIS 8.0+ (si déploiement Windows)

### Étapes
1. Build: `dotnet build`
2. Publish: `dotnet publish -c Release`
3. Configurer connection string
4. Déployer sur serveur

## Support et maintenance

- **Logging**: Serilog configuré pour console
- **Monitoring**: Points de log stratégiques
- **Backup**: Plan de sauvegarde SQL Server
- **Mises à jour**: Patches .NET/EF Core réguliers

---

**Version**: 1.0.0  
**Date**: 2026-09-09  
**Développé avec**: Claude Code
