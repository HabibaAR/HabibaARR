# 📋 WIP - PLATEFORME GPA NOVEC v2.2026
## Conception & Développement - Plateforme de Gestion des Plans d'Action

**Statut:** WIP (Work In Progress)  
**Version:** 2.0  
**Date:** 2025-2026  
**Basé sur:** PROMPT MAÎTRE + Rapport de Stage (53 pages)  
**Réalisé par:** Habiba Arouada  

---

## 🎯 CONTEXTE ET OBJECTIF

### L'Enjeu
La digitalisation du suivi des plans d'action répond à trois enjeux critiques:
- **Visibilité** → Où sommes-nous à tout instant?
- **Responsabilité** → Qui est en charge et sur quelle base?
- **Traçabilité** → Comment avons-nous atteint les résultats?

### Problématique Initiale
NOVEC gère les plans d'action par:
1. **Tableur partagé sur serveur de fichiers** → versions multiples, incohérences
2. **Chaîne de courriels** → pas de lien, responsabilité floue
3. **Mémoire des personnes** → perte d'information
4. **Clôture déclarative** → pas de justificatif, traçabilité faible

### Objectif Fonctionnel
Transformer un suivi déclaratif et dispersé en un processus structuré où:
- ✅ Chaque action a un état connu
- ✅ Chaque responsable est identifié
- ✅ Chaque décision est enregistrée
- ✅ Chaque transition produit une trace horodatée

---

## 🏗️ ARCHITECTURE GÉNÉRALE

### Vision Layered
```
┌─────────────────────────────────────────────┐
│         UI Layer (Razor Views)              │
│  ├─ Responsive Bootstrap 5                 │
│  ├─ Navigation différenciée par rôle        │
│  └─ Forms, Charts, Timeline, Tables         │
├─────────────────────────────────────────────┤
│      Controller Layer (HTTP Routing)        │
│  ├─ AccountController (Auth)                │
│  ├─ DashboardController (KPIs)              │
│  ├─ ActionPlansController (CRUD)            │
│  └─ ActionsController (Workflow)            │
├─────────────────────────────────────────────┤
│      Service Layer (Business Logic)         │
│  ├─ IActionService / ActionService          │
│  ├─ IEvidenceService / EvidenceService      │
│  ├─ IDashboardService / DashboardService    │
│  ├─ IAuditService / AuditService            │
│  └─ IReportService / ReportService          │
├─────────────────────────────────────────────┤
│    Repository Layer (EF Core DbContext)     │
│  ├─ DbSets (ActionPlan, Action, Evidence)   │
│  ├─ Relationships & Constraints             │
│  └─ Migrations                              │
├─────────────────────────────────────────────┤
│        Data Layer (SQL Server/LocalDB)      │
│  └─ Database: HabibaARR                     │
└─────────────────────────────────────────────┘
```

### Stack Technique
| Composant | Technologie |
|-----------|-------------|
| **Backend** | ASP.NET Core 7.0 MVC |
| **Language** | C# |
| **ORM** | Entity Framework Core 7.0 |
| **Database** | SQL Server / LocalDB |
| **Auth** | ASP.NET Identity |
| **Frontend** | Razor + HTML5 + CSS3 |
| **Styling** | Bootstrap 5 (responsive) |
| **Charts** | HTML5 Canvas (KPI dashboards) |
| **Exports** | ClosedXML (Excel), iTextSharp (PDF) |
| **Logging** | Serilog + Application Insights |

---

## 👥 ACTEURS, RÔLES ET HABILITATIONS

### Les 4 Profils Utilisateurs

#### 1️⃣ **GESTIONNAIRE** (Manager / Plan Manager)
**Rôle:** Préparer et contrôler
- ✅ Ouvre le cycle (crée le plan)
- ✅ Découpe le travail en actions
- ✅ Désigne les responsables
- ✅ Transmet le plan (point de non-retour)
- ✅ Contrôle les preuves soumises
- ✅ Valide ou rejette les preuves
- ✅ Clôture les actions

**Responsabilités:**
- Maître du processus, vision synthétique
- Traçabilité des décisions
- Contrôle d'approbation côté serveur + traçage

#### 2️⃣ **RESPONSABLE** (Assignee / Performer)
**Rôle:** Exécuter et justifier
- ✅ Reçoit les actions assignées
- ✅ Accepte ou rejette avec motif
- ✅ Met à jour l'avancement (0-100%)
- ✅ Documente dans le journal
- ✅ Soumet la preuve (justificatif)
- ✅ Télécharge les pièces jointes

**Responsabilités:**
- Espace de travail limité à ses actions assignées
- Droit de refus avec motif (pas d'imposition)
- Transparence totale de ses actions

#### 3️⃣ **DIRECTEUR** (Director / Pilot)
**Rôle:** Piloter (observation stratégique)
- ✅ Accès tableau de bord global (KPI)
- ✅ Export rapports synthétiques
- ✅ Vue lecture seule (pas de modification)
- ✅ Aucune intervention opérationnelle

**Remarque:** Profil d'observation future (extension possible)

#### 4️⃣ **ADMINISTRATEUR** (Admin / Supervisor)
**Rôle:** Superviser
- ✅ Toutes les capacités du Directeur
- ✅ Création/suppression de comptes
- ✅ Attribution de rôles
- ✅ Purge de données
- ✅ Gestion complète de la plateforme

---

## 📊 MATRICE DES RESPONSABILITÉS

| Opération | Admin | Directeur | Gestionnaire | Responsable |
|-----------|-------|-----------|--------------|------------|
| Se connecter | ✅ | ✅ | ✅ | ✅ |
| Tableau de bord (global) | ✅ | ✅ | ✅ | ❌ |
| Exporter rapports | ✅ | ✅ | ✅ | ❌ |
| **Créer un plan** | ❌ | ❌ | ✅ | ❌ |
| **Modifier plan (brouillon)** | ❌ | ❌ | ✅ | ❌ |
| **Transmettre plan** | ❌ | ❌ | ✅ | ❌ |
| Consulter ses actions | ❌ | ❌ | ✅ | ✅ |
| **Accepter action** | ❌ | ❌ | ❌ | ✅ |
| **Rejeter action (motif)** | ❌ | ❌ | ❌ | ✅ |
| **Mettre à jour avancement** | ❌ | ❌ | ❌ | ✅ |
| Écrire au journal | ✅ | ❌ | ✅ | ✅ |
| **Soumettre preuve** | ❌ | ❌ | ❌ | ✅ |
| **Valider preuve** | ❌ | ❌ | ✅ | ❌ |
| **Renvoyer preuve (motif)** | ❌ | ❌ | ✅ | ❌ |
| **Clôturer action** | ❌ | ❌ | ✅ | ❌ |
| Télécharger pièce jointe | ✅ | ✅ | ✅ | ✅ sur ses actions |

### Principes d'Autorisation
1. **Moindre Privilège** → Chaque rôle: permissions strictement nécessaires
2. **Contrôle Côté Serveur** → URL ne suffit pas; vérification serveur obligatoire
3. **Contrôle d'Appartenance** → Responsable ne voit que ses actions assignées
4. **Navigation Différenciée** → Menu adapté au rôle en post-login
5. **Audit Complet** → Chaque action sensible enregistrée (journal d'audit)

---

## 🔄 WORKFLOW ET CYCLE DE VIE D'UNE ACTION

### Vue d'Ensemble: 8 États

```
┌─────────────┐
│  Brouillon  │  ← Gestionnaire prépare, aucun engagement
└──────┬──────┘
       │ Transmission du plan
       ↓
┌─────────────┐
│  Nouveau    │  ← Action transmise, attend décision du Responsable
└──────┬──────┘
       ├─ [Acceptation] → En cours
       └─ [Rejet + motif] → Rejetée
           
┌──────────────┐
│  Rejetée     │  ← Action refusée avec motif, retour à Gestionnaire
└──────┬───────┘
       │ Gestionnaire redéfinit
       ↓
┌──────────────┐
│  En cours    │  ← Responsable accepte et exécute (0-100%)
└──────┬───────┘
       │ Avancement à 100% + preuve soumise
       ↓
┌──────────────────┐
│ Preuve soumise   │  ← Justificatif en attente de contrôle
└──────┬───────────┘
       ├─ [Validation] → Clôturée
       └─ [Rejet + motif] → Preuve renvoyée
           
┌──────────────────┐
│ Preuve renvoyée  │  ← Justificatif insuffisant, nouvelle soumission attendue
└──────┬───────────┘
       │ Responsable réintègre preuve
       ↓
       [Preuve soumise]
           
┌──────────────┐
│  Clôturée    │  ← Cycle terminé, archivée, immutable
└──────────────┘
```

### Détails des États

| État | Signification | Acteur en Attente | Modifiable? | Archivée? |
|------|---------------|-------------------|------------|-----------|
| **Brouillon** | Plan en préparation | Gestionnaire | ✅ | ❌ |
| **Nouveau** | Action transmise | Responsable | ❌ | ❌ |
| **Rejetée** | Action refusée (motif) | Gestionnaire | Correction possible | ❌ |
| **En cours** | Acceptée et exécution | Responsable | ✅ Avancement | ❌ |
| **Preuve soumise** | Justificatif en attente | Gestionnaire | ❌ | ❌ |
| **Preuve renvoyée** | Justificatif insuffisant | Responsable | ✅ Correction | ❌ |
| **Clôturée** | Validée et archivée | Aucun | ❌ | ✅ |

### Transitions et Gardiens

#### Transmission (Brouillon → Nouveau)
- **Condition:** Plan complet (toutes actions ont responsable + échéance)
- **Action:** Validation d'intégrité côté serveur
- **Résultat:** Plan → état Transmis; Actions → état Nouveau

#### Acceptation (Nouveau → En cours)
- **Condition:** Responsable authentifié ET action lui est assignée
- **Action:** Enregistrement consentement, avancement = 0%
- **Résultat:** Action En cours, journal horodaté

#### Rejet (Nouveau/En cours/Preuve renvoyée → Rejetée)
- **Condition:** Motif non vide (validation)
- **Action:** Enregistrement motif, action retourne à Gestionnaire
- **Résultat:** Responsable voit action à corriger; Gestionnaire voit motif

#### Avancement (En cours → En cours)
- **Condition:** Valeur entre 0-100%
- **Action:** Mise à jour + journal (changement enregistré)
- **Résultat:** Historique complet pour pilotage

#### Soumission Preuve (En cours → Preuve soumise)
- **Condition:** Fichier uploadé + commentaire explicatif
- **Action:** Validation fichier (extension, taille, type MIME)
- **Résultat:** Preuve stockée; action passe à Preuve soumise

#### Validation Preuve (Preuve soumise → Clôturée)
- **Condition:** Gestionnaire valide (lecture commentaire + fichiers)
- **Action:** Enregistrement validation + clôture action
- **Résultat:** Action archivée; plan recalculé (% de complétude)

#### Renvoi Preuve (Preuve soumise → Preuve renvoyée)
- **Condition:** Motif de renvoi saisi
- **Action:** Preuve précédente archivée (historique gardé)
- **Résultat:** Responsable voit motif; doit resoumetttre

#### Clôture Plan (Quand dernière action clôturée)
- **Action:** Plan passe à état Clôturé
- **Résultat:** Toutes actions immutables; plan archivé

### Règles de Gestion Implémentées (RG01-RG15)

| Code | Règle | Implémentation |
|------|-------|----------------|
| **RG01** | Une action obligatoirement rattachée à un plan | Constraint FK dans modèle |
| **RG02** | Une action: 1 responsable + 1 seul à un instant | Unicité de référence |
| **RG03** | Plan en brouillon visible uniquement par gestionnaire | Filtrage au ReadList |
| **RG04** | Transmission: toutes actions passent à Nouveau atomiquement | Transactionnelle |
| **RG05** | Seul responsable désigné peut accepter/rejeter | Vérification appartenance |
| **RG06** | Rejet exige motif non vide | Validation formulaire + serveur |
| **RG07** | Avancement: valeur 0-100 | Validation JS + contrôleur |
| **RG08** | Seul responsable peut soumettre preuve pour son action | Vérification ownership |
| **RG09** | Seul gestionnaire du plan valide preuve | FK plan → user |
| **RG10** | Rejet de preuve exige motif | Validation formulaire |
| **RG11** | Clôture impossible avant validation preuve | Guard clause |
| **RG12** | Action clôturée n'accepte plus d'écriture | IsArchived flag |
| **RG13** | Plan clôturé quand toutes actions clôturées | Trigger logique service |
| **RG14** | Chaque transition: entrée journal nominatif + horodatée | AuditLog.Create() |
| **RG15** | Pièce jointe téléchargeable uniquement par utilisateurs habiles | Vérification rôle + ownership |

---

## 📦 MODÈLE DE DONNÉES DÉTAILLÉ

### Entités Principales

#### **ActionPlan** (Plan d'Action)
```csharp
Id: Guid
Reference: string (unique) // PAC-2026-001
Title: string
Description: string
Status: PlanStatus (Draft/Active/Closed/Archived)
StartDate: DateTime
EndDate: DateTime
ManagerId: string (FK → ApplicationUser)
Manager: ApplicationUser (navigation)
Actions: ICollection<Action>
CreatedAt: DateTime
CreatedBy: string
UpdatedAt: DateTime?
UpdatedBy: string?
```

#### **Action** (Action dans le Plan)
```csharp
Id: Guid
ActionPlanId: Guid (FK)
ActionPlan: ActionPlan (navigation)
Reference: string (unique) // ACT-2026-001
Title: string
Description: string
Status: ActionStatus (Draft/New/Accepted/InProgress/
                     EvidenceSubmitted/EvidenceRejected/Completed)
Priority: int (1=Urgente, 2=Haute, 3=Normale, 4=Basse)
DueDate: DateTime
ProgressPercentage: int (0-100)
AssignedToId: string (FK → ApplicationUser, Responsable)
AssignedTo: ApplicationUser (navigation)
ManagerId: string (FK → ApplicationUser, Gestionnaire du plan)
Manager: ApplicationUser (navigation)
RejectionReason: string? (motif de rejet)
Evidence: ICollection<Evidence>
Logs: ICollection<ActionLog>
IsArchived: bool
CreatedAt: DateTime
CreatedBy: string
UpdatedAt: DateTime?
UpdatedBy: string?
```

#### **Evidence** (Preuve / Justificatif)
```csharp
Id: Guid
ActionId: Guid (FK)
Action: Action (navigation)
Comment: string (explication)
SubmittedAt: DateTime
SubmittedBy: string (FK → ApplicationUser)
SubmittedByUser: ApplicationUser (navigation)
Status: EvidenceStatus (Submitted/Approved/Rejected)
ApprovedAt: DateTime?
ApprovedBy: string? (FK → ApplicationUser, Gestionnaire)
ApprovedByUser: ApplicationUser?
RejectedAt: DateTime?
RejectedBy: string?
RejectionReason: string? (motif de rejet)
Attachments: ICollection<Attachment>
IsArchived: bool
```

#### **Attachment** (Pièce Jointe)
```csharp
Id: Guid
EvidenceId: Guid (FK)
Evidence: Evidence (navigation)
FileName: string
FileType: string (extension: .pdf, .doc, .jpg)
FileSizeBytes: long
FilePath: string (chemin serveur: /uploads/...)
UploadedAt: DateTime
UploadedBy: string (FK → ApplicationUser)
UploadedByUser: ApplicationUser
IsArchived: bool
```

#### **ActionLog** (Journal d'Audit / Traçabilité)
```csharp
Id: Guid
ActionId: Guid (FK, nullable)
Action: Action? (navigation)
PlanId: Guid (FK, nullable)
Plan: ActionPlan? (navigation)
EventType: string // "PlanCreated", "ActionAdded", "ActionTransmitted",
                  // "ActionAccepted", "ActionRejected", "ProgressUpdated",
                  // "EvidenceSubmitted", "EvidenceApproved", "EvidenceRejected",
                  // "ActionClosed", "CommentAdded"
OldValue: string? (valeur avant)
NewValue: string? (valeur après)
Comment: string? (contexte humain)
CreatedAt: DateTime (horodatage)
CreatedBy: string (FK → ApplicationUser)
CreatedByUser: ApplicationUser (navigation)
IpAddress: string? (source)
UserAgent: string? (navigateur)
```

#### **ApplicationUser** (Utilisateur / Personne)
```csharp
Id: string (ASP.NET Identity)
Email: string
PasswordHash: string (hashé)
FirstName: string
LastName: string
Position: string (fonction: Ingénieur, Manager, etc.)
Department: string (service)
Role: string (Admin/Directeur/Gestionnaire/Responsable)
CreatedAt: DateTime
LastLoginAt: DateTime?
IsActive: bool
ActionsAssigned: ICollection<Action>
ActionsManaged: ICollection<Action>
PlansManaged: ICollection<ActionPlan>
EvidenceSubmitted: ICollection<Evidence>
Logs: ICollection<ActionLog>
```

### Diagramme ER (Entité-Relation)

```
ApplicationUser (Admin/Dir/Gest/Resp)
    ↑↓ (ActionsAssigned, Manager)
Action
    ↑↓ (Evidence)
Evidence
    ↑↓ (Attachments)
Attachment

ActionPlan
    ↑↓ (Manager)
ApplicationUser

ActionLog
    ↑↓ (Action, Plan, CreatedBy)
(tous les changements)
```

### Contraintes et Indexes

```sql
-- Unicité
UNIQUE(ActionPlan.Reference)
UNIQUE(Action.Reference)
UNIQUE(ApplicationUser.Email)

-- Intégrité Référentielle
FK(Action.ActionPlanId) → ActionPlan(Id) ON DELETE CASCADE
FK(Action.AssignedToId) → ApplicationUser(Id)
FK(Action.ManagerId) → ApplicationUser(Id)
FK(Evidence.ActionId) → Action(Id) ON DELETE CASCADE
FK(Attachment.EvidenceId) → Evidence(Id) ON DELETE CASCADE
FK(ActionLog.ActionId) → Action(Id) ON DELETE SET NULL
FK(ActionLog.PlanId) → ActionPlan(Id) ON DELETE SET NULL

-- Performance
INDEX(Action.Status, Action.AssignedToId)  -- Filtrage rapide
INDEX(Action.DueDate)  -- Triage par échéance
INDEX(ActionLog.CreatedAt, ActionLog.ActionId)  -- Audit
INDEX(ActionPlan.ManagerId, ActionPlan.Status)  -- Plans du Gestionnaire
```

---

## 🎯 CAS D'UTILISATION DÉTAILLÉS (19 UC)

### Cycle d'Accès (UC01-02)

#### **UC01 - S'inscrire**
- **Acteur:** Utilisateur non authentifié
- **Précondition:** Email n'existe pas
- **Scénario nominal:** Email + nom + prénom + mdp → compte créé inactif
- **Alternative:** Email existe → message neutre (pas de révélation)
- **Postcondition:** Compte inactif; admin doit assigner rôle

#### **UC02 - Se connecter**
- **Acteur:** Tout utilisateur ayant compte
- **Précondition:** Compte actif
- **Scénario nominal:** Email + mdp → session ouverte, cookie Auth
- **Alternative:** Identifiants erronés → message générique
- **Postcondition:** Utilisateur redirectionné vers accueil de son rôle

### Cycle Gestionnaire (UC05-07, UC16-17)

#### **UC05 - Créer un plan**
- **Acteur:** Gestionnaire
- **Précondition:** Authentifié avec rôle Gestionnaire
- **Scénario:** Saisit Référence, Titre, Description, Dates → Plan créé à l'état Brouillon
- **Alternative:** Référence déjà utilisée → message d'erreur
- **Postcondition:** Plan visible uniquement par gestionnaire

#### **UC06 - Ajouter/modifier actions**
- **Acteur:** Gestionnaire
- **Précondition:** Plan à l'état Brouillon
- **Scénario:** Remplit Référence, Titre, Priorité, Responsable, Échéance → Action créée
- **Postcondition:** Plan contient une ou plusieurs actions

#### **UC07 - Transmettre un plan**
- **Acteur:** Gestionnaire
- **Précondition:** Plan à l'état Brouillon, toutes actions ont responsable
- **Scénario:** Déclenche transmission → Validation complétude → Plan/Actions passent à Transmis/Nouveau
- **Postcondition:** Actions visibles à leurs responsables; Gestionnaire perd droit de modif

#### **UC16 - Valider une preuve**
- **Acteur:** Gestionnaire
- **Précondition:** Action à l'état Preuve soumise
- **Scénario:** Consulte preuve et fichiers → Valide → Action clôturée
- **Postcondition:** Action immutable; Plan recalculé

#### **UC17 - Renvoyer une preuve**
- **Acteur:** Gestionnaire
- **Précondition:** Action à l'état Preuve soumise
- **Scénario:** Saisit motif → Preuve renvoyée
- **Postcondition:** Responsable voit motif; doit resoumetttre

### Cycle Responsable (UC08-14, UC19)

#### **UC08 - Consulter ses actions**
- **Acteur:** Responsable
- **Scénario:** Tableau "Mes actions" avec tri par échéance, statut, plan
- **Postcondition:** Aucune modif; consultation seule

#### **UC09 - Accepter une action**
- **Acteur:** Responsable
- **Précondition:** Action à l'état Nouveau, assignée à lui
- **Scénario:** Clique "Accepter" → Validation rôle → Action passe à En cours (avancement 0%)
- **Postcondition:** Responsable engage responsabilité; entré au journal

#### **UC10 - Rejeter une action**
- **Acteur:** Responsable
- **Précondition:** Action à l'état Nouveau
- **Scénario:** Saisit motif + valide → Action revient à Gestionnaire avec motif
- **Postcondition:** Gestionnaire voit motif; peut corriger ou changer responsable

#### **UC12 - Mettre à jour l'avancement**
- **Acteur:** Responsable
- **Précondition:** Action à l'état En cours
- **Scénario:** Saisit valeur 0-100 → Validation → Journalisation
- **Postcondition:** Historique d'avancement conservé

#### **UC13 - Écrire au journal**
- **Acteur:** Responsable ou Gestionnaire
- **Précondition:** Action non clôturée
- **Scénario:** Ajoute commentaire libre → Enregistrement avec horodatage
- **Postcondition:** Journal à lecture appending (immuable)

#### **UC14 - Soumettre une preuve**
- **Acteur:** Responsable
- **Précondition:** Action à l'état En cours ou Preuve renvoyée
- **Scénario:** Tape commentaire + sélectionne fichier(s) → Validation types → Enregistrement
- **Postcondition:** Action passe à Preuve soumise; Gestionnaire notifié

#### **UC19 - Télécharger pièce jointe**
- **Acteur:** Tout utilisateur habilité sur action
- **Scénario:** Clique sur nom fichier → Téléchargement depuis serveur
- **Postcondition:** Aucune modif; traçage du téléchargement

### Cycle Pilotage (UC03-04)

#### **UC03 - Consulter tableau de bord**
- **Acteur:** Admin, Directeur, Gestionnaire
- **Scénario:** Affichage KPI: Total actions, Complétées, En retard, % complétion, Graphiques
- **Postcondition:** Vue synthétique avec filtres

#### **UC04 - Exporter un rapport**
- **Acteur:** Admin, Directeur, Gestionnaire
- **Scénario:** Sélectionne filtres → Format (Excel/PDF) → Téléchargement
- **Postcondition:** Rapport horodaté avec les données filtrées

---

## 🔐 SÉCURITÉ ET CONTRÔLES

### Authentification
- **Mécanisme:** ASP.NET Identity (hachage PBKDF2)
- **Session:** Cookie authentifié (HttpOnly, Secure)
- **Timeout:** 30 minutes d'inactivité

### Autorisation
- **Niveaux:**
  1. **Rôle (Controllers):** `[Authorize(Roles = "Gestionnaire")]`
  2. **Ownership (Business Logic):** Vérification que UserId = AssignedToId ou ManagerId
  3. **Audit Trail:** Chaque action sensible enregistrée

### Protection Données
- **Fichiers uploadés:** Validation MIME + extension + taille (max 10 MB)
- **Chemin serveur:** `/uploads/{actionId}/{filename}` (pas accessible direct via URL)
- **CSRF:** Tokens `@Html.AntiForgeryToken()` sur tous POST
- **SQL Injection:** Parameterized queries (EF Core)

### Logging et Audit
- **Actions critiques:** Création plan, transmission, validation preuve, clôture
- **Données enregistrées:** UserId, IP, UserAgent, timestamp, changements avant/après
- **Immuabilité:** ActionLog append-only (jamais modifié/supprimé)

---

## 📊 TABLEAU DE BORD (DASHBOARD)

### KPI Affichés

#### **Vue Gestionnaire**
```
┌──────────────────────────────────────┐
│  Plans en cours      │  5            │
│  Actions totales     │  47           │
│  Actions complétées  │  23 (49%)     │
│  Actions en retard   │  8            │
│  Taux de complétion  │  [████░░] 49% │
└──────────────────────────────────────┘

Répartition par statut:
  En cours      : 12 actions
  Preuve à contrôler: 8 actions
  En attente : 4 actions

Top 5 actions en retard:
  ACT-2026-012 | Intégration API | Echéance: 31/10
  ACT-2026-018 | Tests UAT      | Echéance: 28/10
  ...
```

#### **Vue Responsable**
```
┌──────────────────────────────────────┐
│  Mes actions assignées   │  8        │
│  À ma charge             │  6        │
│  Rejetées                │  2        │
│  Complétées ce mois      │  3        │
└──────────────────────────────────────┘

Mes prochaines échéances:
  Demain     : ACT-2026-045 | Validation
  3 jours    : ACT-2026-061 | Tests
  Semaine    : ACT-2026-033 | Rapport
```

### Graphiques
- **Barre:** Complétion par plan
- **Pie:** Répartition statut (En cours / Complétées / Rejetées)
- **Timeline:** Actions clôturées par semaine
- **Heatmap:** Charge par responsable

---

## 📋 SPÉCIFICATION DES ÉCRANS (13 écrans)

### 1. **Login.cshtml** (Accueil / Authentification)
```
┌────────────────────────────┐
│      NOVEC GROUP           │
│   Plateforme GPA 2026      │
├────────────────────────────┤
│ Email:    [____________]   │
│ Mot passe:[____________]   │
│ Mémoriser: [x]             │
│ [Connexion]  [Réinit MDP]  │
├────────────────────────────┤
│ Test accounts (hover):     │
│ gestionnaire@novec.fr      │
│ responsable@novec.fr       │
│ admin@novec.fr             │
│ Password: Test@12345       │
└────────────────────────────┘
```

### 2. **Dashboard.cshtml** (Tableau de Bord)
- Affichage KPI en cartes (Bootstrap grid)
- Graphiques avec Chart.js ou Canvas HTML5
- Tableau: Actions en retard (sortable, paginated)
- Filtres: Par plan, par responsable, par statut

### 3. **ActionPlans/Index.cshtml** (Liste des Plans)
- Tableau: Reference | Titre | Manager | Status | # Actions | Dates
- Badges pour état (Draft/Active/Closed)
- Bouton "+ Nouveau plan"
- Actions: Voir détails, Transmettre (si Draft)

### 4. **ActionPlans/Create.cshtml** (Créer Plan)
- Formulaire: Reference, Titre, Description, StartDate, EndDate
- Validation côté client (jQuery Validate) + serveur
- Bouton [Créer]

### 5. **ActionPlans/Details.cshtml** (Détails Plan)
- Informations plan (Reference, Titre, Manager, Dates)
- Tableau des actions du plan
- Bouton "Ajouter action" (si Draft)
- Bouton "Transmettre" (si Draft, complet)
- Journal des événements du plan

### 6. **Actions/Create.cshtml** (Créer Action)
- Formulaire: Reference, Titre, Description, Priorité (dropdown), Responsable (dropdown), DueDate
- Validation: Responsable requis
- Bouton [Ajouter]

### 7. **Actions/Index.cshtml** (Mes Actions - Responsable)
- Tableau filtré: Titre | Plan | Status | % Avancement | Échéance
- Tri par: Status, DueDate
- Actions rapides: Accepter, Rejeter, Consulter

### 8. **Actions/Details.cshtml** (Détails Action - Responsable Vue)
```
┌──────────────────────────────────────┐
│ Référence: ACT-2026-001              │
│ Titre: Développer API REST           │
│ Plan: PAC-2026-001                   │
│ Gestionnaire: Jean Dupont            │
│ Statut: En cours [icon]              │
│ Échéance: 31 Oct 2026                │
│ Avancement: [███░░░░░] 30%           │
│ Priorité: Haute                      │
├──────────────────────────────────────┤
│ ACTIONS (selon statut)               │
│ [Accepter] [Rejeter] (si Nouveau)    │
│ [Soumettre preuve] (si En cours)     │
├──────────────────────────────────────┤
│ JOURNAL D'ACTION                     │
│ ┌─────────────────────────────────┐  │
│ │ 15/10 14:32 - J.Dupont          │  │
│ │ Action acceptée                 │  │
│ │ ─────────────────────────────────│  │
│ │ 16/10 09:15 - R.Martin          │  │
│ │ Avancement 30% (était 20%)      │  │
│ │ "Intégration BD en cours"       │  │
│ └─────────────────────────────────┘  │
├──────────────────────────────────────┤
│ PREUVE                               │
│ Statut: Pas encore soumise           │
│ Ou: En attente de contrôle           │
│ ┌─────────────────────────────────┐  │
│ │ Commentaire:                    │  │
│ │ [________________] (textarea)   │  │
│ │ Pièce jointe: [Browse...]       │  │
│ │ [Soumettre]                     │  │
│ └─────────────────────────────────┘  │
└──────────────────────────────────────┘
```

### 9. **Actions/Details.cshtml** (Détails Action - Gestionnaire Vue)
- Vue similaire mais avec:
  - Bouton [Valider preuve] si Preuve soumise
  - Bouton [Renvoyer preuve] si Preuve soumise
  - Zone consultation preuve (fichiers + commentaire)

### 10. **Evidence/Submit.cshtml** (Soumettre Preuve)
```
Formulaire:
│ Commentaire explicatif: [_________]
│ Fichier(s):             [Browse...]
│ [Soumettre]
```

### 11. **Evidence/Validate.cshtml** (Valider Preuve)
```
│ Action: ACT-2026-001
│ Responsable: R.Martin
│ Preuve soumise le: 15/10 14:30
│ Commentaire: "Développement complété, tests en cours"
│
│ Fichiers attachés:
│ • API.postman.json (125 KB)
│ • Tests_UAT.xlsx (89 KB)
│ 
│ [Valider]  [Renvoyer avec motif]
│
│ Si Renvoyer:
│ Motif: [____________]
│ [Valider]
```

### 12. **Journal/Index.cshtml** (Audit Log)
- Tableau: Date | Utilisateur | Événement | Details | Action/Plan
- Filtres: Par plan, par action, par utilisateur, par type événement
- Export: Button pour exporter en CSV

### 13. **Reports/Export.cshtml** (Export)
- Sélection filtres: Plan, Statut, Responsable, DateRange
- Sélection format: Excel / PDF
- Contenu: Listage actions avec statut, responsable, preuve
- [Exporter]

---

## 🛠️ ARCHITECTURE TECHNIQUE EN COUCHES

### Couche Présentation (Views/Controllers)
```
Controllers/
├── AccountController
│   ├── Login(model)
│   ├── Register(model)
│   └── Logout()
├── HomeController
│   └── Index()
├── DashboardController
│   └── Index()
├── ActionPlansController
│   ├── Index()
│   ├── Create(model)
│   ├── Details(id)
│   ├── Edit(id, model)
│   └── Submit(id)
└── ActionsController
    ├── Index()
    ├── Create(planId, model)
    ├── Details(id)
    ├── Accept(id)
    ├── Reject(id, model)
    ├── UpdateProgress(id, model)
    ├── SubmitEvidence(id, model)
    ├── ValidateEvidence(id)
    └── RejectEvidence(id, model)

Views/
├── Account/Login.cshtml
├── Dashboard/Index.cshtml
├── ActionPlans/
│   ├── Index.cshtml
│   ├── Create.cshtml
│   └── Details.cshtml
├── Actions/
│   ├── Index.cshtml
│   ├── Create.cshtml
│   └── Details.cshtml
└── Shared/
    └── _Layout.cshtml (sidebar nav)
```

### Couche Service (Business Logic)
```
Services/
├── IActionPlanService
│   ├── CreateAsync(model)
│   ├── GetByIdAsync(id)
│   ├── GetAllAsync(filter)
│   ├── SubmitAsync(id)
│   └── ArchiveAsync(id)
├── ActionPlanService : IActionPlanService
│   └── [Implémentation]
│
├── IActionService
│   ├── CreateAsync(actionPlan, model)
│   ├── AcceptAsync(id, userId)
│   ├── RejectAsync(id, reason)
│   ├── UpdateProgressAsync(id, percentage)
│   └── CloseAsync(id)
├── ActionService : IActionService
│   └── [Implémentation]
│
├── IEvidenceService
│   ├── SubmitAsync(actionId, model, file)
│   ├── ValidateAsync(evidenceId, userId)
│   ├── RejectAsync(evidenceId, reason)
│   └── GetAttachmentsAsync(evidenceId)
├── EvidenceService : IEvidenceService
│   └── [Implémentation + File validation]
│
├── IDashboardService
│   ├── GetKPIsAsync(userId, role)
│   ├── GetActionsByStatusAsync(filter)
│   └── GetOverdueActionsAsync()
├── DashboardService : IDashboardService
│   └── [Implémentation]
│
├── IAuditService
│   ├── LogEventAsync(eventType, userId, data)
│   ├── GetLogsAsync(filter)
│   └── ArchiveOldLogsAsync()
├── AuditService : IAuditService
│   └── [Implémentation append-only]
│
└── IReportService
    ├── ExportToExcelAsync(filter)
    └── ExportToPdfAsync(filter)
```

### Couche Données (EF Core)
```
Data/
├── ApplicationDbContext : DbContext
│   ├── DbSet<ActionPlan> ActionPlans
│   ├── DbSet<Action> Actions
│   ├── DbSet<Evidence> Evidences
│   ├── DbSet<Attachment> Attachments
│   ├── DbSet<ActionLog> ActionLogs
│   └── DbSet<ApplicationUser> Users
│   └── OnModelCreating() [Configurations]
│
├── DbInitializer
│   └── Initialize() [Seed roles + users]
│
└── Migrations/
    └── [Auto-generated par EF Core]
```

### Dépendances Injections (Program.cs)
```csharp
services.AddScoped<IActionPlanService, ActionPlanService>();
services.AddScoped<IActionService, ActionService>();
services.AddScoped<IEvidenceService, EvidenceService>();
services.AddScoped<IDashboardService, DashboardService>();
services.AddScoped<IAuditService, AuditService>();
services.AddScoped<IReportService, ReportService>();

services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

---

## 📈 DONNÉES DE TEST

### Utilisateurs de Test
| Email | Rôle | Mot de passe | Première action |
|-------|------|--------------|-----------------|
| admin@novec.fr | Admin | Test@12345 | Gérer comptes |
| directeur@novec.fr | Directeur | Test@12345 | Consulter tableaux |
| gestionnaire@novec.fr | Gestionnaire | Test@12345 | Créer plan |
| responsable@novec.fr | Responsable | Test@12345 | Accepter action |

### Plans de Test Pré-créés
| Référence | Titre | Manager | Status | # Actions |
|-----------|-------|---------|--------|-----------|
| PAC-2026-001 | Transformation IT | J.Dupont | Draft | 3 |
| PAC-2026-002 | Qualité Process | M.Bernard | Active | 5 |
| PAC-2026-003 | Réduction Coûts | P.Lemoine | Closed | 4 |

---

## 🚀 DÉPLOIEMENT ET EXPLOITATION

### Environnements
| Env | Database | URL |
|-----|----------|-----|
| **Dev** | LocalDB | https://localhost:5001 |
| **Test** | SQL Server 2019 | https://test.novec.local |
| **Prod** | SQL Server 2019 | https://gpa.novec.fr |

### Migrations
```bash
# Appliquer automatiquement au démarrage (Program.cs)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
    db.Database.Migrate();
}
```

### Sauvegarde de Données
- **Fréquence:** Quotidienne (00:00)
- **Rétention:** 90 jours
- **Audit Log:** Archivé après 2 ans

---

## 📝 NOTES D'IMPLÉMENTATION

### Points Critiques
1. **Transmission atomique:** Toutes les actions doivent passer à "Nouveau" simultanément
2. **Immuabilité de l'audit:** Aucune modification/suppression du journal
3. **Traçage d'accès:** Chaque téléchargement de fichier doit être loggué
4. **Contrôle ownership:** Double vérification (URL + serveur) pour ownership

### Améliorations Futures
- [ ] Notification par email (actions assignées, preuves à contrôler)
- [ ] Mobile app (React Native / Flutter)
- [ ] Intégration SSO (Active Directory)
- [ ] API REST pour tiers
- [ ] Historique de versions des actions
- [ ] Workflow configurable par admin
- [ ] Multi-langues (FR/EN/AR)
- [ ] Signatures numériques sur preuves

---

## 📞 SUPPORT ET MAINTENANCE

**Documentation:**
- `VSCODE_SETUP.md` → Guide d'installation
- `ARCHITECTURE.md` → Architecture détaillée
- `GUIDE_DEMARRAGE.md` → Tutoriel complet

**Support:**
- Équipe Dev: dev@novec.fr
- Ticket Service: support@novec.fr

---

**Version:** 2.0 | **Date:** 2025-2026 | **Statut:** WIP  
**Basé sur:** PROMPT MAÎTRE + Rapport Stage (53 pages) | **Réalisé par:** Habiba Arouada
