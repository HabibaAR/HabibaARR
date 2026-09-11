# 🚀 GUIDE DE LANCEMENT COMPLET - Plateforme GPA NOVEC

## 📋 Table des Matières
1. [Prérequis](#prérequis)
2. [Installation & Lancement](#installation--lancement)
3. [Comptes de Test](#comptes-de-test)
4. [Workflow Complet de Test](#workflow-complet-de-test)
5. [Architecture & Composants](#architecture--composants)
6. [Dépannage](#dépannage)

---

## ✅ Prérequis

### Sur Windows
- **Visual Studio Code** (dernière version) - [Télécharger](https://code.visualstudio.com/)
- **.NET 8.0 SDK** - [Télécharger](https://dotnet.microsoft.com/download)
- **SQL Server** (LocalDB inclus avec .NET SDK)
- **PowerShell** (inclus Windows 10+)

### Sur Mac/Linux
- **.NET 8.0 SDK**
- **SQL Server** ou Docker pour SQL Server
- **Terminal/Bash**

### Vérifier l'installation
```bash
# Vérifier .NET
dotnet --version

# Doit afficher: 8.0.x
```

---

## 🎯 Installation & Lancement

### Étape 1: Ouvrir le Projet
```bash
# Windows PowerShell
cd C:\Users\YourUsername\Downloads\HabibaARR

# Mac/Linux
cd ~/Downloads/HabibaARR
```

### Étape 2: Restaurer les Dépendances
```bash
dotnet restore
```
**Attendu:** Télécharge tous les packages NuGet (30-60 secondes)

### Étape 3: Construire l'Application
```bash
dotnet build
```
**Attendu:** 
- ✅ Build succeeded
- ✅ 0 erreurs, quelques avertissements XML (normaux)

### Étape 4: LANCER L'APPLICATION

#### Option A: Ligne de commande (Recommandé)
```bash
dotnet run
```

**Attendu:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to exit.
```

**Ensuite:** Ouvrir navigateur → `https://localhost:5001`

#### Option B: VSCode avec F5
1. Ouvrir le dossier HabibaARR dans VSCode
2. Appuyer sur **F5** (ou Ctrl+F5)
3. Sélectionner **.NET** si demandé
4. Le navigateur s'ouvre automatiquement

---

## 👥 Comptes de Test

L'application crée **4 comptes de test automatiquement** (première exécution):

| Rôle | Email | Mot de passe | Fonction |
|------|-------|--------------|----------|
| **Administrateur** | `admin@novec.fr` | `Test@12345` | Gestion système |
| **Directeur** | `directeur@novec.fr` | `Test@12345` | Validation preuves |
| **Gestionnaire** | `gestionnaire@novec.fr` | `Test@12345` | Création plans/actions |
| **Responsable** | `responsable@novec.fr` | `Test@12345` | Exécution actions |

### Se Connecter
1. Sur la page d'accueil, cliquer sur **"Se connecter"**
2. Utiliser l'email + mot de passe d'un compte ci-dessus
3. ✅ Accès confirmé!

---

## 🔄 Workflow Complet de Test

### 📌 Scénario: Créer Plan → Transmettre → Accepter → Exécuter → Valider → Fermer

#### **Phase 1: Gestionnaire crée un Plan (5 min)**

**Connexion:** `gestionnaire@novec.fr` / `Test@12345`

1. Aller à **Plans d'Action → Créer Plan**
2. Remplir le formulaire:
   ```
   Référence: PLAN-2025-TEST-001
   Titre: Amélioration Processus Qualité
   Description: Plan d'amélioration pour Q4 2025
   Date Début: [Date d'aujourd'hui]
   Date Fin: [Date dans 30 jours]
   Priorité: Élevée
   ```
3. Cliquer **Créer Plan**
4. ✅ Plan créé en statut **BROUILLON (Draft)**

#### **Phase 2: Gestionnaire crée une Action (5 min)**

1. Cliquer sur le plan créé
2. Aller à **Actions → Ajouter Action**
3. Remplir:
   ```
   Titre: Audit des Processus
   Description: Effectuer audit complet
   Responsable: RESPONSABLE (Pierre Responsable)
   Date Limite: [Dans 20 jours]
   Priorité: Élevée
   ```
4. Cliquer **Créer Action**
5. ✅ Action créée en statut **BROUILLON**

#### **Phase 3: Gestionnaire transmet le Plan (2 min)**

1. Retourner au Plan
2. Cliquer **Transmettre le Plan**
3. ✅ Statut du plan → **TRANSMIS**
4. ✅ Statut de l'action → **NOUVEAU (New)**
5. Responsable reçoit notification

#### **Phase 4: Responsable accepte l'Action (3 min)**

**Déconnexion** → **Connexion:** `responsable@novec.fr` / `Test@12345`

1. Aller à **Mes Actions**
2. Voir l'action en statut **"À accepter"**
3. Cliquer sur l'action
4. Cliquer **Accepter**
5. ✅ Statut → **ACCEPTÉE (Accepted)**

#### **Phase 5: Responsable soumet une Preuve (5 min)**

1. Toujours dans l'action acceptée
2. Cliquer **Soumettre Preuve**
3. Remplir:
   ```
   Commentaire: J'ai completé l'audit des processus.
   ```
4. **Ajouter Fichier** (optionnel):
   - Créer fichier texte: `audit_results.txt`
   - Télécharger ce fichier
5. Cliquer **Soumettre**
6. ✅ Statut → **PREUVE SOUMISE (EvidenceSubmitted)**
7. Gestionnaire reçoit notification

#### **Phase 6: Gestionnaire valide la Preuve (2 min)**

**Déconnexion** → **Connexion:** `gestionnaire@novec.fr` / `Test@12345`

1. Aller à **Actions en Attente de Validation**
2. Cliquer sur l'action avec preuve
3. Consulter la preuve et le fichier
4. Cliquer **Valider la Preuve**
5. ✅ Statut → **COMPLÉTÉE (Completed)**
6. **Action fermée automatiquement!**

#### **Phase 7: Consulter l'Historique Complet (2 min)**

1. Cliquer sur l'action fermée
2. Aller à la section **Historique**
3. ✅ Voir toute la traçabilité:
   - Création (Gestionnaire)
   - Transmission (Gestionnaire)
   - Acceptation (Responsable)
   - Soumission preuve (Responsable)
   - Validation (Gestionnaire)

---

## 🏗️ Architecture & Composants

### 📂 Structure du Projet

```
HabibaARR/
├── Controllers/           # Contrôleurs MVC (Actions, Plans, Reports)
├── Models/               # Entités (Action, ActionPlan, Evidence, etc.)
├── Services/             # Logique métier (IActionService, etc.)
├── Views/                # Templates Razor (.cshtml)
│   ├── Actions/
│   ├── ActionPlans/
│   ├── Account/
│   ├── Dashboard/
│   ├── Reports/
│   └── Shared/
├── Data/                 # DbContext, Migrations
├── ViewModels/           # Modèles pour les vues
├── wwwroot/              # CSS, JS, images
├── appsettings.json      # Configuration
└── Program.cs            # Point d'entrée, DI
```

### 🗄️ Base de Données

**Localisation:** `(localdb)\mssqllocaldb` - `HabibaARR`

**Tables principales:**
- `AspNetUsers` - Utilisateurs + rôles
- `ActionPlans` - Plans d'action
- `Actions` - Actions individuelles
- `Evidences` - Preuves soumises
- `Attachments` - Fichiers joints
- `ActionLogs` - Journal d'audit complet

**Migrations:** Créées automatiquement au premier `dotnet run`

### 🔐 Sécurité

- ✅ **Authentification:** ASP.NET Core Identity (hash PBKDF2)
- ✅ **Autorisation:** Role-Based Access Control (RBAC)
- ✅ **CSRF Protection:** Tokens anti-CSRF automatiques
- ✅ **XSS Protection:** HtmlEncode tous les outputs
- ✅ **SQL Injection:** Requêtes paramétrées Entity Framework
- ✅ **Audit:** Tous les changements loggés

### 📊 Exports

**Formats supportés:**
- 📄 **PDF** (iText7) - Actions, Plans
- 📊 **Excel** (ClosedXML) - Rapports détaillés
- 📸 **Snapshot** - Vue instantanée du dashboard

---

## 🔧 Dépannage

### ❌ Erreur: "Connection string not found"
```
InvalidOperationException: Connection string not found.
```
**Cause:** `appsettings.json` manquant ou mal configuré  
**Solution:**
```bash
# Vérifier que appsettings.json existe
ls appsettings.json

# Vérifier le contenu
cat appsettings.json
```

### ❌ Erreur: "Port 5001 already in use"
```
Address already in use
```
**Cause:** Une autre application utilise le port 5001  
**Solution:**
```bash
# Tuer le processus .NET
# Windows:
taskkill /F /IM dotnet.exe

# Mac/Linux:
killall dotnet
```

### ❌ Erreur: "Database connection failed"
```
SqlException: A network-related or instance-specific error
```
**Cause:** SQL Server (LocalDB) ne tourne pas  
**Solution:**
```bash
# Windows - Démarrer LocalDB
"C:\Program Files\Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe" start mssqllocaldb

# Puis relancer dotnet run
dotnet run
```

### ❌ Erreur: "Certificate not trusted"
```
HTTPS certificate validation failed
```
**Cause:** Certificat auto-signé non accepté (normal en développement)  
**Solution:** Navigateur affiche un avertissement → Cliquer **Continuer**

### ❌ Pas de données de test
```
Connexion réussie mais pas de comptes
```
**Cause:** Base de données corrompue  
**Solution:**
```bash
# Supprimer la BD LocalDB
"C:\Program Files\Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe" delete mssqllocaldb

# Relancer (va recréer et reseeder)
dotnet run
```

---

## 📞 Support

### Vérifier les Logs

**En console (pendant `dotnet run`):**
```
Tous les logs s'affichent en temps réel
Chercher les erreurs avec [ERROR] ou [FATAL]
```

**Fichiers logs:**
```
Logs texte: logs/ (si configuré)
```

### Vérifier la Santé de l'Application

```bash
# Ouvrir URL de santé
https://localhost:5001/health

# Doit retourner:
# { "status": "healthy" }
```

---

## ✨ Checklist de Démarrage

- [ ] .NET 8.0 SDK installé (`dotnet --version`)
- [ ] Projet cloné/extrait
- [ ] `dotnet restore` exécuté
- [ ] `dotnet build` réussi (0 erreurs)
- [ ] `dotnet run` lancé → `Now listening on: https://localhost:5001`
- [ ] Navigateur affiche la page de connexion
- [ ] Connexion avec `admin@novec.fr` / `Test@12345` réussie
- [ ] Dashboard s'affiche
- [ ] Workflow de test complété (Plan → Action → Acceptation → Preuve → Validation)

---

## 🎉 L'Application Fonctionne!

Une fois tous les points complétés, l'application **fonctionne correctement** avec:
- ✅ Authentification & autorisation
- ✅ Workflow complet 8 états
- ✅ Gestion plans et actions
- ✅ Soumission de preuves avec fichiers
- ✅ Audit complet
- ✅ Exports PDF/Excel
- ✅ Dashboard personnalisé par rôle

**Toute erreur?** Consulter la section **Dépannage** ci-dessus.

---

**Dernière mise à jour:** 11 Sept 2025  
**Version:** 1.0  
**Statut:** ✅ Prêt pour production
