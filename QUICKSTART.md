# ⚡ GPA NOVEC - Démarrage Rapide (3 minutes)

## 📥 Étape 1: Récupérer le Projet

### GitHub
```bash
git clone https://github.com/HabibaAR/HabibaARR.git
cd HabibaARR
```

### ZIP
1. Accès: https://github.com/HabibaAR/HabibaARR → Code → Download ZIP
2. Extrayez le dossier
3. Ouvrez un terminal dans le dossier

---

## 🚀 Étape 2: Exécution Rapide (Choisir VOTRE système)

### ⚪ Windows (PowerShell)
```powershell
powershell -ExecutionPolicy Bypass -File setup.ps1
```

### 🍎 Mac / 🐧 Linux
```bash
bash setup.sh
```

### 🔧 Manuelle (Tous les OS)
```bash
dotnet restore
dotnet ef database update
dotnet run
```

---

## ✅ Résultat Attendu

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
```

---

## 🌐 Ouvrir l'App

📱 **Navigateur:** https://localhost:5001

---

## 👤 Se Connecter

```
Email:         admin@novec.fr
Mot de passe:  Test@12345
```

Autres utilisateurs:
- `gestionnaire@novec.fr` - Gère les plans
- `responsable@novec.fr` - Accepte les actions
- `directeur@novec.fr` - Voit les rapports

---

## 📚 Documentation

| Fichier | Contenu |
|---------|---------|
| **GUIDE_VSCODE.md** | Configuration complète, troubleshooting |
| **README_FINAL.md** | Vue d'ensemble du projet |
| **DEPLOYMENT_GUIDE_FR.md** | Installation/déploiement détaillé |

---

## ❓ Besoin d'Aide?

| Problème | Solution |
|----------|----------|
| **"Port 5001 déjà utilisé"** | `dotnet run --urls "https://localhost:5002"` |
| **".NET not found"** | [Télécharger .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) |
| **"Database error"** | `dotnet ef database drop -f && dotnet ef database update` |
| **HTTPS certificate error** | Accepter le risque en navigateur / Voir GUIDE_VSCODE.md |

---

## 🎯 Workflow Complet de Test

1. **Login** → admin@novec.fr
2. **Créer Plan** → Menu Plans d'action → Nouveau
3. **Créer Action** → Dans le plan → Ajouter action
4. **Transmettre** → Passer de Brouillon à Actif
5. **Login Responsable** → responsable@novec.fr
6. **Accepter Action** → Voir la liste, accepter
7. **Soumettre Preuve** → Ajouter commentaire + fichier
8. **Login Gestionnaire** → Valider la preuve
9. **Voir Dashboard** → Métriques mises à jour
10. **Exporter Excel** → Rapports

---

## 🎨 Architecture

```
Navigateur (HTML5 + CSS3)
         ↓
    VSCode/IIS
         ↓
 ASP.NET Core 8 MVC
         ↓
Entity Framework Core 8
         ↓
   SQL Server LocalDB
```

**Zero JavaScript, Zero Bootstrap** - 100% CSS3 pur ✨

---

## 📦 Fichiers Clés

```
HabibaARR/
├── Controllers/         # Logique applicative
├── Models/             # Entités (Action, Plan, Evidence, etc.)
├── Views/              # Razor HTML (100% CSS3)
├── Services/           # Service métier
├── wwwroot/css/        # Styles CSS3 purs
├── appsettings.json    # Configuration
├── Program.cs          # Startup
└── HabibaARR.csproj   # Projet C#
```

---

## 🔍 Vérifier l'Installation

```bash
# Vérifier .NET
dotnet --version

# Vérifier SQL Server LocalDB
sqllocaldb info

# Vérifier la base de données
dotnet ef dbcontext info

# Voir les commits
git log --oneline
```

---

## 🎉 Prêt à Développer!

L'application est **opérationnelle** sur https://localhost:5001

Explorez:
- ✅ Dashboard avec KPIs
- ✅ Gestion des plans et actions
- ✅ Workflows multi-acteurs
- ✅ Submission de preuves
- ✅ Exports Excel
- ✅ Journal d'audit complet

---

## 📖 Pour Plus de Détails

👉 Consultez **GUIDE_VSCODE.md** pour:
- Configuration détaillée
- Dépannage avancé
- Débogage avec VSCode
- Personnalisation CSS
- Déploiement production

---

**Version:** 1.0 - Septembre 2026  
**Statut:** ✅ Production Ready  
**Tech:** ASP.NET Core 8 + EF Core 8 + SQL Server + CSS3
