# GPA NOVEC - Spécification Technique Complète

**Version:** 2.0  
**Date:** 11 Septembre 2026  
**Statut:** En cours d'implémentation

---

## 🎯 Vue d'Ensemble du Projet

GPA NOVEC est une plateforme web ASP.NET Core 8 pour gérer les plans d'action, actions, preuves (evidences), et audits. Elle supporte 4 rôles avec des permissions distinctes.

---

## 👥 Rôles et Permissions

### 1️⃣ ADMIN
- **Accès:** Tous les plans, toutes les actions
- **Actions:**
  - Voir Dashboard global
  - Exporter rapports (Excel + PDF)

### 2️⃣ DIRECTEUR
- **Accès:** Tous les plans, toutes les actions (lecture seule)
- **Actions:**
  - Voir Dashboard global
  - Exporter rapports (Excel + PDF)

### 3️⃣ GESTIONNAIRE
- **Accès:** Tous les plans, toutes les actions
- **Actions:**
  - ✓ Créer plans d'action
  - ✓ Transmettre plan (Brouillon → Actif)
  - ✓ Créer actions dans un plan
  - ✓ Modifier actions en brouillon ou rejetées
  - ✓ Valider preuves (Approuver/Rejeter)
  - ✓ Clôturer actions (Complété)
  - ✓ Voir Dashboard
  - ✓ Exporter rapports

### 4️⃣ RESPONSABLE
- **Accès:** Actions assignées uniquement
- **Actions:**
  - ✓ Voir actions assignées
  - ✓ Accepter/Rejeter actions (motif obligatoire)
  - ✓ Rédiger dans journal d'action
  - ✓ Soumettre preuves avec pièces jointes
  - ✓ Voir Dashboard
  - ✓ Exporter rapports

---

## 📊 Workflow des Actions (7 Statuts)

```
Brouillon (Draft) [GESTIONNAIRE crée]
       ↓
       ↓ [GESTIONNAIRE transmet]
       ↓
Nouvelle (New)
       ↓
       ├→ Acceptée (Accepted) [RESPONSABLE accepte]
       │       ↓
       │       ├→ En cours (InProgress)
       │       │       ↓
       │       └→ Preuve soumise (EvidenceSubmitted) [RESPONSABLE soumet]
       │               ↓
       │               ├→ Approuvée → Complétée ✓✓ [GESTIONNAIRE valide]
       │               └→ Rejetée (EvidenceRejected) [retour à EvidenceSubmitted]
       │
       └→ Rejetée (Rejected) [RESPONSABLE refuse + motif]
               ↓
               [GESTIONNAIRE peut modifier]
```

---

## 📋 Workflow des Plans (4 Statuts)

```
Brouillon (Draft) [GESTIONNAIRE crée]
       ↓
       ↓ [GESTIONNAIRE transmet]
       ↓
Actif (Active)
       ↓
       ↓ [Actions complétées]
       ↓
Clôturé (Closed)
       ↓
Archivé (Archived)
```

---

## 🔧 Implémentation Détaillée

### Controllers à Corriger/Compléter

#### 1. **ActionsController.cs** ✅ PARTIELLEMENT FIXE
- [x] Index: Affiche actions (FIXE - maintenant accessible)
- [ ] Details: Affiche détails + workflow
- [ ] Create: Crée action (brouillon)
- [ ] Accept: Accepte action
- [ ] Reject: Rejette action avec motif
- [ ] SubmitEvidence: Soumet preuve
- [ ] **MISSING:** Close/Complete action

#### 2. **ActionPlansController.cs**
- Gérer transmission plan (Draft → Active)
- Gérer clôture plan (Active → Closed)
- Listing avec statuts

#### 3. **DashboardController.cs**
- KPIs par rôle
- Graphiques

#### 4. **ReportsController.cs**
- Export Excel (✓ existe)
- **MISSING:** Export PDF

### Models (À Vérifier)

```csharp
// Action.cs - 7 statuts
enum ActionStatus {
    Draft = 0,
    New = 1,
    Accepted = 2,
    InProgress = 3,
    EvidenceSubmitted = 4,
    EvidenceRejected = 5,
    Completed = 6,
    Rejected = 7
}

// Evidence.cs - 3 statuts
enum EvidenceStatus {
    Submitted = 0,
    Approved = 1,
    Rejected = 2
}

// ActionLog.cs - Audit trail
public class ActionLog {
    int Id
    int ActionId
    ActionLogEventType EventType // Created, Submitted, Accepted, Rejected, ...
    string Comment
    DateTime CreatedAt
    ApplicationUser CreatedBy
}
```

### Services (À Compléter)

#### **IActionService.cs** Méthodes Requises:
```csharp
// Lecture
GetByIdAsync(id)
GetAllAsync() // ✅ AJOUTE
GetByPlanAsync(planId)
GetAssignedToAsync(userId)
GetOverdueActionsAsync()
GetCountByStatusAsync(status)
GetByStatusAsync(status)
GetTotalCountAsync()

// Écriture
CreateAsync(action, userId)
UpdateAsync(action)
SubmitAsync(id, userId) // Draft → New
AcceptAsync(id, userId) // New → Accepted
RejectAsync(id, userId, reason) // New → Rejected
CompleteAsync(id, userId) // EvidenceSubmitted → Completed
AddLogAsync(actionId, eventType, userId, comment, oldValue, newValue)
```

#### **IReportService.cs** - À Étendre:
```csharp
// Actuel
ExportActionsExcelAsync(...)

// À Ajouter
ExportActionsPdfAsync(...)
ExportPlansPdfAsync(...)
ExportDashboardPdfAsync(...)
```

### Views (À Completer)

#### Actions/Details.cshtml
- [x] Affichage infos de base
- [x] Accept/Reject buttons
- [x] Evidence submission form
- [x] Audit trail
- [ ] **MISSING:** Complete/Close button (pour Gestionnaire)
- [ ] **MISSING:** CSS pour status Draft, InProgress

#### Home/Index.cshtml
- [ ] **CRITICAL:** Ajouter LOGO NOVEC en haut
- [ ] Make it professional

#### Reports/Index.cshtml
- [x] Export Excel
- [ ] **MISSING:** PDF export options

---

## 🚨 Problèmes Identifiés & Solutions

### 1. Actions not displaying (QUAND JE CLIQUE SUR ACTION AFFICHE RIEN)
**Status:** ✅ FIXE
- **Cause:** ActionsController.Index retournait Forbid() pour non-Responsables
- **Solution:** 
  - ✅ Added GetAllAsync() to IActionService
  - ✅ Implemented GetAllAsync() in ActionService
  - ✅ Updated ActionsController.Index to allow Admin/Gestionnaire/Directeur

### 2. Action recording not working (ACTION N'ENREGISTRE PLUS)
**Status:** 🔍 À INVESTIGUER
- **Possible Causes:**
  - Model binding issue avec CreateActionViewModel
  - Database save error
  - Authorization issue on Create POST
- **Investigation Needed:**
  - Check CreateActionViewModel (already found it exists in ActionViewModel.cs)
  - Verify ActionService.CreateAsync() is being called
  - Check error logging in Create POST

### 3. Acceptance/Rejection workflow incomplete
**Status:** ⏳ À COMPLÉTER
- **Missing:**
  - [ ] Validate rejection motif is required
  - [ ] Add comments to action journal during rejection
  - [ ] Handle state transitions properly

### 4. Action closure functionality
**Status:** ⏳ À AJOUTER
- **Required:**
  - [ ] Add "Complete" button in Details view for Gestionnaire
  - [ ] Complete action should transition from EvidenceSubmitted → Completed
  - [ ] Mark completion date

### 5. PDF report export
**Status:** ⏳ À AJOUTER
- **Required:**
  - [ ] Use iTextSharp or similar library
  - [ ] Export actions to PDF
  - [ ] Export dashboard to PDF

### 6. NOVEC logo on home page
**Status:** 🚨 CRITICAL
- **Required:**
  - [ ] Add logo image to wwwroot/images/
  - [ ] Update Home/Index.cshtml
  - [ ] Style professionally

---

## 📁 Fichiers à Modifier/Créer

### Controllers
- [ ] ActionsController.cs - Add Close/Complete method
- [ ] ActionPlansController.cs - Add Transmit and Close methods
- [ ] ReportsController.cs - Add PDF export

### Services
- [ ] ReportService.cs - Add PDF methods
- [ ] DashboardService.cs - Ensure KPI calculations

### Views
- [x] Actions/Index.cshtml - Already good
- [ ] Actions/Details.cshtml - Add Complete button
- [ ] Home/Index.cshtml - Add NOVEC logo
- [ ] Reports/Index.cshtml - Add PDF options
- [ ] Shared/_Layout.cshtml - Ensure navigation is correct

### Models
- [ ] Check ActionLog EventTypes are complete

### CSS
- [ ] wwwroot/css/site.css - Add missing status badges

---

## ✅ Checklist de Complément

### Phase 1: Fix Critical Issues
- [x] Fix Actions not displaying
- [ ] Fix Action recording
- [ ] Verify Accept/Reject workflow

### Phase 2: Add Missing Features
- [ ] Add Complete/Close action
- [ ] Add PDF export
- [ ] Add NOVEC logo

### Phase 3: Testing
- [ ] Test full workflow for each role
- [ ] Test state transitions
- [ ] Test permissions

### Phase 4: Deployment
- [ ] Build Release
- [ ] Test on deployment server

---

## 🧪 Test Scenarios

### Scenario 1: Gestionnaire crée un plan et une action
1. Login as gestionnaire@novec.fr
2. Create Plan (Brouillon)
3. Create Action within Plan
4. Transmit Plan (Brouillon → Actif)
5. Action becomes "Nouvelle"

### Scenario 2: Responsable accepte et soumet preuve
1. Login as responsable@novec.fr
2. See actions assigned
3. Accept action (Nouvelle → Acceptée)
4. Submit evidence with files
5. Evidence shows as "Soumise"

### Scenario 3: Gestionnaire valide preuve et clôt
1. Login as gestionnaire@novec.fr
2. Go to Actions
3. Find action with "Preuve soumise"
4. Approve evidence
5. Complete action (→ Complétée)

### Scenario 4: Export reports
1. Login as anyone
2. Go to Reports
3. Export actions as Excel ✓
4. Export actions as PDF (NEW)
5. Export dashboard as PDF (NEW)

---

## 🔐 Sécurité & Audit

- ✓ ASP.NET Core Identity with roles
- ✓ HTTPS/SSL
- ✓ Authorization checks on each action
- ✓ Audit trail via ActionLog
- ✓ User tracking on every change

---

## 🚀 Déploiement

### Pre-Deployment
```bash
dotnet build
dotnet test (if tests exist)
dotnet publish -c Release
```

### Database
```bash
dotnet ef database update
```

### Hosting
- IIS / Azure App Service / Docker
- SQL Server (LocalDB for dev, SQL Server for prod)
- HTTPS only

---

**Next:** Implement missing features following this specification.
