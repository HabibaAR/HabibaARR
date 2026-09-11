# 🚀 GUIDE DE DÉPLOIEMENT - GPA NOVEC

## Plateforme de Gestion des Plans d'Action - Version Finale

---

## 📋 CONTENU

1. [Prérequis](#prérequis)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Démarrage](#démarrage)
5. [Accès Initial](#accès-initial)
6. [Fonctionnalités](#fonctionnalités)
7. [Architecture](#architecture)
8. [Dépannage](#dépannage)

---

## 🔧 Prérequis

### Système d'Exploitation
- Windows 10+ / macOS 10.15+ / Linux (Ubuntu 20.04+)

### Logiciels Requis
- **.NET 8.0 SDK** - [Télécharger](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server LocalDB** (Windows) ou SQL Server Express
  - Windows: Inclus avec Visual Studio ou téléchargeable séparément
  - macOS/Linux: SQL Server Docker (optionnel pour dev)
- **Git** - [Télécharger](https://git-scm.com/)

### Optionnel
- **Visual Studio 2022** ou **Visual Studio Code**
- **SQL Server Management Studio (SSMS)**

---

## 📥 Installation

### 1. Cloner le Projet

```bash
git clone https://github.com/HabibaAR/HabibaARR.git
cd HabibaARR
```

### 2. Vérifier .NET Installation

```bash
dotnet --version
```

Doit afficher: `8.0.xxx`

### 3. Restaurer les Dépendances

```bash
dotnet restore
```

### 4. Créer la Base de Données

```bash
dotnet ef database update
```

Cela va:
- Créer la base de données locale
- Appliquer toutes les migrations
- Semer les données de test

---

## ⚙️ Configuration

### Fichier appsettings.json

Vérifier/modifier les paramètres:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HabibaARR;Trusted_Connection=true;"
  },
  "AppSettings": {
    "SessionTimeoutMinutes": 60,
    "PasswordMinLength": 8,
    "RequireUppercase": true,
    "RequireNumbers": true
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Variables d'Environnement (Optionnel)

```bash
# Linux/macOS
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=https://localhost:5001

# Windows PowerShell
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:ASPNETCORE_URLS="https://localhost:5001"
```

---

## ▶️ Démarrage

### Mode Développement

```bash
dotnet run
```

Sortie attendue:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to exit.
```

### Mode Production

```bash
dotnet run --configuration Release
```

---

## 🔐 Accès Initial

### URL de l'Application
```
https://localhost:5001
```

### Identifiants de Test Fournis

#### Administrateur
- **Email:** admin@novec.fr
- **Mot de passe:** Test@12345
- **Permissions:** Accès total, gestion des utilisateurs, tous les rapports

#### Gestionnaire
- **Email:** gestionnaire@novec.fr
- **Mot de passe:** Test@12345
- **Permissions:** Créer/gérer plans et actions, valider preuves, exporter rapports

#### Responsable
- **Email:** responsable@novec.fr
- **Mot de passe:** Test@12345
- **Permissions:** Accepter/rejeter actions, soumettre preuves, suivi personnel

#### Directeur
- **Email:** directeur@novec.fr
- **Mot de passe:** Test@12345
- **Permissions:** Accès au dashboard global, rapports

---

## ✨ Fonctionnalités Disponibles

### Page d'Accueil Publique
- Présentation institutionnelle
- Affichage des fonctionnalités principales
- Processus de gestion en 6 étapes
- Liens connexion/accueil

### Authentification
- **Connexion:** Email + Mot de passe + "Se souvenir de moi"
- **Inscription:** Formulaire avec validation forte des mots de passe
- **Gestion des Rôles:** Contrôlée par administrateur uniquement

### Tableau de Bord
- **Cartes KPI:** Total actions, complétées, en cours, en retard
- **Statut des Actions:** Distribution visuelle par statut
- **Taux de Clôture:** Indicateur avec barre de progression
- **Avancement par Statut:** Barres de progression détaillées
- **Résumé des Plans:** Table récapitulative

### Gestion des Plans d'Action
- **Créer:** Nouveau plan avec dates et description
- **Consulter:** Liste avec statut et nombre d'actions
- **Transmettre:** Passage de Brouillon à Nouveau
- **Voir Détails:** Vue complète avec actions associées
- **Statuts:** Brouillon → Actif → Clôturé → Archivé

### Gestion des Actions
- **Liste Complète:** Tableau multi-colonnes avec filtres
- **Statuts:** 7 états différents (Nouvelle, Acceptée, En cours, etc.)
- **Priorités:** 4 niveaux visuellement codifiés
- **Progression:** Barres de progression CSS3
- **Preuves:** Soumission de fichiers avec validation
- **Workflow:** Acceptation/rejet/transmission

### Rapports & Exports
- **Export Actions:** Liste complète en Excel
- **Export Plans:** Tous les plans en Excel
- **Dashboard Snapshot:** Métriques actuelles en Excel
- **Filtrage:** Par plan spécifique
- **Traçabilité:** Logging de tous les exports

---

## 🏗️ Architecture

### Structure du Projet

```
HabibaARR/
├── Controllers/          # Logique des requêtes HTTP
│   ├── AccountController.cs
│   ├── ActionPlansController.cs
│   ├── ActionsController.cs
│   ├── DashboardController.cs
│   ├── HomeController.cs
│   └── ReportsController.cs
│
├── Models/              # Entités métier
│   ├── ApplicationUser.cs
│   ├── ActionPlan.cs
│   ├── Action.cs
│   ├── Evidence.cs
│   ├── Attachment.cs
│   └── ActionLog.cs
│
├── Services/            # Logique métier
│   ├── ActionService.cs
│   ├── ActionPlanService.cs
│   ├── EvidenceService.cs
│   ├── ReportService.cs
│   ├── UserService.cs
│   └── DashboardService.cs
│
├── ViewModels/          # DTOs pour les vues
│   ├── LoginViewModel.cs
│   ├── RegisterViewModel.cs
│   └── ...ViewModels
│
├── Data/                # Contexte EF Core
│   ├── ApplicationDbContext.cs
│   ├── DbInitializer.cs
│   └── Migrations/
│
├── Views/               # Razor Templates
│   ├── Home/
│   ├── Account/
│   ├── Dashboard/
│   ├── ActionPlans/
│   ├── Actions/
│   ├── Reports/
│   └── Shared/
│
├── wwwroot/
│   └── css/
│       └── site.css     # Styles CSS3 personnalisés
│
├── appsettings.json
├── Program.cs
└── HabibaARR.csproj
```

### Pile Technologique

| Composant | Technologie |
|-----------|------------|
| Framework | ASP.NET Core 8.0 MVC |
| Langage | C# 12.0 |
| ORM | Entity Framework Core 8.0 |
| Base de Données | SQL Server |
| Authentification | ASP.NET Core Identity |
| Export Excel | ClosedXML |
| Logging | Serilog |
| Frontend | HTML5 + CSS3 (aucun JavaScript) |

### Design Pattern

- **MVC:** Separation of Concerns
- **Repository Pattern:** Via Entity Framework
- **Service Layer:** Logique métier centralisée
- **Dependency Injection:** Intégré à ASP.NET Core
- **ViewModels:** DTOs entre Controllers et Views

---

## 🐛 Dépannage

### Erreur: "Connection string not found"

**Solution:**
```bash
# Vérifier le fichier appsettings.json existe
dotnet ef database update
```

### Erreur: ".NET 8.0 not installed"

**Solution:**
```bash
# Vérifier la version installée
dotnet --list-sdks

# Télécharger .NET 8.0
# https://dotnet.microsoft.com/download/dotnet/8.0
```

### Base de Données Corrompue

**Solution:**
```bash
# Supprimer et recréer
dotnet ef database drop -f
dotnet ef database update
```

### Port 5001 Déjà Utilisé

**Solution:**
```bash
# Changer le port dans appsettings.json
"Urls": "https://localhost:5002"

# Ou via la ligne de commande
dotnet run --urls "https://localhost:5002"
```

### Performance Lente

**Optimisations:**
```csharp
// Déjà optimisées par défaut:
// - Entity Framework lazy loading désactivé
// - N+1 queries évitées avec .Include()
// - Indexes sur les colonnes de recherche
// - Caching des données statiques

// Pour plus de performance:
dotnet run --configuration Release
```

---

## 📖 Utilisation

### Workflow Complet - Gestionnaire

1. **Créer un Plan**
   - Menu → Plans d'action → Nouveau plan
   - Remplir: Titre, Description, Dates
   - Sauvegarder

2. **Créer une Action**
   - Dans le plan → Ajouter action
   - Détails: Titre, Description, Responsable, Priorité
   - Définir dates et pourcentage d'avancement

3. **Transmettre le Plan**
   - Plans d'action → Bouton "Transmettre"
   - Plan passe de Brouillon à Actif

### Workflow Complet - Responsable

1. **Consulter les Actions**
   - Menu → Actions
   - Voir la liste des actions assignées

2. **Accepter ou Rejeter**
   - Cliquer sur l'action
   - "Accepter" ou saisir motif et "Rejeter"
   - Action devient "Acceptée" ou "Rejetée"

3. **Soumettre une Preuve**
   - Cliquer sur l'action acceptée
   - Section "Preuves" → Soumettre preuve
   - Ajouter commentaire et fichiers
   - Valider

4. **Marquer Complétée**
   - Après approbation de la preuve par le gestionnaire
   - Action devient "Complétée"

---

## 📞 Support

Pour les problèmes:

1. **Consulter les logs:**
   ```bash
   # Logs dans la console lors du développement
   # Logs dans Windows Event Viewer en production
   ```

2. **Vérifier la base de données:**
   - SQL Server Management Studio (SSMS)
   - Serveur: `(localdb)\mssqllocaldb`
   - Base: `HabibaARR`

3. **Redémarrer l'application:**
   ```bash
   # Arrêter avec Ctrl+C
   # Redémarrer avec dotnet run
   ```

---

## ✅ Checklist de Déploiement

- [ ] .NET 8.0 SDK installé
- [ ] SQL Server LocalDB disponible
- [ ] `dotnet restore` réussi
- [ ] `dotnet ef database update` réussi
- [ ] Application démarre sans erreur
- [ ] Connexion possible avec identifiants de test
- [ ] Dashboard affiche les métriques
- [ ] Création de plan fonctionne
- [ ] Export Excel fonctionne
- [ ] Logs s'affichent correctement

---

## 🎉 Conclusion

**Plateforme opérationnelle et prête pour:**
- Gestion des plans d'action complète
- Workflow multi-acteurs automatisé
- Traçabilité complète avec audit
- Rapports et exports Excel
- Dashboard avec KPIs

**Version:** 1.0 - Production Ready  
**Date:** Septembre 2026  
**Status:** ✅ COMPLÈTE

---

Bonne utilisation de GPA NOVEC! 🚀
