# 🚀 GPA NOVEC - Guide d'Exécution sur VSCode

## ✅ Prérequis Obligatoires

### Windows / Mac / Linux
1. **Visual Studio Code** - [Télécharger](https://code.visualstudio.com/)
2. **.NET 8.0 SDK** - [Télécharger](https://dotnet.microsoft.com/download/dotnet/8.0)
3. **SQL Server LocalDB** (Windows) ou **SQL Server Docker** (Mac/Linux)
4. **Git** - [Télécharger](https://git-scm.com/)

### Extensions VSCode Recommandées
- C# Dev Kit (Microsoft)
- C# Extensions (kreativ)
- REST Client (humao.rest-client)

---

## 📥 Étape 1: Récupérer le Projet

### Option A: Via Git (Recommandé)
```bash
# Cloner le repository
git clone https://github.com/HabibaAR/HabibaARR.git
cd HabibaARR

# Vérifier que vous êtes sur la branche main ou votre branche
git branch -v
```

### Option B: Télécharger un ZIP
- Allez sur https://github.com/HabibaAR/HabibaARR
- Cliquez sur **Code** → **Download ZIP**
- Extrayez le dossier

---

## 🔧 Étape 2: Ouvrir dans VSCode

```bash
# Depuis le dossier HabibaARR
code .
```

VSCode va détecter automatiquement le projet C#/.NET.

---

## 💾 Étape 3: Configurer la Base de Données

### 3.1 Installer SQL Server LocalDB (Windows uniquement)
```bash
# Vérifier l'installation
sqllocaldb info mssqllocaldb

# Si erreur, installer via Visual Studio Installer
# Chercher "SQL Server Express LocalDB" dans les options
```

### 3.2 Créer et Initialiser la Base de Données
```bash
# Depuis le dossier HabibaARR (ouvert dans VSCode)
# Utiliser le terminal intégré: Ctrl+`

dotnet restore
dotnet ef database update
```

**Résultat attendu:**
```
Build started...
Build succeeded.
Done. Database updated successfully.
```

La base de données `HabibaARR` est créée avec:
- Tables: Users, Roles, Plans, Actions, Evidence, Attachments, Logs
- Utilisateurs de test seeding
- Indexes sur colonnes critiques

---

## ▶️ Étape 4: Démarrer l'Application

### Via VSCode

**Option 1: Terminal intégré**
```bash
# Depuis VSCode (Ctrl+`)
dotnet run
```

**Option 2: Utiliser le débogueur**
1. Appuyez sur **F5** ou **Run** → **Start Debugging**
2. Sélectionnez **.NET 8.0 (Latest)**
3. L'app démarre en debug mode

**Résultat attendu:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to exit.
```

---

## 🌐 Étape 5: Accéder à l'Application

### Dans le navigateur:
```
https://localhost:5001
```

Si HTTPS pose problème (certificat auto-signé):
- Acceptez le risque / Continuez quand même
- Ou modifiez `appsettings.json` pour HTTP

---

## 👤 Étape 6: Se Connecter

### Identifiants de Test Fournis:

| Rôle | Email | Mot de passe | Permissions |
|------|-------|-------------|------------|
| **Admin** | admin@novec.fr | Test@12345 | Tous accès |
| **Gestionnaire** | gestionnaire@novec.fr | Test@12345 | Créer/gérer plans et actions |
| **Responsable** | responsable@novec.fr | Test@12345 | Accepter/rejeter actions |
| **Directeur** | directeur@novec.fr | Test@12345 | Dashboard et rapports |

### Workflow de Test Complet:
1. Se connecter en tant que **Gestionnaire**
2. Créer un **Plan d'action** (Référence: PAC-2026-001)
3. Créer une **Action** dans le plan
4. **Transmettre** le plan (Brouillon → Actif)
5. Se déconnecter / Se connecter en tant que **Responsable**
6. **Accepter** l'action assignée
7. **Soumettre une preuve** avec commentaire et fichier
8. Se reconnecter en tant que **Gestionnaire**
9. **Valider** la preuve (Action → Complétée)
10. Voir les **Métriques** dans le Dashboard
11. **Exporter** en Excel

---

## 🛠️ Dépannage Courants

### ❌ "Connection string not found"
```bash
# Vérifier appsettings.json existe
cat appsettings.json

# Recréer la base de données
dotnet ef database drop -f
dotnet ef database update
```

### ❌ "Port 5001 is already in use"
```bash
# Arrêter le processus qui utilise le port, ou:
dotnet run --urls "https://localhost:5002"
```

### ❌ ".NET 8.0 not found"
```bash
# Vérifier les versions installées
dotnet --list-sdks

# Télécharger .NET 8.0
# https://dotnet.microsoft.com/download/dotnet/8.0
```

### ❌ Certificat HTTPS invalide
```bash
# Générer un certificat auto-signé
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

### ❌ Erreur de migration EF Core
```bash
# Supprimer et recréer complètement
dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add Initial
dotnet ef database update
```

---

## 📁 Structure du Projet (VSCode)

```
HabibaARR/
├── Controllers/              # Logique HTTP
│   ├── AccountController.cs
│   ├── ActionsController.cs
│   ├── ActionPlansController.cs
│   ├── DashboardController.cs
│   ├── ReportsController.cs
│   └── HomeController.cs
│
├── Models/                   # Entités métier
│   ├── ApplicationUser.cs
│   ├── Action.cs
│   ├── ActionPlan.cs
│   ├── Evidence.cs
│   ├── Attachment.cs
│   └── ActionLog.cs
│
├── Services/                 # Logique métier
│   ├── ActionService.cs
│   ├── ActionPlanService.cs
│   ├── EvidenceService.cs
│   ├── ReportService.cs
│   └── DashboardService.cs
│
├── Views/                    # Razor Templates
│   ├── Account/
│   ├── Actions/
│   ├── ActionPlans/
│   ├── Dashboard/
│   ├── Reports/
│   └── Shared/
│
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Migrations/
│
├── wwwroot/
│   ├── css/site.css          # Styles CSS3 complets
│   └── uploads/              # Fichiers téléchargés
│
├── appsettings.json          # Configuration
├── Program.cs                # Startup
└── HabibaARR.csproj         # Projet C#
```

---

## 🔍 Développement & Débogage

### Débogueur VSCode
1. Définir un **breakpoint** (clic sur la ligne, rouge point)
2. Appuyer sur **F5** pour démarrer le débogueur
3. L'exécution s'arrête au breakpoint
4. Inspecter les variables dans le panneau **Variables**

### Logs en Temps Réel
- Les logs s'affichent dans le terminal VSCode
- Configurés via `appsettings.json` (Serilog)
- Fichiers logs dans `./logs/`

### Exécuter les Tests (si présents)
```bash
dotnet test
```

---

## 📊 Fonctionnalités Disponibles

✅ **Authentification & Autorisation**
- Login sécurisé avec ASP.NET Identity
- 4 rôles avec permissions distinctes
- "Se souvenir de moi"

✅ **Plans d'Action**
- Créer/modifier/consulter plans
- Workflow: Brouillon → Actif → Clôturé → Archivé
- Transmission au responsable

✅ **Actions**
- 7 statuts avec workflow complet
- 4 niveaux de priorité
- Barres de progression
- Indicateurs de retard

✅ **Preuves & Pièces Jointes**
- Soumettre preuves avec commentaires
- Upload de fichiers multiples
- Validation par gestionnaire

✅ **Dashboard KPI**
- Cartes KPI (Total, Complétées, En retard)
- Distribution des statuts
- Taux de clôture
- Graphiques CSS3

✅ **Rapports & Exports**
- Export Excel des actions
- Export Excel des plans
- Snapshot du dashboard

✅ **Journal d'Audit**
- Historique complet de chaque action
- Traçabilité des changements
- Timestamps précis

---

## 🎨 Design & Personnalisation

### Palettes CSS3 (wwwroot/css/site.css)
```css
--primary-color: #2a5298;      /* Bleu primaire */
--secondary-color: #ff6b6b;    /* Rouge secondaire */
--success-color: #27ae60;      /* Vert succès */
--danger-color: #e74c3c;       /* Rouge danger */
--warning-color: #f39c12;      /* Orange warning */
--info-color: #3498db;         /* Bleu info */
```

Modifier ces variables pour changer l'apparence globale.

---

## 📝 Configuration Avancée

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HabibaARR;Trusted_Connection=true;"
  },
  "AppSettings": {
    "SessionTimeoutMinutes": 60,
    "PasswordMinLength": 8,
    "MaxUploadSizeMB": 10,
    "AllowedFileExtensions": ["pdf", "docx", "xlsx", "jpg", "png"]
  }
}
```

### Logging (Serilog)
Les logs sont écrits dans:
- **Développement**: Console + fichier `./logs/`
- **Production**: Event Viewer Windows

---

## 🚀 Déploiement Production

### Avant le déploiement:
```bash
# Publier en Release
dotnet publish -c Release -o ./publish
```

### Avec Docker (optionnel):
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HabibaARR.dll"]
```

---

## 📞 Support & Ressources

### Documentation du Projet
- `README_FINAL.md` - Vue d'ensemble complète
- `DEPLOYMENT_GUIDE_FR.md` - Guide détaillé déploiement

### Ressources Externes
- [Docs ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [C# Language](https://docs.microsoft.com/dotnet/csharp)

### Commit Git pour Suivre les Changements
```bash
# Voir l'historique
git log --oneline

# Voir les détails d'un commit
git show fab7486
```

---

## ✅ Checklist Démarrage

- [ ] .NET 8.0 SDK installé (`dotnet --version`)
- [ ] SQL Server LocalDB disponible (`sqllocaldb info`)
- [ ] VSCode ouvert avec le projet (`code .`)
- [ ] Terminal VSCode ouvert (`Ctrl+``)
- [ ] `dotnet restore` exécuté
- [ ] `dotnet ef database update` réussi
- [ ] `dotnet run` affiche "Now listening on: https://localhost:5001"
- [ ] Application accessible sur https://localhost:5001
- [ ] Connexion réussie avec test@novec.fr

---

## 🎉 Prêt!

L'application GPA NOVEC est maintenant **opérationnelle** sur votre VSCode! 🚀

Bon développement! 💻

---

**Version:** 1.0  
**Date:** Septembre 2026  
**Tech Stack:** ASP.NET Core 8 + EF Core 8 + SQL Server  
**Design:** 100% CSS3 (aucun Bootstrap/JavaScript)
