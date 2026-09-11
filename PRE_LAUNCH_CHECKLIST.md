# ✅ Checklist Pré-Lancement - GPA NOVEC

**Avant de lancer l'application, vérifiez ces points.**

## 🔧 Configuration Système

- [ ] .NET 8.0 SDK installé
  ```bash
  dotnet --version
  # Devrait afficher: 8.0.xxx
  ```

- [ ] SQL Server LocalDB disponible
  - Windows: Devrait être préinstallé avec Visual Studio
  - Linux/Mac: Utilisez Docker ou install SQL Server
  
- [ ] VSCode installé avec extensions recommandées
  - C# Dev Kit
  - .NET Extension Pack
  - C# (ms-dotnettools.csharp)

## 📁 Structure du Projet

Vérifiez que ces fichiers/dossiers existent:

```
HabibaARR/
├── .vscode/
│   ├── launch.json          ✓
│   ├── tasks.json           ✓
│   ├── settings.json        ✓
│   └── extensions.json      ✓
├── Controllers/             ✓
├── Services/                ✓
├── Models/                  ✓
├── Views/                   ✓
├── wwwroot/                 ✓
├── Data/                    ✓
├── HabibaARR.csproj         ✓
├── Program.cs               ✓
├── appsettings.json         ✓
├── DEMARRER.sh              ✓
├── RUN_LOCALLY.md           ✓
└── ACTORS_WORKFLOW_DETAILED.md  ✓
```

## 🚀 Lancement (Choisissez UNE méthode)

### Méthode 1: VSCode avec F5 (Recommandé)
```
1. Ouvrir VSCode
2. Ouvrir le terminal: Ctrl + `
3. Premier lancement: Ctrl + Shift + B → Sélectionner "build"
4. Attendre le build ✓
5. Appuyer sur F5 pour lancer
6. Le navigateur s'ouvre automatiquement
```

### Méthode 2: Terminal Direct
```bash
cd /chemin/vers/HabibaARR
dotnet run
```

### Méthode 3: Script de Démarrage (Linux/Mac)
```bash
cd /chemin/vers/HabibaARR
chmod +x DEMARRER.sh
./DEMARRER.sh
```

## 🌐 Accès à l'Application

Une fois lancée:
- 🔗 **URL:** https://localhost:5001
- ⚠️  Acceptez le certificat SSL auto-signé (c'est normal en développement)

## 🔑 Première Connexion

1. **Choisissez un utilisateur:**
   - Email: `gestionnaire@novec.fr`
   - Mot de passe: `Test@12345`

2. **Vous êtes dans!** ✅

## 📊 Test Rapide du Workflow

```
1. Cliquez sur "Plans d'action" → "Créer nouveau"
2. Remplissez les champs et cliquez "Créer"
3. Cliquez "Ajouter une action"
4. Remplissez et cliquez "Créer l'action"
5. Cliquez "Transmettre le plan"
6. Logout et reconnectez comme responsable@novec.fr
7. Cliquez sur "Actions"
8. Cliquez sur l'action créée
9. Cliquez "Accepter"
10. Remplissez le journal et cliquez "Soumettre une preuve"
11. Logout et reconnectez comme gestionnaire@novec.fr
12. Cliquez sur l'action → Vérifiez le bouton "Clôturer"
13. Cliquez "Clôturer" ✅
```

## ⚠️ Erreurs Possibles et Solutions

### "Port 5001 déjà utilisé"
```bash
dotnet run --urls "https://localhost:5002"
```

### "Database error" ou migration échoue
```bash
dotnet ef database drop -f
dotnet ef database update
```

### "Build échoue"
```bash
dotnet clean
dotnet restore
dotnet build
```

### "HTTPS Certificate error"
- C'est normal en développement
- Acceptez le certificat dans le navigateur
- Ou désactivez SSL pour développement:
  ```bash
  dotnet run --urls "http://localhost:5000"
  ```

### "Login ne fonctionne pas"
```bash
# Réinitialiser la base de données:
dotnet ef database drop -f
dotnet ef database update
# Les utilisateurs de test sont créés automatiquement
```

## 📱 Fonctionnalités à Tester

- ✅ Connexion avec 4 rôles différents
- ✅ Créer un plan d'action
- ✅ Créer une action
- ✅ Accepter/Rejeter une action
- ✅ Soumettre une preuve
- ✅ Valider/Rejeter une preuve
- ✅ Clôturer une action
- ✅ Voir le Dashboard
- ✅ Exporter en Excel
- ✅ Exporter en PDF
- ✅ Voir l'historique d'audit

## 🎯 Résultat Attendu

Une fois tout lancé, vous devriez voir:

```
1. Page d'accueil avec logo NOVEC ✅
2. Navigation avec tous les menus ✅
3. Workflow complet fonctionnel ✅
4. Exports Excel et PDF working ✅
5. Audit trail complet ✅
6. Aucun erreur en console ✅
```

## 📞 Si Vous Avez un Problème

1. **Vérifiez les logs:**
   - Ouvrez la console VSCode (Ctrl + `)
   - Cherchez les messages d'erreur rouges
   - Notez le message exact

2. **Consultez la section "Erreurs Possibles" ci-dessus**

3. **Réinitialisez complètement:**
   ```bash
   dotnet clean
   rm -rf bin obj
   dotnet restore
   dotnet ef database drop -f
   dotnet ef database update
   dotnet run
   ```

4. **Dernier recours:** 
   - Fermez VSCode
   - Supprimez le dossier `bin` et `obj`
   - Rouvrez VSCode
   - Recommencez

---

**✨ Vous êtes prêt! Lancez l'application et testez!** 🚀
