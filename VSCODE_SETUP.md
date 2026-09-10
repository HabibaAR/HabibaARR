# ⚡ GUIDE COMPLET - EXÉCUTER SUR VS CODE

## 📋 Prérequis

### À installer sur votre machine

1. **.NET 7.0 SDK** 
   - Télécharger: https://dotnet.microsoft.com/download
   - Vérifier: `dotnet --version` (doit afficher 7.x.x)

2. **SQL Server 2019+** 
   - Option 1: SQL Server Express (gratuit) https://www.microsoft.com/sql-server/sql-server-downloads
   - Option 2: LocalDB (inclus avec Visual Studio/VS Code tools)
   - Option 3: Docker: `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest`

3. **VS Code**
   - Télécharger: https://code.visualstudio.com/

### Extensions VS Code essentielles

Une fois VS Code ouvert, installer ces extensions:

1. **C# Dev Kit** (Microsoft) - obligatoire pour ASP.NET
2. **.NET Runtime Installer** (Microsoft)
3. **SQL Server (mssql)** (Microsoft) - optionnel pour gérer la DB

**Installation rapide:**
- Cliquer l'icône Extensions (Ctrl+Shift+X)
- Chercher "C# Dev Kit" → Installer
- Chercher "mssql" → Installer

---

## 🚀 DÉMARRAGE COMPLET - Étape par étape

### ÉTAPE 1: Cloner le repository

```bash
# Ouvrir un terminal dans le répertoire où vous voulez le projet
# Puis exécuter:
git clone <URL-du-repo> HabibaARR
cd HabibaARR
```

### ÉTAPE 2: Ouvrir dans VS Code

```bash
# Depuis le répertoire du projet:
code .
```

Ou manuellement:
- VS Code → Fichier → Ouvrir le dossier → Sélectionner `HabibaARR`

**Attendre 30-60 secondes** pour que VS Code indexe le projet (notifications en bas à droite).

### ÉTAPE 3: Restaurer les dépendances

Dans VS Code, ouvrir le **Terminal intégré** (Ctrl+`):

```bash
dotnet restore
```

Attendez que ça termine (ça peut prendre 2-3 minutes).

### ÉTAPE 4: Configurer la base de données

#### Option A: LocalDB (PLUS SIMPLE - Recommandé)

Pas besoin de faire quoi que ce soit! La chaîne de connexion par défaut utilise LocalDB.

Vérifier que LocalDB est installé:
```bash
# Dans le terminal VS Code:
sqllocaldb info mssqllocaldb
```

Si pas trouvé, installer: https://docs.microsoft.com/sql/database-engine/configure-windows/sql-server-2019-express-localdb

#### Option B: SQL Server Express

Si vous avez SQL Server Express à `localhost\SQLEXPRESS`:

Modifier `appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=HabibaARR;Trusted_Connection=true;"
}
```

#### Option C: Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword@123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

# Modifier appsettings.json:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1,1433;Database=HabibaARR;User Id=sa;Password=YourPassword@123;"
}
```

### ÉTAPE 5: Créer la base de données

**Important:** Les migrations s'appliquent **automatiquement** au démarrage de l'app.

Mais pour tester avant:
```bash
# Dans le terminal VS Code:
dotnet ef database update
```

Cela va:
- ✅ Créer la base de données
- ✅ Créer toutes les tables
- ✅ Insérer les données de test (4 utilisateurs avec rôles)

### ÉTAPE 6: Lancer l'application

**Méthode 1: Avec le débugueur (RECOMMANDÉ)**

- Appuyer sur **F5** 
- Ou Exécuter → Démarrer le débogage
- Ou Ctrl+Shift+D → Cliquer "Lancer"

VS Code va:
1. Compiler le projet
2. Démarrer l'app
3. Ouvrir automatiquement le navigateur sur https://localhost:5001

**Méthode 2: Sans débugueur**

```bash
dotnet run
```

Puis ouvrir manuellement: https://localhost:5001

**Méthode 3: Avec rechargement automatique (Hot Reload)**

```bash
dotnet watch run
```

Le projet se recompile/relance automatiquement quand vous sauvegardez un fichier (parfait pour le développement).

---

## 🔐 Se connecter

Une fois l'app lancée (https://localhost:5001), cliquer "Se connecter".

### Identifiants de test:

| Rôle | Email | Mot de passe |
|------|-------|--------------|
| **Admin** | admin@novec.fr | Test@12345 |
| **Directeur** | directeur@novec.fr | Test@12345 |
| **Gestionnaire** | gestionnaire@novec.fr | Test@12345 |
| **Responsable** | responsable@novec.fr | Test@12345 |

**Tester en tant que GESTIONNAIRE en premier** (pour créer un plan et une action).

---

## ✨ Tester la plateforme

### Workflow complet à tester:

**1. En tant que GESTIONNAIRE:**
- Aller à "Plans d'action"
- Créer un nouveau plan
- Créer une action dans le plan
- Assigner à un responsable
- Transmettre le plan

**2. En tant que RESPONSABLE:**
- Aller à "Actions"
- Voir l'action assignée
- Accepter l'action
- Soumettre une preuve (ajouter un fichier test)

**3. Retour GESTIONNAIRE:**
- Voir la preuve dans les détails de l'action
- Valider la preuve
- L'action passe à "Complétée"

**4. DASHBOARD:**
- Aller au tableau de bord
- Voir les KPI et statistiques
- Vérifier que l'action complétée est comptée

---

## 🛠️ Développement local

### Modifier le code

VS Code va automatiquement **recompiler** quand vous sauvegardez un fichier (si `dotnet watch run`).

**Points clés:**
- **Controllers** en C# → `/Controllers`
- **Vues** en Razor → `/Views`
- **Styles** CSS → `/wwwroot/css/site.css`
- **JavaScript** → `/wwwroot/js/site.js`

### Arrêter l'app

- **Avec F5:** Cliquer "Arrêter" ou Maj+F5
- **Avec terminal:** Ctrl+C

### Relancer l'app

F5 ou `dotnet run`

---

## 🐛 Troubleshooting

### ❌ "Cannot connect to SQL Server"

```bash
# Vérifier que LocalDB/SQL Server fonctionne:
# LocalDB:
sqllocaldb start mssqllocaldb

# SQL Server Express:
# Vérifier que le service "SQL Server (SQLEXPRESS)" est lancé dans Services Windows
```

### ❌ "Port 5001 already in use"

```bash
# Utiliser un autre port:
dotnet run --urls "https://localhost:5002"
```

### ❌ "Migrations not applied"

```bash
dotnet ef database update
```

### ❌ "C# extension not found"

1. Ctrl+Shift+X (Extensions)
2. Chercher "C#"
3. Installer "C# Dev Kit" (Microsoft)
4. Recharger VS Code (Ctrl+Shift+P → "Reload Window")

### ❌ ".NET SDK not found"

```bash
# Vérifier l'installation:
dotnet --version

# Si besoin, installer:
# https://dotnet.microsoft.com/download
```

---

## 📊 Commandes utiles VS Code

| Action | Raccourci |
|--------|-----------|
| **Lancer l'app** | F5 |
| **Arrêter l'app** | Maj+F5 |
| **Terminal intégré** | Ctrl+` |
| **Palette de commandes** | Ctrl+Maj+P |
| **Afficher les fichiers** | Ctrl+E |
| **Chercher dans le code** | Ctrl+Maj+F |
| **Comparer fichiers** | Sélect 2 fichiers → Clic droit → "Compare" |

---

## 📝 Structure du projet dans VS Code

```
HabibaARR/
├── Controllers/              # Logique HTTP
│   ├── AccountController.cs
│   ├── DashboardController.cs
│   ├── ActionPlansController.cs
│   └── ActionsController.cs
│
├── Models/                   # Entités métier
│   ├── ApplicationUser.cs
│   ├── Action.cs
│   ├── ActionPlan.cs
│   ├── Evidence.cs
│   └── ...
│
├── Services/                 # Logique métier
│   ├── IActionService.cs
│   ├── ActionService.cs
│   └── ...
│
├── Views/                    # Vues Razor
│   ├── Home/
│   ├── Account/
│   ├── Dashboard/
│   ├── Actions/
│   └── ActionPlans/
│
├── wwwroot/                  # Assets
│   ├── css/site.css
│   ├── js/site.js
│   └── uploads/              # Fichiers uploadés
│
├── Program.cs                # Configuration app
├── appsettings.json          # Settings
└── HabibaARR.csproj         # Définition projet
```

---

## ✅ Checklist de démarrage

- [ ] .NET 7.0 SDK installé (`dotnet --version`)
- [ ] SQL Server/LocalDB accessible
- [ ] VS Code ouvert avec le projet
- [ ] Extension C# Dev Kit installée
- [ ] `dotnet restore` exécuté
- [ ] `dotnet ef database update` exécuté
- [ ] **F5** pour lancer l'app
- [ ] https://localhost:5001 accessible
- [ ] Login avec admin@novec.fr / Test@12345
- [ ] Dashboard affiche les KPI ✅

---

## 🎉 C'est prêt!

Vous pouvez maintenant:
- ✅ Développer en local avec hot reload
- ✅ Tester toutes les fonctionnalités
- ✅ Modifier le code en temps réel
- ✅ Déboguer avec les points d'arrêt (F9)
- ✅ Versionner avec Git

**Bon développement! 🚀**

---

**En cas de problème:**
- Voir `GUIDE_DEMARRAGE.md` pour plus de détails
- Voir `ARCHITECTURE.md` pour la doc technique
- Vérifier les logs dans le terminal VS Code

Prêt à développer la plateforme NOVEC sur VS Code! 💪
