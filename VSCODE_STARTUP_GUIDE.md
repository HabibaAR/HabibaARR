# 🎯 Guide Visuel - Exécution dans VSCode

## 1️⃣ Ouverture du Projet

### Étape 1: Ouvrir le dossier
```
File → Open Folder
Sélectionnez: /home/user/HabibaARR
```

### Étape 2: Accepter les extensions
VSCode vous demandera d'installer les extensions recommandées.
**Cliquez "Install All"**

```
[Pop-up] "C# Dev Kit"
       ↓ Install
[Pop-up] ".NET Extension Pack"
       ↓ Install
[Pop-up] "ms-dotnettools.csharp"
       ↓ Install
```

---

## 2️⃣ Premier Build (Une seule fois)

### Via Palette de Commandes:
```
Ctrl + Shift + P
    ↓ Tape "Run Build Task"
    ↓ Sélectionnez "build"
    ↓ Attendez la fin
```

Ou plus simplement:
```
Ctrl + Shift + B (Raccourci directe pour Build)
```

**Résultat attendu:**
```
✅ Build completed in X seconds
```

---

## 3️⃣ Lancement de l'Application

### Le Plus Simple: Appuyez sur F5

```
F5
  ↓
[VSCode] Building...
  ↓
[VSCode] Debugging... 
  ↓
[Navigateur] S'ouvre automatiquement
  ↓
https://localhost:5001
```

### Ou Manuellement:

```
Run → Start Debugging (ou Ctrl + F5)
```

---

## 🌐 Vous Voyez le Navigateur?

### ✅ Succès! L'application fonctionne!

```
https://localhost:5001

🔐 Se connecter:
   Email: gestionnaire@novec.fr
   Mot de passe: Test@12345
   
   ↓ Click Login
   
✅ Vous êtes sur la page d'accueil
   Avec le logo NOVEC en haut et au centre
```

---

## 🧪 Tester le Workflow Complet

### Dans VSCode Console en bas:

1. **Vérifiez qu'il n'y a pas d'erreurs rouges**
   ```
   INFO: Application started
   INFO: Now listening on https://localhost:5001
   ```

2. **Ouvrez l'application:**
   - Clic le lien bleu dans le terminal VSCode, OU
   - Allez manuellement à https://localhost:5001

3. **Test rapide (2 minutes):**
   ```
   Accueil → Plans d'action → Créer → Ajouter action 
        → Transmettre → Logout
   
   Login comme responsable@novec.fr
        → Actions → Accepter l'action → Soumettre preuve
   
   Logout → Login comme gestionnaire@novec.fr
        → Actions → Voir action → Clôturer
   
   ✅ Action devient CLÔTURÉE
   ```

---

## 🛑 Arrêter l'Application

### Option 1: Clic Stop dans VSCode
```
Run → Stop (ou Maj + F5)
```

### Option 2: Ctrl + C dans le terminal
```
Terminal → Ctrl + C
```

---

## 🔄 Redémarrer

### Pour relancer:
```
F5 (ou Run → Start Debugging)
```

### Le navigateur s'ouvre automatiquement!

---

## 📊 Interface VSCode Repères

```
┌──────────────────────────────────────────────────┐
│  File   Edit   View   Run   Terminal   Help      │ ← Menus
├──────────────────────────────────────────────────┤
│ 🔍 Explorer  📝 .cs files  ▶️ Debug    📝 Search │ ← Volets
├──────────────────────────────────────────────────┤
│ │ HabibaARR/                                     │
│ │ ├── Controllers/                              │
│ │ ├── Services/                                 │
│ │ ├── Views/                                    │
│ │ ├── wwwroot/                                  │
│ │ └── HabibaARR.csproj                          │
├──────────────────────────────────────────────────┤
│                                                  │
│  [Code editor affichant le fichier sélectionné] │
│                                                  │
├──────────────────────────────────────────────────┤
│  DEBUG CONSOLE                                   │
│  > INFO: Application started                     │
│  > listening on https://localhost:5001           │
└──────────────────────────────────────────────────┘
```

---

## 🆘 Problèmes Courants

### ❌ "Port 5001 already in use"

**Solution:**
```
Run → Stop (ou Maj + F5)
Attendez 3 secondes
F5 pour relancer
```

Ou:
```
Terminal → New Terminal
netstat -ano | find "5001"  (Windows) 
lsof -i :5001              (Mac/Linux)
Tuez le processus
F5 pour relancer
```

### ❌ "Build failed"

**Solution:**
```
Ctrl + Shift + P
Tape "dotnet: restore"
Attendre...
Ctrl + Shift + B (build again)
```

### ❌ "Database connection error"

**Solution:**
```
Terminal → New Terminal
dotnet ef database drop -f
dotnet ef database update
F5 pour relancer
```

### ❌ "The certificate is invalid"

**C'est normal en développement!**

Dans le navigateur:
```
Advanced → Proceed anyway (ou cliquez le lien bleu)
```

---

## 📚 Fichiers à Consulter

**Dans VSCode, ouvrez ces fichiers pour comprendre:**

| Fichier | Pour quoi |
|---------|-----------|
| `ACTORS_WORKFLOW_DETAILED.md` | Comprendre le workflow |
| `IMPLEMENTATION_SUMMARY.md` | Guide de test complet |
| `RUN_LOCALLY.md` | Instructions de lancement |
| `PRE_LAUNCH_CHECKLIST.md` | Checklist avant lancement |

**Pour les ouvrir:**
```
Ctrl + P
Tape le nom du fichier
Appuyez Enter
```

---

## ✨ C'est tout!

```
Vous avez maintenant:
✅ Projet ouvert dans VSCode
✅ Build configuré
✅ Application en exécution
✅ Navigateur accédant https://localhost:5001
✅ Workflow complet testable
✅ Aucune erreur ✓
```

**Bienvenue dans GPA NOVEC!** 🎉

---

**Besoin d'aide?** Consultez les fichiers markdown dans le projet.
