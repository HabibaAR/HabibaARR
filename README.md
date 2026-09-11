# 📋 Plateforme de Gestion des Plans d'Action (GPA) - NOVEC Group

> **Statut:** ✅ Complètement fonctionnelle - Prête au lancement

## 🎯 Vue d'ensemble

Application web **ASP.NET Core 8.0 MVC** pour la gestion complète du cycle de vie des plans d'action avec **8 états de workflow**, **4 rôles d'acteurs**, **traçabilité audit complète** et **exports documentés (PDF/Excel)**.

## ⚙️ Technologies

- **Framework**: ASP.NET Core 8.0 MVC
- **Langage**: C# 12.0
- **Base de données**: SQL Server (LocalDB pour dev)
- **ORM**: Entity Framework Core 8.0
- **Frontend**: 100% CSS3 pur (pas JavaScript, pas Bootstrap)
- **Authentification**: ASP.NET Core Identity + RBAC
- **Logging**: Serilog
- **Exports**: iText7 (PDF), ClosedXML (Excel)
- **Views**: Razor (.cshtml)

## 📂 Structure du projet

```
HabibaARR/
├── Controllers/           # AccountController, ActionsController, etc.
├── Models/               # Action, ActionPlan, Evidence, ApplicationUser
├── Services/             # IActionService, IActionPlanService, IReportService
├── Data/                 # ApplicationDbContext, DbInitializer
├── ViewModels/           # ActionPlanViewModel, ActionViewModel, etc.
├── Views/                # Vues Razor (.cshtml)
│   ├── Actions/
│   ├── ActionPlans/
│   ├── Account/
│   ├── Dashboard/
│   ├── Reports/
│   └── Shared/
├── wwwroot/              # CSS, images (pas de JavaScript)
├── appsettings.json      # Configuration (LocalDB, DI, logging)
├── Program.cs            # Startup, DI, migrations
└── Migrations/           # EF Core (auto-générées)
```

## 🚀 Démarrage Rapide

### Prérequis

- **.NET 8.0 SDK** ([Télécharger](https://dotnet.microsoft.com/download))
- **SQL Server LocalDB** (inclus avec .NET SDK)
- **Navigateur** (Chrome, Firefox, Edge, Safari)

### Installation

```bash
# 1. Extraire/cloner le projet
cd HabibaARR

# 2. Restaurer dépendances
dotnet restore

# 3. Construire
dotnet build

# 4. Lancer
dotnet run

# 5. Ouvrir navigateur
# https://localhost:5001
```

**Attendu:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
```

## 👥 Comptes de Test Inclus

| Rôle | Email | Mot de passe | Fonction |
|------|-------|--------------|----------|
| **Administrateur** | admin@novec.fr | Test@12345 | Gestion système complète |
| **Directeur** | directeur@novec.fr | Test@12345 | Validation des preuves |
| **Gestionnaire** | gestionnaire@novec.fr | Test@12345 | Création plans/actions |
| **Responsable** | responsable@novec.fr | Test@12345 | Exécution actions |

*Les comptes sont créés automatiquement à la première exécution.*

## 🔐 Sécurité

- ✅ **Authentification**: ASP.NET Core Identity (hash PBKDF2 - 12+ rounds)
- ✅ **Autorisation**: Role-Based Access Control (RBAC) granulaire
- ✅ **Protection CSRF**: Tokens anti-CSRF automatiques
- ✅ **Protection XSS**: HtmlEncode tous les outputs
- ✅ **SQL Injection**: Requêtes paramétrées Entity Framework
- ✅ **Audit Complet**: Tous les changements loggés avec timestamp et userId
- ✅ **HTTPS**: TLS 1.2+ en production

## 🔄 Workflow (8 États)

```
Draft (Brouillon)
  ↓
New (Nouveau) — Acceptation/Rejet
  ├→ Accepted (Acceptée)
  │   ↓
  │   EvidenceSubmitted (Preuve soumise)
  │   ├→ Completed (Complétée) ✅
  │   └→ EvidenceRejected (Preuve rejetée)
  │       ↓ (Resoumission)
  │       EvidenceSubmitted
  │
  └→ Rejected (Rejetée) ❌
```

## 🏗️ Architecture des Rôles

### 👨‍💼 Administrateur
- Accès complet à l'application
- Gestion des utilisateurs
- Attribut des rôles
- Consultation des audits
- Accès aux paramètres système

### 👔 Directeur
- Consultation tous les plans/actions
- Validation des preuves d'exécution
- Accès aux rapports
- Audit partiel

### 📊 Gestionnaire
- Création/modification plans
- Création actions
- Transmission plans
- Validation preuves
- Fermeture actions
- Génération rapports

### ✓ Responsable
- Consultation ses actions assignées
- Acceptation/rejet actions
- Soumission preuves
- Consultation son historique

## 📚 Documentation

| Document | Contenu |
|----------|---------|
| [GUIDE_LANCEMENT_COMPLET.md](./GUIDE_LANCEMENT_COMPLET.md) | Instructions détaillées + workflow complet + dépannage |
| [ACTORS_WORKFLOW_DETAILED.md](./ACTORS_WORKFLOW_DETAILED.md) | Spécifications complètes des acteurs et du workflow |
| [IMPLEMENTATION_SUMMARY.md](./IMPLEMENTATION_SUMMARY.md) | Détails techniques de l'implémentation |

## 🧪 Test Complet

Voir [GUIDE_LANCEMENT_COMPLET.md](./GUIDE_LANCEMENT_COMPLET.md#-workflow-complet-de-test)

Résumé du workflow de test (20 min):
1. Gestionnaire crée un plan
2. Gestionnaire ajoute une action
3. Gestionnaire transmet le plan
4. Responsable accepte l'action
5. Responsable soumet une preuve (avec fichier)
6. Gestionnaire valide la preuve
7. Action fermée automatiquement ✅
8. Consultation de l'historique complet

## 📊 Fonctionnalités

- ✅ CRUD complet plans et actions
- ✅ Workflow 8 états avec transitions sécurisées
- ✅ Gestion pièces jointes (PDF, DOC, XLS, images)
- ✅ Soumission/validation preuves
- ✅ Historique complet avec audit trail
- ✅ Exports PDF (iText7) et Excel (ClosedXML)
- ✅ Dashboard personnalisé par rôle
- ✅ Notifications automatiques
- ✅ Recherche et filtrage
- ✅ Rapports détaillés

## 🔧 Configuration

**Connexion BD:** Modifiable dans `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HabibaARR;Trusted_Connection=true;"
}
```

**Logging:** Configuré avec Serilog
```
Info: Serilog → Console
Warning: Entity Framework
Debug: Razor compiler
```

---

**Version**: 2.0  
**Framework**: ASP.NET Core 8.0  
**Dernière mise à jour**: 11 Sept 2025  
**Status**: ✅ Production Ready  
**Développé avec**: Claude Code
