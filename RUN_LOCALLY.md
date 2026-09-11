# 🚀 Guide Rapide - Exécuter GPA NOVEC Localement

## Option 1️⃣: Avec VSCode (Recommandé)

### Étape 1: Ouvrir dans VSCode
```bash
# Terminal (cmd, PowerShell, bash)
cd /home/user/HabibaARR
code .
```

### Étape 2: Dans VSCode
1. **Ouvrir le Terminal** → Ctrl + ` (ou Terminal → New Terminal)
2. **Exécuter la tâche de build** → Ctrl + Shift + B
   - Sélectionnez: `build`
   - Attendez la fin ✅
3. **Lancer l'app** → F5 (ou Run → Start Debugging)
   - Le navigateur s'ouvre automatiquement à `https://localhost:5001`

### Ou via la palette de commandes
- Ctrl + Shift + P
- Tape: "Run Build Task" → Sélectionne `build`
- Ctrl + Shift + P → "Run: Start Debugging" ou appuie F5

---

## Option 2️⃣: Avec Terminal (Plus Simple)

### Pour Linux/Mac:
```bash
cd /home/user/HabibaARR
chmod +x DEMARRER.sh
./DEMARRER.sh
```

### Pour Windows (PowerShell):
```powershell
cd C:\Users\...\HabibaARR
.\DEMARRER.sh
```

Ou simplement (Windows CMD/PowerShell):
```bash
dotnet run
```

---

## 🔑 Identifiants de Connexion

| Rôle | Email | Mot de Passe |
|------|-------|-------------|
| **Gestionnaire** | gestionnaire@novec.fr | Test@12345 |
| **Responsable** | responsable@novec.fr | Test@12345 |
| **Directeur** | directeur@novec.fr | Test@12345 |
| **Administrateur** | admin@novec.fr | Test@12345 |

---

## 🧪 Test Rapide du Workflow Complet

Suivez les étapes du document: `IMPLEMENTATION_SUMMARY.md`

### Résumé:
1. **Login** comme Gestionnaire
2. **Créer** un plan d'action → Ajouter une action
3. **Transmettre** le plan
4. **Login** comme Responsable
5. **Accepter** l'action
6. **Soumettre** une preuve
7. **Login** comme Gestionnaire
8. **Valider** la preuve
9. **Action** devient **CLÔTURÉE** ✅

---

## ⚠️ Si Erreur "Port 5001 Déjà Utilisé"

```bash
dotnet run --urls "https://localhost:5002"
```

---

## 📊 Accès aux Données

- **Dashboard** → Accueil
- **Actions** → Sidebar → Actions
- **Plans d'Action** → Sidebar → Plans d'action
- **Rapports** → Sidebar → Rapports & Exports
- **Base de Données** → SQL Server LocalDB (créée automatiquement)

---

## 🛠️ Troubleshooting

### "Port 5001 déjà utilisé"
```bash
dotnet run --urls "https://localhost:5002"
```

### "Database error"
```bash
dotnet ef database drop -f
dotnet ef database update
```

### ".NET not found"
Téléchargez .NET 8.0: https://dotnet.microsoft.com/download

### "Build échoue"
```bash
dotnet clean
dotnet restore
dotnet build
```

---

## 📚 Documentation

| Fichier | Pour quoi |
|---------|-----------|
| `ACTORS_WORKFLOW_DETAILED.md` | Comprendre le workflow et les rôles |
| `IMPLEMENTATION_SUMMARY.md` | Tester toutes les fonctionnalités |
| `FINAL_STATUS.md` | Voir le résumé complet |
| `TECHNICAL_SPECIFICATION.md` | Architecture technique |

---

**C'est prêt! Lancez l'app et testez le workflow complet.** 🚀
