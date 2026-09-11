# 🚀 COMMENT LANCER L'APPLICATION SUR VSCODE

## ✅ Prérequis Vérifiés

- ✅ Application compile sans erreurs (0 erreurs)
- ✅ Code complet et structuré
- ✅ Tous les services implémentés
- ✅ Tous les contrôleurs présents
- ✅ BD configurée (LocalDB - fonctionne sur Windows/Mac)

---

## 📋 Étapes pour VSCode (Windows/Mac)

### Étape 1️⃣: Télécharger & Extraire le ZIP

**Windows:**
```
1. Télécharger: HabibaARR-COMPLET-2025.zip
2. Clic droit → Extraire tout
3. Choisir dossier de destination
```

**Mac:**
```bash
unzip HabibaARR-COMPLET-2025.zip
```

**Résultat:** Dossier `HabibaARR` créé

---

### Étape 2️⃣: Ouvrir dans VSCode

**Option A: Depuis VSCode**
```
1. Ouvrir VSCode
2. File → Open Folder
3. Sélectionner dossier HabibaARR
4. Cliquer "Open"
```

**Option B: Depuis Terminal**
```bash
code HabibaARR
```

**Résultat:** Le projet s'ouvre dans VSCode

---

### Étape 3️⃣: Restaurer les Dépendances

**Dans VSCode Terminal:**
```
Ctrl+` (ou View → Terminal)
```

**Puis exécuter:**
```bash
dotnet restore
```

**Attendu:** Télécharge tous les packages (30-60 secondes)

---

### Étape 4️⃣: LANCER AVEC F5 🎉

**Méthode 1: Appuyer sur F5**
```
Appuyer sur: F5
ou Ctrl+F5
```

**Méthode 2: Depuis Terminal**
```bash
dotnet run
```

**Attendu:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started.
```

✅ **L'application démarre!**

---

### Étape 5️⃣: Ouvrir dans Navigateur

Le navigateur devrait s'ouvrir automatiquement à:
```
https://localhost:5001
```

Si ce n'est pas automatique, ouvrir manuellement:
```
https://localhost:5001
```

✅ **Page de connexion s'affiche!**

---

### Étape 6️⃣: Se Connecter

**Utiliser ce compte:**
```
Email: admin@novec.fr
Mot de passe: Test@12345
```

**Cliquer "Se connecter"**

✅ **Vous êtes connecté!**

---

## 🎮 VSCode Configuration (Déjà incluse)

Le projet inclut la configuration VSCode:

**Fichiers inclus:**
```
.vscode/launch.json     ← Configuration F5
.vscode/tasks.json      ← Build, Run, Watch tasks
.vscode/settings.json   ← C# formatting
.vscode/extensions.json ← Extensions recommandées
```

**Quand vous ouvrez le projet, VSCode vous proposera:**
```
"Install the recommended C# extensions for this workspace?"
→ Cliquer "Install"
```

Cela installe automatiquement tout ce qui est nécessaire.

---

## 🔧 Configuration VSCode (Optionnel)

### Installer les extensions recommandées

1. Ouvrir Extensions (Ctrl+Shift+X)
2. Chercher chaque extension:
   ```
   - C# Dev Kit (Microsoft)
   - C# (Microsoft)
   - Pylance (optionnel)
   - REST Client (optionnel)
   ```
3. Cliquer "Install"

### Configurer Build & Run

Les tâches sont déjà configurées! Vous pouvez:

**Build (Ctrl+Shift+B):**
```
Select task → .NET: build
```

**Run (Terminal):**
```bash
dotnet run
```

---

## ✅ Checklist de Démarrage

- [ ] ZIP téléchargé et extrait
- [ ] Dossier HabibaARR ouvert dans VSCode
- [ ] `dotnet restore` exécuté (terminal)
- [ ] F5 appuyé (ou `dotnet run` dans terminal)
- [ ] Application démarre → https://localhost:5001
- [ ] Page de connexion s'affiche
- [ ] Connexion réussie (admin@novec.fr / Test@12345)
- [ ] Dashboard s'affiche
- [ ] 🎉 SUCCESS!

---

## 🆘 Problèmes Courants

### ❌ "Port 5001 already in use"
```bash
# Tuer le processus .NET
# Windows (PowerShell):
taskkill /F /IM dotnet.exe

# Mac/Linux:
killall dotnet
```

### ❌ "Certificate not trusted"
Navigateur affiche avertissement → Cliquer **Continue** (c'est normal en développement)

### ❌ "dotnet: command not found"
.NET n'est pas installé:
```
Télécharger: https://dotnet.microsoft.com/download
Installer: .NET 8.0 SDK
Redémarrer VSCode
```

### ❌ Extensions ne s'installent pas
```
1. Ouvrir Extensions (Ctrl+Shift+X)
2. Chercher: C#
3. Cliquer "Install" sur C# Dev Kit
4. Redémarrer VSCode
```

---

## 🎯 Test Complet (20 min)

Une fois connecté, testez le workflow:

1. **Créer un plan** (5 min)
   - Plans d'Action → Créer Plan
   - Remplir titre, description
   - Cliquer Créer

2. **Ajouter une action** (5 min)
   - Cliquer sur le plan
   - Actions → Ajouter Action
   - Cliquer Créer

3. **Transmettre** (2 min)
   - Bouton "Transmettre le Plan"
   - Statut change

4. **Accepter** (2 min)
   - Déconnexion
   - Connexion: responsable@novec.fr / Test@12345
   - Mes Actions → Accepter

5. **Soumettre preuve** (3 min)
   - Soumettre Preuve
   - Ajouter fichier (optionnel)
   - Cliquer Soumettre

6. **Valider** (2 min)
   - Déconnexion
   - Reconnexion: gestionnaire@novec.fr
   - Valider la preuve

7. **Action fermée!** ✅

---

## 🎉 L'Application Fonctionne!

Une fois ces étapes complétées:

✅ Application lancée sur https://localhost:5001  
✅ Connexion fonctionnelle  
✅ Workflow complet testé  
✅ Dashboard affichée  
✅ Plans créables  
✅ Actions gérables  
✅ Exports fonctionnels  

**Bravo! Vous avez réussi!** 🎉

---

## 📚 Documentation

Tous les guides sont dans le dossier HabibaARR:

- **LANCER_MAINTENANT.md** - Guide rapide
- **GUIDE_LANCEMENT_COMPLET.md** - Guide détaillé
- **README.md** - Vue d'ensemble
- **IMPLEMENTATION_CHECKLIST.md** - Spécifications

---

## 💡 Conseils VSCode

### Raccourcis Utiles
```
Ctrl+` → Terminal
F5 → Lancer (debug)
Ctrl+Shift+B → Build
Ctrl+F5 → Run sans debug
Ctrl+K Ctrl+0 → Replier tous les dossiers
Ctrl+P → Quick Open fichiers
```

### Mode Debug
```
F5 lance en mode Debug
Points d'arrêt (breakpoints): Cliquer à gauche d'une ligne
Ctrl+F5 lance sans debug (plus rapide)
```

### Extensions Recommandées
```
✅ C# Dev Kit (essentiellement)
✅ C# (essentiellement)
✅ REST Client (optionnel - pour tester API)
✅ Prettier (optionnel - format code)
```

---

**Version:** 1.0  
**Date:** 11 Sept 2025  
**Status:** ✅ Ready to Launch
