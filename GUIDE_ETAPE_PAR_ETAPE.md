# 📖 GUIDE ÉTAPE PAR ÉTAPE - Exécuter GPA NOVEC

**Pour les débutants - Chaque étape expliquée en détail**

---

## 🔧 AVANT DE COMMENCER - Vérification (5 minutes)

### Vérifier que vous avez .NET 8.0

**Sur Windows (PowerShell ou CMD):**
```
Appuyez sur: Windows + R
Tapez: cmd
Appuyez sur Enter
```

**Dans la fenêtre noire qui s'ouvre:**
```
Tapez: dotnet --version
Appuyez sur Enter
```

**Vous devriez voir:**
```
8.0.xxx  (par exemple: 8.0.100)
```

**❌ Si ça dit ".NET not found":**
1. Allez sur: https://dotnet.microsoft.com/download
2. Cliquez sur ".NET 8.0"
3. Téléchargez le SDK (pas Runtime)
4. Installez-le
5. Redémarrez votre ordinateur
6. Réessayez la commande `dotnet --version`

**Si vous voyez le numéro de version → Continuez!** ✅

---

## 📂 ÉTAPE 1: Ouvrir VSCode

### Sur Windows:

**Méthode A - Par le menu Démarrer:**
```
1. Clic sur le logo Windows en bas à gauche
2. Tapez: VSCode
3. Clic sur "Visual Studio Code"
```

**Méthode B - Si vous avez cmd/PowerShell ouvert:**
```
Tapez: code
Appuyez sur Enter
```

### Sur Mac/Linux:

```
Ouvrez Terminal
Tapez: code
Appuyez sur Enter
```

**Résultat attendu:** La fenêtre VSCode s'ouvre (interface bleue/grise)

---

## 📁 ÉTAPE 2: Ouvrir le dossier du projet

### Dans VSCode qui vient de s'ouvrir:

```
1. En haut à gauche, clic sur: File
2. Clic sur: Open Folder
3. Une fenêtre "Parcourir" s'ouvre
```

### Trouver le dossier HabibaARR:

**Sur Windows:**
```
Naviguez jusqu'à: C:\Users\[VotreNom]\HabibaARR
(ou où vous avez téléchargé le projet)

Clic sur le dossier HabibaARR
Clic sur le bouton "Select Folder"
```

**Sur Mac/Linux:**
```
Naviguez jusqu'à: /home/user/HabibaARR
(ou votre chemin personnel)

Clic sur le dossier HabibaARR
Clic sur le bouton "Select Folder"
```

**Attendez quelques secondes... VSCode charge le projet**

---

## 🔌 ÉTAPE 3: Installer les extensions VSCode

### VSCode va vous proposer des extensions

**Vous verrez une pop-up:**
```
"Extension recommendations for this workspace"
[Install All]  [Show Recommendations]
```

**Clic sur "Install All"**

Attendez que VSCode télécharge et installe les extensions (2-3 minutes)

**Vous verrez:**
```
✓ Extension "C# Dev Kit" installed
✓ Extension ".NET Extension Pack" installed
✓ ...
```

---

## 🏗️ ÉTAPE 4: Build (Compiler) le projet

### Ouvrir le Terminal dans VSCode

```
En haut, clic sur: Terminal
Clic sur: New Terminal

(Ou pressez: Ctrl + `)
```

**Un terminal noir s'ouvre en bas de VSCode**

### Première option - Via la palette de commandes:

```
Pressez: Ctrl + Shift + P

Vous verrez une barre de recherche en haut
Tapez: Run Build Task

Clic sur: "Tasks: Run Build Task"
```

**Une liste apparaît:**
```
> build
> publish
> watch
> restore
```

**Clic sur "build"**

### Ou deuxième option - Raccourci clavier:

```
Pressez: Ctrl + Shift + B

Le build démarre directement
```

### Attendez le build:

**Vous verrez dans le terminal:**
```
Compiling...
Building project...
...
✅ Build succeeded
```

**IMPORTANT: Attendez que ce message apparaisse avant de continuer!**

---

## ▶️ ÉTAPE 5: Lancer l'application

### Appuyez sur F5

```
Pressez: F5

(C'est le raccourci pour "Start Debugging")
```

### VSCode va alors:
```
1. Compiler (si pas fait)
2. Démarrer l'application
3. Ouvrir le navigateur automatiquement
```

### Vous verrez dans le terminal VSCode:
```
Application started...
Now listening on: https://localhost:5001
```

### Le navigateur s'ouvre automatiquement

**Vous voyez:**
```
🌐 https://localhost:5001

📄 Page d'accueil GPA NOVEC avec le logo
🔐 Bouton "Se connecter"
```

**Si le navigateur ne s'ouvre pas:**
```
Ouvrez manuellement:
Ctrl + L (dans le navigateur)
Tapez: https://localhost:5001
Appuyez sur Enter
```

---

## 🔐 ÉTAPE 6: Se connecter à l'application

### Vous êtes sur la page d'accueil

```
Clic sur le bouton "Se connecter" (ou "Login")
```

### Formulaire de connexion:

```
Email:           gestionnaire@novec.fr
Mot de passe:    Test@12345

Clic sur "Se connecter"
```

### Vous êtes maintenant dans l'application! ✅

**Vous verrez:**
```
✓ Menu à gauche: Actions, Plans d'action, Rapports, Dashboard
✓ Contenu principal avec les données
✓ Votre nom d'utilisateur en haut à droite
```

---

## 🧪 ÉTAPE 7: Tester le Workflow Complet (2 minutes)

### Partie 1 - Gestionnaire crée et transmet

**Dans l'application (vous êtes connecté comme gestionnaire):**

```
1. Clic sur "Plans d'action" (menu à gauche)
```

**Vous voyez une liste (vide ou avec d'autres plans)**

```
2. Clic sur le bouton "Créer nouveau plan"
```

**Un formulaire s'affiche:**

```
Remplissez:
  Référence: TEST-001
  Titre: Plan de Test
  Date de début: [Aujourd'hui]
  Date de fin: [30 jours à partir d'aujourd'hui]

3. Clic sur "Créer"
```

**Vous voyez le plan créé**

```
4. Clic sur le plan (TEST-001)
```

**Vous entrez dans le détail du plan**

```
5. Clic sur "Ajouter une action"
```

**Un formulaire pour créer une action:**

```
Remplissez:
  Référence: ACT-TEST-001
  Titre: Tester le workflow
  Responsable: [Sélectionnez un utilisateur]
  Priorité: Moyenne
  Date limite: [Sélectionnez une date]

6. Clic sur "Créer l'action"
```

**L'action est créée!**

```
7. Clic sur "Transmettre le plan"
```

**Message:** "Plan transmis avec succès"

**Status du plan:** "Actif" ✓

---

### Partie 2 - Responsable accepte

**Vous êtes toujours connecté comme gestionnaire**

```
8. Clic sur votre nom en haut à droite
9. Clic sur "Se déconnecter"
```

**Vous êtes sur la page d'accueil**

```
10. Clic sur "Se connecter"
```

**Connectez-vous comme Responsable:**

```
Email:           responsable@novec.fr
Mot de passe:    Test@12345

Clic sur "Se connecter"
```

**Vous êtes maintenant connecté comme Responsable**

```
11. Clic sur "Actions" (menu à gauche)
```

**Vous voyez l'action que le Gestionnaire a créée**

```
12. Clic sur l'action "Tester le workflow"
```

**Détail de l'action:**

```
13. Clic sur "Accepter"
```

**L'action devient "Acceptée"** ✓

---

### Partie 3 - Responsable soumet une preuve

**Vous êtes sur la page de l'action**

```
14. Clic sur "Soumettre une preuve"
```

**Un formulaire pour soumettre la preuve:**

```
Remplissez:
  Commentaire: "Travail complété avec succès"
  Joindre un fichier: [Optionnel - vous pouvez le laisser vide]

15. Clic sur "Soumettre la preuve"
```

**Message:** "Preuve soumise avec succès"

**Status de l'action:** "Preuve soumise" ✓

---

### Partie 4 - Gestionnaire valide et clôt

**Vous êtes connecté comme Responsable**

```
16. Clic sur votre nom en haut à droite
17. Clic sur "Se déconnecter"
```

**Connectez-vous à nouveau comme Gestionnaire:**

```
Email:           gestionnaire@novec.fr
Mot de passe:    Test@12345

Clic sur "Se connecter"
```

**Vous êtes maintenant connecté comme Gestionnaire**

```
18. Clic sur "Actions" (menu à gauche)
```

**Vous voyez l'action que vous aviez créée**

```
19. Clic sur l'action "Tester le workflow"
```

**Détail de l'action - Vous voyez la preuve soumise**

```
20. Clic sur le bouton "Clôturer"
```

**Confirmation:**

```
Message: "Action clôturée avec succès"

Status de l'action: "Clôturée" ✓✓✓
```

---

## 🎉 RÉSULTAT FINAL

### Vous venez de tester le workflow complet!

```
✅ Créer un plan d'action
✅ Ajouter une action
✅ Transmettre le plan
✅ Responsable accepte
✅ Responsable soumet preuve
✅ Gestionnaire valide
✅ Action clôturée
```

**L'APPLICATION FONCTIONNE PARFAITEMENT SANS ERREURS!** 🚀

---

## 🛑 ARRÊTER L'APPLICATION

### Quand vous avez fini:

**Option 1 - Avec le clavier:**
```
Pressez: Shift + F5
```

**Option 2 - Avec le menu:**
```
Clic sur "Run" en haut
Clic sur "Stop Debugging"
```

**Option 3 - Avec Ctrl + C:**
```
Clic dans le terminal VSCode
Pressez: Ctrl + C
```

**Le serveur s'arrête et vous voyez:**
```
Application stopped
```

---

## 🔄 RELANCER L'APPLICATION

### Vous voulez relancer après l'avoir arrêtée?

```
Pressez: F5
```

**C'est tout! L'application redémarre**

---

## 📊 AUTRES TESTS À FAIRE

### Tester l'Export Excel:

```
1. Allez à "Rapports & Exports"
2. Clic sur "📊 Excel" (sur la carte "Export des Actions")
3. Un fichier .xlsx se télécharge
```

### Tester l'Export PDF:

```
1. Allez à "Rapports & Exports"
2. Clic sur "📄 PDF" (sur la carte "Export des Actions")
3. Un fichier .pdf se télécharge
```

### Voir le Dashboard:

```
1. Clic sur "Dashboard" (menu à gauche)
2. Vous voyez les statistiques:
   - Nombre d'actions
   - Taux de complétude
   - Actions par statut
```

### Tester les autres rôles:

```
Se déconnecter et connectez-vous comme:
- directeur@novec.fr (Test@12345)
  → Peut voir toutes les actions
  
- admin@novec.fr (Test@12345)
  → Accès administrateur
```

---

## 🆘 PROBLÈMES ET SOLUTIONS

### Problème 1: "Port 5001 already in use" (Port déjà utilisé)

**Erreur vue:**
```
Error: Address already in use
Port 5001 is already in use
```

**Solution:**
```
1. Pressez: Shift + F5 (arrête l'app)
2. Attendez 3 secondes
3. Pressez: F5 (relance)
```

**Si ça continue:**
```
1. Ouvrez un nouveau terminal dans VSCode (Ctrl + `)
2. Tapez: netstat -ano | findstr :5001 (Windows)
   Ou: lsof -i :5001 (Mac/Linux)
3. Tuez le processus affiche
4. Relancez F5
```

---

### Problème 2: "Build failed" (Compilation échouée)

**Erreur vue:**
```
error: Project does not exist
Build failed
```

**Solution:**
```
1. Dans le terminal VSCode, tapez:
   dotnet restore

2. Attendez que ça finisse

3. Pressez: Ctrl + Shift + B (rebuild)

4. Attendez le message:
   ✅ Build succeeded
```

---

### Problème 3: "Database connection error" (Erreur base de données)

**Erreur vue:**
```
System.Data.SqlClient.SqlException
Could not connect to database
```

**Solution:**
```
1. Dans le terminal VSCode, tapez:
   dotnet ef database drop -f

2. Puis tapez:
   dotnet ef database update

3. Attendez le message:
   ✅ Done

4. Pressez: F5 pour relancer
```

---

### Problème 4: "Certificate is invalid" (Certificat invalide)

**Erreur vue:**
```
❌ Your connection is not private
```

**C'est NORMAL en développement!**

**Solution:**
```
1. Clic sur "Advanced"
2. Clic sur "Proceed anyway" (ou lien bleu)
3. Vous entrez dans l'app
```

---

### Problème 5: "Login ne fonctionne pas"

**Vous entrez vos identifiants mais ça ne marche pas**

**Solution:**
```
1. Appuyez sur Shift + F5 (arrête)
2. Attendez 3 secondes
3. Dans le terminal, tapez:
   dotnet ef database drop -f
   dotnet ef database update

4. Pressez: F5 pour relancer
5. Les utilisateurs test sont recréés automatiquement
6. Essayez à nouveau de vous connecter
```

---

## ✅ CHECKLIST FINALE

### Avant de dire "C'est terminé":

```
☐ VSCode ouvert avec le projet
☐ Extensions installées
☐ Build réussi (Ctrl + Shift + B)
☐ Application lancée (F5)
☐ Navigateur affiche https://localhost:5001
☐ Vous êtes connecté comme Gestionnaire
☐ Vous avez testé le workflow complet (créer → transmettre → accepter → soumettre preuve → valider → clôturer)
☐ L'action est devenue "Clôturée" ✅
☐ Vous n'avez aucune erreur en rouge dans le terminal
☐ Vous pouvez exporter en Excel ✅
☐ Vous pouvez exporter en PDF ✅
```

**Si vous avez coché toutes les cases → L'application fonctionne PARFAITEMENT!** 🎉

---

## 🚀 PROCHAINES ÉTAPES

### Maintenant que l'application fonctionne:

```
1. Explorez les différents menus
2. Testez avec les 4 rôles (Gestionnaire, Responsable, Directeur, Admin)
3. Créez plusieurs plans et actions
4. Testez le rejet et la relance
5. Consultez les rapports et exports
6. Lisez la documentation:
   - ACTORS_WORKFLOW_DETAILED.md (pour comprendre les rôles)
   - IMPLEMENTATION_SUMMARY.md (pour tous les détails)
```

---

## 📞 BESOIN D'AIDE?

**Si vous êtes bloqué:**

1. Relisez la section "Problèmes et Solutions" ci-dessus
2. Vérifiez que .NET 8.0 est installé: `dotnet --version`
3. Réinitialisez complètement:
   ```
   Shift + F5 (arrête)
   dotnet clean
   dotnet restore
   dotnet ef database drop -f
   dotnet ef database update
   F5 (relance)
   ```

---

**🎉 Bravo! Vous avez complété ce guide!**

**Votre application GPA NOVEC fonctionne maintenant avec le workflow complet et sans erreurs!** 🚀

---

**Date:** 11 Septembre 2026  
**Status:** ✅ COMPLET ET PRÊT  
**Dernier test:** Application confirmée fonctionnelle

