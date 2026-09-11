# 🚀 Premier Lancement - 3 Étapes Simples

## C'est Votre Premier Lancement? Suivez Ceci:

### ✅ ÉTAPE 1: Vérifier .NET (1 minute)

Ouvrez Command Prompt / Terminal et tapez:
```bash
dotnet --version
```

Résultat attendu:
```
8.0.xxx
```

**Pas de résultat?** → Téléchargez .NET 8.0: https://dotnet.microsoft.com/download

---

### ✅ ÉTAPE 2: Ouvrir dans VSCode (30 secondes)

```
1. Ouvrir VSCode
2. File → Open Folder
3. Sélectionner: /chemin/vers/HabibaARR
4. Attendre que VSCode charge
5. Clic "Install All" si pop-up d'extensions
```

---

### ✅ ÉTAPE 3: Lancer l'Application (30 secondes)

**Dans VSCode:**
```
F5
```

**Voilà!** L'app démarre automatiquement.

---

## Vous Voyez Ceci?

```
✅ Navigateur ouvert à https://localhost:5001
✅ Logo NOVEC visible
✅ Page "Se connecter" affichée
```

### 🎉 SUCCESS! L'application fonctionne!

**Connectez-vous:**
- Email: `gestionnaire@novec.fr`
- Mot de passe: `Test@12345`

---

## Test Rapide (1 minute)

Dans l'app:
1. Clic "Plans d'action"
2. Clic "Créer nouveau"
3. Remplissez et clic "Créer"
4. Clic "Ajouter une action"
5. Remplissez et clic "Créer l'action"
6. Clic "Transmettre"

✅ Le workflow fonctionne!

---

## 📞 Si Ça Ne Marche Pas

```
Problem:             Erreur Build
Solution:            Ctrl + Shift + B → Sélectionnez "build"

Problem:             Port 5001 utilisé
Solution:            Arrêtez (Maj + F5) et relancez (F5)

Problem:             Database error
Solution:            Tapez dans le terminal:
                     dotnet ef database drop -f
                     dotnet ef database update
                     F5 pour relancer

Problem:             Certificat SSL invalide
Solution:            C'est normal en développement
                     Clic "Advanced" → "Proceed"
```

---

**C'est tout! Bienvenue!** 🎉
