# GPA NOVEC - Workflow Complet et Rôles des Acteurs

**Date:** 11 Septembre 2026  
**Version:** 2.0  
**Basé sur:** Rapport Officiel NOVEC - Chapitres 4 & 6  
**Status:** ✅ Documentation Complète

---

## 📋 Table des Matières

1. [Workflow Complet](#workflow-complet)
2. [Les 4 Acteurs](#les-4-acteurs)
3. [Gestionnaire - Rôles Détaillés](#gestionnaire--rôles-détaillés)
4. [Responsable - Rôles Détaillés](#responsable--rôles-détaillés)
5. [Directeur - Rôles Détaillés](#directeur--rôles-détaillés)
6. [Administrateur - Rôles Détaillés](#administrateur--rôles-détaillés)
7. [Matrice de Permissions](#matrice-de-permissions)
8. [Exemples de Parcours](#exemples-de-parcours)

---

## 🔄 Workflow Complet

### Diagramme d'État Global

```
┌─────────────────────────────────────────────────────────────────┐
│                   CYCLE DE VIE D'UNE ACTION                      │
└─────────────────────────────────────────────────────────────────┘

                    ┌──────────────┐
                    │  1. BROUILLON │  ← Gestionnaire crée
                    │    (Draft)    │
                    └──────┬───────┘
                           │
                    [Gestionnaire transmet]
                           │
                    ┌──────▼──────────┐
                    │ 2. NOUVELLE     │ ← Gestionnaire attend réaction
                    │  (New)          │
                    └──────┬──────────┘
                           │
            ┌──────────────┴──────────────┐
            │                             │
    [Responsable accepte]      [Responsable refuse]
            │                             │
      ┌─────▼──────┐            ┌────────▼─────┐
      │ 3. ACCEPTÉE │            │ 4. REJETÉE   │
      │ (Accepted)  │            │ (Rejected)   │
      └─────┬───────┘            └────────┬─────┘
            │                             │
    [Responsable agit]      [Gestionnaire modifie/relance]
            │                             │
      ┌─────▼───────────┐                │
      │ 5. EN COURS     │                │
      │ (In Progress)   │ ◄──────────────┘
      └─────┬───────────┘
            │
   [Responsable soumet preuve]
            │
      ┌─────▼──────────────────┐
      │ 6. PREUVE SOUMISE      │ ← Gestionnaire examine
      │ (EvidenceSubmitted)    │
      └─────┬────────┬──────────┘
            │        │
    [Validation OK]  [Validation NOK]
            │        │
      ┌─────▼──┐   ┌─▼──────────────────┐
      │ 7. ✅   │   │ 8. PREUVE REJETÉE  │
      │CLÔTURÉE │   │ (EvidenceRejected) │
      │(Completed)  └─┬──────────────────┘
      └────────┘      │
                      [Responsable réagit - retour étape 5]
                      │
                      └──► EN COURS (In Progress)
```

### Les 8 États de l'Action

| # | État FR | État EN | Acteur en Charge | Description |
|---|---------|---------|------------------|-------------|
| 1 | Brouillon | Draft | **Gestionnaire** | Action créée, en attente de transmission |
| 2 | Nouvelle | New | **Responsable** | Transmise au Responsable, en attente de réponse |
| 3 | Acceptée | Accepted | **Responsable** | Responsable accepte l'action, passe à l'exécution |
| 4 | Rejetée | Rejected | **Gestionnaire** | Responsable refuse, Gestionnaire peut modifier |
| 5 | En Cours | InProgress | **Responsable** | Action en cours de réalisation |
| 6 | Preuve Soumise | EvidenceSubmitted | **Gestionnaire** | Responsable a soumis une preuve, en attente de validation |
| 7 | Clôturée | Completed | **Aucun** | Action validée et clôturée (archivée) |
| 8 | Preuve Rejetée | EvidenceRejected | **Responsable** | Preuve invalide, Responsable doit recommencer |

### Transitions d'État Autorisées

```
Brouillon (1)
    └─→ Nouvelle (2)                    [par Gestionnaire: "Transmettre"]

Nouvelle (2)
    ├─→ Acceptée (3)                    [par Responsable: "Accepter"]
    └─→ Rejetée (4)                     [par Responsable: "Rejeter"]

Acceptée (3)
    └─→ En Cours (5)                    [implicite lors de la première action]

Rejetée (4)
    └─→ En Cours (5)                    [par Gestionnaire: "Réactiver"]

En Cours (5)
    └─→ Preuve Soumise (6)              [par Responsable: "Soumettre preuve"]

Preuve Soumise (6)
    ├─→ Clôturée (7)                    [par Gestionnaire: "Clôturer/Valider"]
    └─→ Preuve Rejetée (8)              [par Gestionnaire: "Rejeter preuve"]

Preuve Rejetée (8)
    └─→ En Cours (5)                    [par Responsable: "Relancer"]

Clôturée (7)
    └─→ [TERMINÉE - ARCHIVÉE]           [Pas de transition possible]
```

---

## 👥 Les 4 Acteurs

### Vue d'Ensemble

| Acteur | Objectif Principal | Traduction Fonctionnelle | Symbole |
|--------|-------------------|--------------------------|---------|
| **Gestionnaire** | Créer, piloter et clôturer les plans d'action | Responsable de la mise en œuvre globale du processus | 📋 |
| **Responsable** | Exécuter et justifier l'accomplissement des actions | Responsable de la réalisation technique | ✓ |
| **Directeur** | Superviser et valider les performances globales | Lecteur avec pouvoir de décision final | 👁 |
| **Administrateur** | Gérer la plateforme et les utilisateurs | Support technique et configuration | ⚙️ |

---

## 📋 Gestionnaire – Rôles Détaillés

**Objectif Principal:** Créer, piloter et clôturer les plans d'action  
**Rôle:** Pilote du processus GPA, responsable de bout en bout

### Permissions Globales

✅ Créer des plans d'action  
✅ Créer des actions dans les plans  
✅ Voir toutes les actions (filtrées par plan si nécessaire)  
✅ Transmettre les plans (changer statut)  
✅ Modifier les actions (titre, description, date limite)  
✅ Valider ou rejeter les preuves soumises  
✅ Clôturer les actions validées  
✅ Exporter les données (Excel + PDF)  
✅ Voir le Dashboard (métriques)  

❌ Exécuter les actions (c'est le rôle du Responsable)  
❌ Voir les détails internes des preuves non soumises  

### Rôles par État de l'Action

#### État 1: BROUILLON (Draft)
- **Acteur en charge:** Gestionnaire
- **Actions possibles:**
  - ✏️ Créer une nouvelle action
  - 🔄 Modifier l'action (titre, description, référence, responsable, date limite)
  - 🗑️ Supprimer l'action
  - ➡️ **Transmettre le plan** (changement d'état: Brouillon → Nouvelle)
- **Informations visibles:**
  - Référence, titre, description complète
  - Responsable assigné
  - Dates (création, limite)
  - Priorité et statut
- **Décisions à prendre:**
  - Qui sera responsable de cette action?
  - Quelle est la date limite réaliste?
  - Quels sont les critères d'acceptation?

#### État 2: NOUVELLE (New)
- **Acteur en charge:** Responsable (mais Gestionnaire surveille)
- **Actions du Gestionnaire:**
  - 👁 **Observer** l'action en attente de réaction
  - 📊 Voir le statut "Responsable n'a pas encore réagi"
  - ⏰ Surveiller les délais de réaction
  - 📝 **Ajouter des commentaires** pour clarifier
  - 🔔 **Envoyer des rappels** (si feature disponible)
- **Informations visibles:**
  - Toutes les informations de l'action
  - Qui (Responsable) doit réagir
  - Délai de réaction attendu
- **Décisions:**
  - Est-ce que le Responsable a besoin de clarifications?
  - Faut-il rappeler le Responsable?

#### État 3: ACCEPTÉE (Accepted)
- **Acteur en charge:** Responsable (exécution)
- **Actions du Gestionnaire:**
  - 👁 Suivre la progression
  - 📊 Voir le pourcentage de complétude
  - 📝 Lire le journal d'action (notes du Responsable)
  - ✋ **Mettre en pause** ou **redémarrer** si nécessaire (rôle d'administrateur)
- **Informations visibles:**
  - Statut "En attente de réalisation"
  - Journal des mises à jour du Responsable
  - Progression estimée
- **Décisions:**
  - La progression est-elle normale?
  - Faut-il intervenir?

#### État 4: REJETÉE (Rejected)
- **Acteur en charge:** Gestionnaire (peut modifier)
- **Actions possibles:**
  - 📝 Lire le motif du rejet (fourni par Responsable)
  - ✏️ **Modifier l'action** (ajuster les critères, date, responsable)
  - ➡️ **Réactiver l'action** (la retransmettre)
  - 💬 **Commenter** pour discuter du rejet
- **Informations visibles:**
  - Motif exact du rejet
  - Feedback du Responsable
  - Détails originaux de l'action
- **Décisions:**
  - Comment adapter l'action pour qu'elle soit acceptable?
  - La date limite est-elle réaliste?
  - Le Responsable a-t-il besoin de support?

#### État 5: EN COURS (InProgress)
- **Acteur en charge:** Responsable (exécute)
- **Actions du Gestionnaire:**
  - 👁 **Suivre l'avancement**
  - 📊 Voir le journal d'action complet
  - 📞 **Communiquer** avec le Responsable via les commentaires
  - ⏱️ Surveiller les délais (warning si approche de l'échéance)
- **Informations visibles:**
  - Progression en temps réel
  - Dernière mise à jour du Responsable
  - Jours restants jusqu'à l'échéance
  - Tous les commentaires et journaux
- **Décisions:**
  - La progression est-elle satisfaisante?
  - Faut-il redéployer des ressources?

#### État 6: PREUVE SOUMISE (EvidenceSubmitted)
- **Acteur en charge:** Gestionnaire (valide/rejette)
- **Actions essentielles:**
  - 📎 **Consulter la preuve** (fichier attaché)
  - 📋 **Lire le commentaire** d'accompagnement
  - ✅ **VALIDER** → Action devient CLÔTURÉE
  - ❌ **REJETER** → Action devient PREUVE REJETÉE
  - 💬 **Commenter** sur la validité de la preuve
- **Informations visibles:**
  - Preuve attachée (document, fichier)
  - Commentaire explicatif du Responsable
  - Historique complet de l'action
  - Dates de soumission
- **Décisions critiques:**
  - La preuve démontre-t-elle que l'action est complétée?
  - Respecte-t-elle les critères d'acceptation?
  - Faut-il des clarifications supplémentaires?
  - L'action peut-elle être clôturée?

#### État 7: CLÔTURÉE (Completed)
- **Acteur en charge:** Aucun (archivée)
- **Actions du Gestionnaire:**
  - 👁 **Consulter** l'action complétée (lecture seule)
  - 📊 Inclure dans les rapports
  - 📥 **Exporter** comme fait accompli
  - 📈 Contribuer aux statistiques
- **Informations visibles:**
  - Historique complet
  - Date de clôture
  - Preuve finale validée
  - KPIs (Taux de réussite, temps d'exécution)

#### État 8: PREUVE REJETÉE (EvidenceRejected)
- **Acteur en charge:** Responsable (doit réagir)
- **Actions du Gestionnaire:**
  - 💬 **Commenter** sur le motif du rejet
  - 📝 **Clarifier les critères** attendus
  - 👁 Surveiller que le Responsable relance
- **Informations visibles:**
  - Preuve précédente rejetée
  - Critères d'acceptation
  - Commentaires explicatifs
- **Décisions:**
  - Quels critères précis manquaient?
  - Faut-il modifier les critères pour faire sens?

---

## ✓ Responsable – Rôles Détaillés

**Objectif Principal:** Exécuter et justifier l'accomplissement des actions  
**Rôle:** Exécuteur technique, responsable de la réalisation

### Permissions Globales

✅ Voir les actions qui vous sont assignées  
✅ Accepter ou refuser une action qui vous est confiée  
✅ Mettre à jour le journal d'action (commentaires, progression)  
✅ Soumettre une preuve quand l'action est terminée  
✅ Joindre des fichiers de preuve  
✅ Exporter les données (Excel + PDF)  
✅ Voir le Dashboard (ses propres actions)  

❌ Voir toutes les actions (seulement les siennes)  
❌ Créer des actions  
❌ Valider des preuves  
❌ Clôturer des actions  

### Rôles par État de l'Action

#### État 2: NOUVELLE (New)
- **Acteur en charge:** Responsable
- **Actions essentielles:**
  - 📖 **LIRE** l'action avec tous les critères de réussite
  - 💬 **Poser des questions** via commentaires si besoin de clarification
  - ✅ **ACCEPTER** l'action → (transition vers État 3: ACCEPTÉE)
  - ❌ **REFUSER** l'action + motif → (transition vers État 4: REJETÉE)
- **Informations visibles:**
  - Titre et description complète de l'action
  - Date limite
  - Critères de succès
  - Priorité
  - Responsable (vous-même)
- **Décisions critiques:**
  - Puis-je réaliser cette action?
  - Est-ce réaliste dans le délai?
  - Ai-je besoin de clarifications?
  - Quels sont exactement les critères d'acceptation?

**Cas d'usage 1:** Responsable accepte
```
Action NOUVELLE (2)
   → Responsable clique "Accepter"
   → Action devient ACCEPTÉE (3)
   → Responsable peut maintenant commencer le travail
```

**Cas d'usage 2:** Responsable refuse
```
Action NOUVELLE (2)
   → Responsable clique "Rejeter"
   → Responsable saisit le motif du rejet
   → Action devient REJETÉE (4)
   → Gestionnaire peut modifier et relancer
```

#### État 3: ACCEPTÉE (Accepted)
- **Acteur en charge:** Responsable (exécution)
- **Actions possibles:**
  - 🚀 **Commencer le travail**
  - 📝 **Mettre à jour le journal** avec les étapes complétées
  - 📊 **Mettre à jour la progression** (%)
  - 💬 **Poster des commentaires** d'avancement
  - 📎 **Joindre des fichiers intermédiaires** (optionnel)
- **Informations visibles:**
  - Statut "En attente de démarrage"
  - Critères d'acceptation
  - Date limite
  - Journal des mises à jour
- **Décisions:**
  - Par où commencer?
  - Quelles étapes faut-il documenter?

#### État 5: EN COURS (InProgress)
- **Acteur en charge:** Responsable (exécution)
- **Actions essentielles:**
  - 📝 **Mettre à jour le journal** régulièrement
  - 📊 **Augmenter la progression** au fur et à mesure
  - 💬 **Documenter les étapes clés**
  - 📅 **Signaler les retards** éventuels
  - 🆘 **Demander du support** si problème
  - ➡️ **Passer à PREUVE SOUMISE** quand terminé
- **Informations visibles:**
  - État d'avancement actuel
  - Journal complet
  - Jours restants
  - Historique des commentaires
- **Décisions:**
  - Suis-je en retard?
  - Quelles preuves vais-je documenter?
  - Quand serai-je prêt à soumettre?

**Exemple de journal d'action:**
```
[Jour 1] Démarrage des travaux
- Analyser les spécifications
- Préparer les outils
Progression: 10%

[Jour 3] Implémentation
- Code principal écrit
- Tests unitaires en cours
Progression: 40%

[Jour 5] Tests et peaufinage
- Tests de régression complétés
- Documentation finalisée
Progression: 90%

[Jour 6] Prêt à soumettre
- Tous les critères sont satisfaits
Progression: 100%
```

#### État 6: PREUVE SOUMISE (EvidenceSubmitted)
- **Acteur en charge:** Gestionnaire (valide)
- **Actions du Responsable:**
  - ⏸️ **Attendre la validation**
  - 💬 **Être disponible** pour répondre à des questions
  - 🔔 **Être alerté** une fois le résultat connu
- **Informations visibles:**
  - Preuve soumise
  - Commentaire d'accompagnement
  - Statut "En attente de validation"
- **Rôle passif:**
  - Le Responsable attend la décision du Gestionnaire

#### État 7: CLÔTURÉE (Completed)
- **Acteur en charge:** Aucun
- **Actions du Responsable:**
  - 👁 **Consulter** l'action clôturée (archives)
  - 📊 Contribuer aux statistiques personnelles
- **Informations visibles:**
  - Action terminée avec succès
  - Date de clôture
  - KPIs personnels

#### État 8: PREUVE REJETÉE (EvidenceRejected)
- **Acteur en charge:** Responsable (relance)
- **Actions essentielles:**
  - 📖 **LIRE** le motif du rejet
  - 💬 **Comprendre les critères** manquants
  - ✏️ **Relancer l'action** (= retour à EN COURS)
  - 📝 **Soumettre une nouvelle preuve**
- **Informations visibles:**
  - Raison du rejet
  - Critères d'acceptation non satisfaits
  - Preuve précédente
  - Commentaires du Gestionnaire
- **Décisions:**
  - Comment satisfaire les critères manquants?
  - Quoi inclure dans la nouvelle preuve?

**Cas d'usage - Boucle d'amélioration:**
```
Preuve Rejetée (8)
   → Responsable lit le motif: "Documentation manquante"
   → Responsable retourne à EN COURS (5)
   → Responsable complète la documentation
   → Responsable change de nouveau la progression à 100%
   → Responsable soumet une nouvelle preuve
   → Retour à PREUVE SOUMISE (6)
```

---

## 👁 Directeur – Rôles Détaillés

**Objectif Principal:** Superviser et valider les performances globales  
**Rôle:** Superviseur/Observateur, lecteur de rapports

### Permissions Globales

✅ Voir toutes les actions (lecture seule)  
✅ Voir les plans d'action (lecture seule)  
✅ Accéder aux rapports et exports  
✅ Consulter le Dashboard (vue d'ensemble)  
✅ Exporter les données (Excel + PDF)  
✅ Voir les KPIs globaux  

❌ Modifier les actions  
❌ Créer les actions  
❌ Valider les preuves  
❌ Clôturer les actions  
❌ Accéder aux données de base de données brutes  

### Rôles par État de l'Action

#### À tous les états (1-8)
- **Acteur en charge:** Observer (lecture seule)
- **Actions possibles:**
  - 👁 **CONSULTER** l'action
  - 📊 **LIRE** l'historique complet
  - 📋 **CONSULTER** le journal d'action
  - 📈 **ANALYSER** la progression
  - 💬 **LIRE** les commentaires
  - 📥 **EXPORTER** les données pour analyse externe
- **Informations visibles:**
  - TOUTES les informations (comme le Gestionnaire)
  - Vue complète du workflow
  - Historique des modifications
  - Toutes les preuves soumises
- **Décisions:**
  - Cet action est-elle sur la bonne trajectoire?
  - Le processus fonctionne-t-il correctement?
  - Y a-t-il des goulots d'étranglement?

### Cas d'usage du Directeur

**Scénario 1: Supervision Périodique**
```
Directeur accède au Dashboard
   → Voit: 45 actions en cours, 12 complétées, 3 en retard
   → Clique sur "Actions en retard"
   → Voit quelles actions dépassent la date limite
   → Consulte les détails
   → Évalue si intervention Gestionnaire nécessaire
   → Prend connaissance pour rapports à la direction
```

**Scénario 2: Rapport Mensuel**
```
Directeur clique "Exporter les données" (PDF)
   → Reçoit rapport complet: 
      - Nombre d'actions par état
      - Taux de complétude
      - Taux de rejet
      - Délais moyens de réalisation
   → Analyse et présente à la direction
```

**Scénario 3: Investigation d'Anomalie**
```
Directeur remarque un taux de rejet élevé (40% vs normal 10%)
   → Consulte les actions rejetées
   → Lit les motifs de rejet
   → Analyse les commentaires
   → Identifie le problème (ex: critères d'acceptation flous)
   → Signale au Gestionnaire pour correction
```

---

## ⚙️ Administrateur – Rôles Détaillés

**Objectif Principal:** Gérer la plateforme et les utilisateurs  
**Rôle:** Support technique et administrateur système

### Permissions Globales

✅ Voir toutes les actions (lecture + écriture)  
✅ Gérer les utilisateurs (créer, modifier, supprimer)  
✅ Gérer les rôles et permissions  
✅ Accéder aux logs d'audit complets  
✅ Configurer les paramètres système  
✅ Effectuer les sauvegardes  
✅ Exporter tous les données  

### Rôles par État de l'Action

#### À tous les états (1-8)
- **Acteur en charge:** Administrateur système
- **Actions possibles:**
  - 👁 **CONSULTER** l'action
  - ✏️ **MODIFIER** l'action (si nécessaire - pas recommandé en production)
  - 🔧 **FORCER** un changement d'état (override)
  - 📊 **AUDITER** toutes les modifications
  - 🗑️ **SUPPRIMER** (exceptionnellement)
  - 👤 **GÉRER** les droits d'accès
- **Rôles de support:**
  - 👥 Créer/modifier/supprimer les utilisateurs
  - 🔐 Réinitialiser les mots de passe
  - 📋 Gérer les rôles (Admin, Directeur, Gestionnaire, Responsable)
  - 🔔 Configurer les notifications
  - 💾 Effectuer les sauvegardes
  - 📊 Exporter les données compètes

### Cas d'usage de l'Administrateur

**Support Utilisateur:**
```
Un Responsable oublie son mot de passe
   → Administrateur:
      - Localise l'utilisateur
      - Initie "Réinitialiser le mot de passe"
      - Envoie lien de reset à l'utilisateur
      - Vérifie que l'utilisateur peut se reconnecter
```

**Audit d'Anomalie:**
```
Gestionnaire déclare avoir validé une action, mais elle n'est pas clôturée
   → Administrateur:
      - Accède aux logs d'audit complets
      - Vérifie la séquence d'événements
      - Voit exactement ce qui s'est passé (timing, IP, etc.)
      - Identifie si c'est un bug ou une erreur utilisateur
      - Prend l'action correctrice
```

**Maintenance:**
```
Migration vers nouvelle base de données
   → Administrateur:
      - Exporte toutes les données
      - Effectue la migration
      - Valide l'intégrité des données
      - Réinitialise les connexions
      - Notifie les utilisateurs
```

---

## 📊 Matrice de Permissions

### Permissions par Rôle et Action

```
RÔLE\PERMISSION          | CRÉER | MODIFIER | VALIDER | CLÔTURER | VOIR_TOUS | EXPORT | DASHBOARD |
------------------------+-------+----------+---------+----------+----------+--------+-----------+
Gestionnaire             |  ✅   |    ✅    |   ✅    |    ✅    |    ✅    |  ✅    |    ✅     |
Responsable              |  ❌   |    ⚠️    |   ❌    |    ❌    |    ❌*   |  ✅    |    ✅*    |
Directeur                |  ❌   |    ❌    |   ❌    |    ❌    |    ✅    |  ✅    |    ✅     |
Administrateur           |  ✅   |    ✅    |   ✅    |    ✅    |    ✅    |  ✅    |    ✅     |
```

**Légende:**
- ✅ = Permission complète
- ❌ = Pas de permission
- ⚠️ = Permission partielle (seulement ses propres actions)
- \* = Voir seulement ses propres actions assignées

### Détails des Permissions du Responsable

```
ACTION                  | PERMISSION | NOTES
------------------------+------------+----------------------------------
Créer une action        |    ❌      | Seulement Gestionnaire crée
Modifier sa propre      |    ⚠️      | Peut mettre à jour le journal
action (journal)        |            | et la progression
Modifier l'action       |    ❌      | Gestionnaire seul
(titre, date, etc.)    |            |
Accepter l'action       |    ✅      | Oui, c'est son rôle clé
Rejeter l'action        |    ✅      | Oui, avec motif
Soumettre une preuve    |    ✅      | Oui, quand terminée
Valider une preuve      |    ❌      | Rôle du Gestionnaire
Clôturer une action     |    ❌      | Rôle du Gestionnaire
Voir ses actions        |    ✅      | Uniquement les siennes
Voir TOUTES les actions |    ❌      | Pas d'accès global
Exporter ses actions    |    ✅      | Excel/PDF filtrés
Voir Dashboard          |    ✅      | Ses propres stats
```

---

## 🔄 Exemples de Parcours Complets

### 📖 Exemple 1: Workflow Nominal (Succès du 1er coup)

```
┌─────────────────────────────────────────────────────────────────┐
│   CAS NOMINAL: Action créée, acceptée, complétée du premier coup │
└─────────────────────────────────────────────────────────────────┘

JOUR 1 - Gestionnaire crée un plan et une action
═══════════════════════════════════════════════════════════════════
Gestionnaire:
  ✏️ Crée un plan: "Audit IT 2026"
  ✏️ Crée une action: "Tester la sécurité du serveur"
     - Référence: ACT-2026-SEC-001
     - Responsable: Alice (Responsable technique)
     - Date limite: 15 septembre 2026
     - Critères: Rapport d'audit complété et validé
  Status d'action: 1. BROUILLON

JOUR 1 - Gestionnaire transmet
═════════════════════════════════
Gestionnaire:
  ➡️ Clique "Transmettre le plan"
  Status du plan: ACTIF
  Status d'action: 2. NOUVELLE
  ⏰ Responsable doit réagir sous 48h

JOUR 2 - Responsable accepte
══════════════════════════════
Alice (Responsable):
  👁 Lit l'action
  ✅ Clic "Accepter"
  Status d'action: 3. ACCEPTÉE
  📝 Le travail peut commencer

JOURS 3-10 - Responsable travaille
═════════════════════════════════════
Alice (Responsable):
  [Jour 3] 📝 Journal: "Téléchargé outils de test, préparation en cours"
           📊 Progression: 20%
  [Jour 5] 📝 Journal: "Tests de pénétration lancés"
           📊 Progression: 50%
  [Jour 8] 📝 Journal: "Analyse des résultats en cours"
           📊 Progression: 80%
  Status d'action: 5. EN COURS

JOUR 10 - Responsable soumet preuve
═════════════════════════════════════
Alice (Responsable):
  📎 Jointe le fichier: "Rapport_Audit_IT_Final.pdf"
  💬 Commentaire: "Audit complété. 2 vulnérabilités trouvées et 
                  documentées. Recommandations incluses."
  ➡️ Clique "Soumettre la preuve"
  Status d'action: 6. PREUVE SOUMISE

JOUR 11 - Gestionnaire valide
════════════════════════════════
Gestionnaire:
  👁 Consulte la preuve
  📋 Lit le rapport d'audit
  ✅ Clique "Valider"
  Status d'action: 7. CLÔTURÉE
  📊 Ajoute aux statistiques de réussite

RÉSULTAT FINAL
═══════════════
✅ Action complétée en 10 jours
✅ Preuve acceptée du premier coup
✅ 0 itération
✅ Taux de réussite 100%
```

---

### ❌ Exemple 2: Workflow avec Rejet initial

```
┌─────────────────────────────────────────────────────────────────┐
│   CAS DE REJET: Responsable refuse la tâche à la réception       │
└─────────────────────────────────────────────────────────────────┘

JOUR 1 - Gestionnaire crée et transmet
═══════════════════════════════════════════════════════════════════
Gestionnaire:
  ✏️ Crée action: "Rédiger documentation API"
  ➡️ Transmet le plan
  Status d'action: 2. NOUVELLE

JOUR 2 - Responsable REFUSE
══════════════════════════════
Bob (Responsable):
  👁 Lit l'action
  ❌ Clic "Rejeter"
  📝 Motif: "Date limite impossible. Projet démarre seulement 
             le 20 septembre. Demande 10 jours minimum."
  Status d'action: 4. REJETÉE

JOUR 2 - Gestionnaire corrige et relance
══════════════════════════════════════════════
Gestionnaire:
  📖 Lit le motif du rejet
  💭 Convient que la date limite est effectivement trop proche
  ✏️ Modifie l'action:
     - Date limite: 5 octobre (au lieu de 20 septembre)
     - Descriptif: Ajoute clarification sur les exigences
  ➡️ Clic "Réactiver / Relancer"
  Status d'action: 5. EN COURS (relancé)

JOUR 2 - Responsable relit et accepte tacitement
══════════════════════════════════════════════════════
Bob (Responsable):
  👁 Notification: "Action relancée avec modifications"
  👁 Voit la nouvelle date limite: 5 octobre
  ✅ Clic "Accepter"
  Status d'action: 3. ACCEPTÉE

JOUR 3-15 - Travail avec date réaliste
═════════════════════════════════════════
Bob (Responsable):
  [Jour 3] 📝 Commencement du travail
  [Jour 10] 📝 Première version rédigée (progression 60%)
  [Jour 12] 📝 Révision et exemples code (progression 85%)
  [Jour 13] 📝 Documentation finalisée et testée (progression 100%)
  Status d'action: 5. EN COURS

JOUR 13 - Preuve soumise
═════════════════════════
Bob (Responsable):
  📎 Jointe: "API_Documentation_Final.docx"
  ➡️ Soumet la preuve
  Status d'action: 6. PREUVE SOUMISE

JOUR 14 - Validation et clôture
═════════════════════════════════
Gestionnaire:
  👁 Consulte la documentation
  ✅ Clique "Valider"
  Status d'action: 7. CLÔTURÉE

RÉSULTAT FINAL
═══════════════
✅ Action complétée (secondaire)
⚠️  Une itération initiale (rejet + correction)
✅ Date limite réaliste appliquée
✅ Qualité préservée (pas de rush)
```

---

### 🔁 Exemple 3: Workflow avec Rejet de Preuve

```
┌─────────────────────────────────────────────────────────────────┐
│   CAS REJET DE PREUVE: La preuve ne répond pas aux critères     │
└─────────────────────────────────────────────────────────────────┘

[Jours 1-8: Même parcours que exemple 1 jusqu'à "Preuve soumise"]

JOUR 9 - Responsable soumet preuve (mais incomplète)
════════════════════════════════════════════════════════
Charlie (Responsable):
  📎 Jointe: "Test_Results.txt" (fichier basique)
  💬 Commentaire: "Tests complétés. Résultats en fichier."
  Status d'action: 6. PREUVE SOUMISE

JOUR 10 - Gestionnaire REJETTE la preuve
═══════════════════════════════════════════════════
Gestionnaire:
  👁 Consulte la preuve
  ❌ Fichier trop basique (pas formaté, pas professionnel)
  ❌ Clic "Rejeter la preuve"
  💬 Motif: "Preuve insuffisante. Besoin:
            1. Rapport formaté avec sections (intro, méthodologie)
            2. Graphs/tableaux des résultats
            3. Conclusions et recommandations
            4. Format PDF professionnel"
  Status d'action: 8. PREUVE REJETÉE

JOUR 11 - Responsable relance et améliore
═════════════════════════════════════════════
Charlie (Responsable):
  📖 Lit les critères manquants
  ✏️ Améliore le rapport:
     - Réorganise en sections professionnelles
     - Ajoute des graphiques
     - Rédige conclusions
     - Exporte en PDF
  📝 Met à jour la progression: 100%
  Status d'action: 5. EN COURS (relancé)

JOUR 12 - Responsable soumet nouvelle preuve
══════════════════════════════════════════════════
Charlie (Responsable):
  📎 Jointe: "Rapport_Tests_Complet_v2.pdf"
  💬 Commentaire: "Preuve révisée avec tous les critères:
                   - Sections d'introduction et méthodologie
                   - Graphs des résultats
                   - Conclusions professionnelles"
  ➡️ Clique "Soumettre la preuve"
  Status d'action: 6. PREUVE SOUMISE (2e tentative)

JOUR 13 - Gestionnaire valide
════════════════════════════════
Gestionnaire:
  👁 Consulte le nouveau rapport
  ✅ Clique "Valider"
  Status d'action: 7. CLÔTURÉE

RÉSULTAT FINAL
═══════════════
✅ Action complétée (mais avec itération)
⚠️  Une tentative échouée, relancée, réussie
✅ Norme de qualité appliquée
✅ Apprentissage pour les fois suivantes
```

---

### 👁 Exemple 4: Rôle du Directeur

```
┌─────────────────────────────────────────────────────────────────┐
│   VUE DIRECTEUR: Supervision mensuelle et reporting              │
└─────────────────────────────────────────────────────────────────┘

DÉBUT DE MOIS - Directeur accède au Dashboard
═════════════════════════════════════════════════
Directeur (M. Dubois):
  👁 Voit aperçu globale:
    • Total actions: 150
    • Complétées: 120 (80%)
    • En cours: 20 (13%)
    • Rejetées/en retard: 10 (7%)
    
  ⚠️  Identifie:
    - 3 actions dépassent la date limite
    - 2 actions en retard de plus de 5 jours
    - Taux de rejet de preuve: 5% (normal: 3%)

MILIEU DE MOIS - Investigation
═════════════════════════════════
Directeur:
  👁 Consulte les 3 actions en retard
  📖 Lit les détails complets pour chacune:
    • ACT-001: Raison: Responsable malade 3 jours
    • ACT-002: Raison: Dépendance non livrée à temps
    • ACT-003: Raison: Critères flous, rejeté 2 fois
  
  💭 Analyse:
    - ACT-001 & 002: Imprévus normaux, rien à faire
    - ACT-003: Problème récurrent de critères flous
    
  ➡️  Signale au Gestionnaire: "Besoin de clarifier 
      les critères pour la famille de projets IT"

FIN DE MOIS - Génération du rapport
═════════════════════════════════════
Directeur:
  📥 Clique "Exporter rapport PDF"
  📊 Génère document avec:
    • Nombre actions complétées: 125 (vs 120 prévues)
    • Taux de réussite: 82%
    • Délai moyen: 8 jours (vs 10 prévus)
    • Taux de rejet: 5% (vs 3% cible)
    • 3 actions en retard à suivre
    
  📈 Prépare présentation pour la direction

SUIVI GESTIONNAIRE
═════════════════════════════════════════════════
Directeur → Gestionnaire:
  "J'ai remarqué que les actions IT ont 
   un taux de rejet plus élevé. Besoin de 
   revoir les critères d'acceptation?"

Gestionnaire → Directeur:
  "Oui, convenu. Vais clarifier les 
   critères avec le Responsable IT."

RÉSULTAT
═════════
✅ Directeur effectue supervision passive
✅ Identifie problèmes sans intervention directe
✅ Génère rapports pour la direction
✅ Contribue à l'amélioration continue
```

---

## 📝 Synthèse des Rôles

### Par Action dans le Workflow

| État | État FR | Gestionnaire | Responsable | Directeur | Admin |
|------|---------|--------------|-------------|-----------|-------|
| 1 | BROUILLON | **CRÉE** | Voir | Voir | Voir |
| 2 | NOUVELLE | Surveille | **ACCEPTE/REFUSE** | Voir | Voir |
| 3 | ACCEPTÉE | Surveille | **EXÉCUTE** | Voir | Voir |
| 4 | REJETÉE | **MODIFIE** | Attend | Voir | Voir |
| 5 | EN COURS | Suit progrès | **TRAVAILLE** | Voir | Voir |
| 6 | PREUVE SOUMISE | **VALIDE** | Attend | Voir | Voir |
| 7 | CLÔTURÉE | Archive | Consulte | Consulte | Consulte |
| 8 | PREUVE REJETÉE | Commente | **RELANCE** | Voir | Voir |

---

## ✨ Points Clés à Retenir

### 1. **Gestionnaire = Pilote**
   - Crée et transmet les plans et actions
   - Surveille la progression
   - Valide les preuves
   - Clôture les actions
   - Rôle de contrôle et de pilotage

### 2. **Responsable = Exécuteur**
   - Accepte/refuse les actions assignées
   - Exécute le travail
   - Soumet les preuves
   - Voit uniquement ses propres actions
   - Rôle d'exécution technique

### 3. **Directeur = Superviseur**
   - Accès en lecture à TOUT
   - Génère rapports et statistiques
   - Identifie anomalies
   - Pas d'actions directes
   - Rôle de pilotage stratégique

### 4. **Administrateur = Support**
   - Gère les utilisateurs et rôles
   - Audit complet
   - Support technique
   - Rôle d'infrastructure

### 5. **Transitions d'État**
   - Brouillon → Nouvelle : Gestionnaire transmet
   - Nouvelle → Acceptée/Rejetée : Responsable choisit
   - En Cours → Preuve Soumise : Responsable termine
   - Preuve Soumise → Clôturée : Gestionnaire valide
   - Preuve Rejetée → En Cours : Responsable relance

---

**Date de création:** 11 Septembre 2026  
**Basé sur:** Rapport Officiel NOVEC Chapitres 4 & 6  
**Version:** 2.0 - Complète  
**Status:** ✅ Documentation Finalisée

---

**FIN DU DOCUMENT**
