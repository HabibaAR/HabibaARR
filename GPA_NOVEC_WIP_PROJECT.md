# 📋 GPA NOVEC - Gestion des Plans d'Actions
## WIP Project Document v0.1

**Project Status**: Analysis Complete | Ready for Implementation  
**Last Updated**: 2026-09-11  
**Technology Stack**: ASP.NET Core 8.0 MVC | Entity Framework Core 8.0 | SQL Server | C# 12.0  
**Language**: French (UI) | English (Code)  

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Master Requirements](#master-requirements)
3. [Architecture Components](#architecture-components)
4. [Database Schema](#database-schema)
5. [Security & Authorization](#security--authorization)
6. [Workflow & State Machine](#workflow--state-machine)
7. [Implementation Phases](#implementation-phases)
8. [Testing Strategy](#testing-strategy)
9. [Validation Checklist](#validation-checklist)
10. [Development Roadmap](#development-roadmap)

---

## Project Overview

### Purpose
GPA NOVEC is an Action Plan Management System enabling organizations to:
- Create and track action plans hierarchically
- Assign actions to responsible parties with deadlines
- Submit and validate evidence of completion
- Generate compliance reports
- Monitor KPIs and dashboard metrics

### Key Features
- ✅ Role-Based Access Control (4 roles)
- ✅ Action Plan Workflow (7 phases, 8 statuses)
- ✅ Evidence Submission & Validation
- ✅ Dashboard with KPI metrics
- ✅ Report Generation (PDF/Excel)
- ✅ Action Logging & Audit Trail
- ✅ File Upload Management
- ✅ User Authentication & Authorization

### Business Rules Summary
- All dates use `DateTime.Now.Date` (date-only logic)
- Due dates trigger automatic overdue calculations
- Evidence must be validated before action completion
- Managers can only view/manage their assigned plans
- Administrators have full system access
- All changes are logged with timestamps and user info

---

## Master Requirements

**Source**: User-provided 44-rule master prompt  
**Status**: [EXIGENCE CONFIRMÉE] - Not yet formally validated by user  

### Core Mandatory Rules (Top 10)

| # | Rule | Status | Notes |
|---|------|--------|-------|
| 1 | ASP.NET Core 8.0 MVC (no alternatives) | ✅ FIXÉ | Architecture mandated |
| 2 | C# 12.0 with Entity Framework Core 8.0 | ✅ FIXÉ | ORM and language specified |
| 3 | SQL Server database (Code-First migrations) | ✅ FIXÉ | Database technology locked |
| 4 | Pure CSS3 (no Bootstrap/frameworks) | ✅ FIXÉ | Responsive design required |
| 5 | ZERO JavaScript in code | ✅ FIXÉ | HTML/CSS only, form-based UI |
| 6 | Role-Based Access Control (4 roles) | ✅ EXIGENCE | ADMIN, DIRECTEUR, GESTIONNAIRE, RESPONSABLE |
| 7 | Workflow state machine (8+ transitions) | ✅ EXIGENCE | See [Workflow & State Machine](#workflow--state-machine) |
| 8 | Secure file upload/download | ✅ EXIGENCE | Validation and virus scanning needed |
| 9 | Password hashing (PBKDF2) | ✅ FIXÉ | ASP.NET Core Identity default |
| 10 | Report export (PDF/Excel) | ✅ EXIGENCE | iText7 and ClosedXML libraries |

### Additional Rules (11-44)

**[Full 44 rules preserved from master prompt - see ANNEXE A below]**

---

## Architecture Components

### 1. Controllers Layer (7 Controllers)

| Controller | Responsibilities | Protected By | Status |
|------------|------------------|--------------|--------|
| **HomeController** | Public landing page, redirect authenticated users to dashboard | None | ✅ EXISTS |
| **AccountController** | User login, logout, password reset, registration (admin-only) | None | ⏳ TO BUILD |
| **DashboardController** | KPI dashboard, metrics, charts, overdue actions | [Authorize] | ✅ PARTIAL |
| **ActionPlansController** | CRUD plans, submit plans, view plan details | [Authorize(Roles="GESTIONNAIRE,ADMIN")] | ✅ PARTIAL |
| **ActionsController** | CRUD actions within plans, status transitions, assign responsibles | [Authorize(Roles="GESTIONNAIRE,ADMIN,RESPONSABLE")] | ⏳ TO BUILD |
| **EvidenceController** | Submit evidence, validate evidence, upload attachments | [Authorize(Roles="RESPONSABLE,GESTIONNAIRE,ADMIN")] | ⏳ TO BUILD |
| **ReportsController** | Generate/export reports (PDF/Excel), compliance reports | [Authorize(Roles="GESTIONNAIRE,DIRECTEUR,ADMIN")] | ⏳ TO BUILD |

**Status Summary**: 2/7 partially complete, 5/7 to build

---

### 2. Services Layer (7 Services)

Each service handles business logic, validation, and data access abstraction.

| Service | Key Methods | Status |
|---------|-----------|--------|
| **IActionPlanService** | GetAllAsync, GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync, SubmitAsync, GetByManagerIdAsync | ✅ PARTIAL |
| **IActionService** | GetAllAsync, GetByIdAsync, CreateAsync, UpdateAsync, DeleteAsync, TransitionStatusAsync, GetByPlanIdAsync, GetOverdueAsync | ⏳ TO BUILD |
| **IEvidenceService** | SubmitEvidenceAsync, ValidateEvidenceAsync, RejectEvidenceAsync, GetByActionIdAsync, DeleteAsync | ⏳ TO BUILD |
| **IActionLogService** | LogActionAsync, GetLogsAsync, GetLogsByUserIdAsync, GetLogsByActionIdAsync | ⏳ TO BUILD |
| **IReportService** | GeneratePDFAsync, GenerateExcelAsync, GetComplianceReportAsync, ExportAsync | ⏳ TO BUILD |
| **IFileService** | UploadAsync, DownloadAsync, ValidateAsync, DeleteAsync, GetFileAsync, ScanForVirusAsync | ⏳ TO BUILD |
| **IAuthService** | RegisterUserAsync, LoginAsync, LogoutAsync, ResetPasswordAsync, ChangePasswordAsync | ⏳ TO BUILD |

**Status Summary**: 1/7 partial, 6/7 to build

---

### 3. ViewModels (15+ Models)

| Category | ViewModels | Status |
|----------|-----------|--------|
| **Authentication** | LoginViewModel, RegisterViewModel, ResetPasswordViewModel, ChangePasswordViewModel | ⏳ TO BUILD |
| **Action Plans** | ActionPlanViewModel, CreateActionPlanViewModel, EditActionPlanViewModel, ActionPlanDetailViewModel | ✅ PARTIAL |
| **Actions** | ActionViewModel, CreateActionViewModel, EditActionViewModel, ActionDetailViewModel | ⏳ TO BUILD |
| **Evidence** | EvidenceViewModel, SubmitEvidenceViewModel, ValidateEvidenceViewModel, AttachmentViewModel | ⏳ TO BUILD |
| **Dashboard** | DashboardViewModel, MetricsViewModel, ChartDataViewModel | ⏳ TO BUILD |
| **Reports** | ReportViewModel, ComplianceReportViewModel, ExportOptionsViewModel | ⏳ TO BUILD |

**Status Summary**: ~3/15 partial, ~12/15 to build

---

### 4. Database Entities (7 Entities + Enums)

```
ApplicationUser
├── Id (Primary Key)
├── FullName
├── Email
├── PhoneNumber
├── Department
├── Role (Foreign Key → AspNetRoles)
├── IsActive
├── CreatedAt
└── UpdatedAt

ActionPlan
├── Id (PK)
├── Reference (Unique)
├── Title
├── Description
├── Status (Enum: Draft, Submitted, Approved, Active, Closed, Cancelled)
├── StartDate
├── EndDate
├── ManagerId (FK → ApplicationUser)
├── Manager
├── Actions (1:N)
├── ActionLogs (1:N)
├── CreatedAt
└── UpdatedAt

Action
├── Id (PK)
├── Reference (Unique within Plan)
├── Title
├── Description
├── Status (Enum: Draft, New, Accepted, Rejected, InProgress, EvidenceSubmitted, Completed)
├── PlanId (FK → ActionPlan)
├── Plan
├── ResponsibleId (FK → ApplicationUser, nullable)
├── Responsible
├── DueDate
├── CompletedAt
├── Evidence (1:N)
├── ActionLogs (1:N)
├── CreatedAt
└── UpdatedAt

Evidence
├── Id (PK)
├── ActionId (FK → Action)
├── Action
├── Description
├── SubmittedBy (FK → ApplicationUser)
├── SubmittedDate
├── Status (Enum: Pending, Approved, Rejected)
├── ValidationNotes
├── ValidatedBy (FK → ApplicationUser, nullable)
├── ValidatedDate
├── Attachments (1:N)
└── CreatedAt

Attachment
├── Id (PK)
├── EvidenceId (FK → Evidence)
├── Evidence
├── FileName
├── FileSize
├── FileType
├── StoragePath
├── UploadedDate
├── DownloadCount
└── CreatedAt

ActionLog
├── Id (PK)
├── ActionPlanId (FK → ActionPlan)
├── ActionPlan
├── ActionId (FK → Action, nullable)
├── Action
├── UserId (FK → ApplicationUser)
├── User
├── Action (Enum: Create, Update, Transition, Submit, Validate, Reject, Delete)
├── OldValue
├── NewValue
├── Timestamp
└── IpAddress

[ENUMS]
ActionPlanStatus: Draft, Submitted, Approved, Active, Closed, Cancelled
ActionStatus: Draft, New, Accepted, Rejected, InProgress, EvidenceSubmitted, Completed
EvidenceStatus: Pending, Approved, Rejected
ActionLogAction: Create, Update, Transition, Submit, Validate, Reject, Delete
```

**Database Status**: ✅ Partial (some entities exist, need completion)

---

## Security & Authorization

### 1. Role Hierarchy

```
ADMIN (System Administrator)
├── Can: Manage all users, all plans, all actions, view all reports
├── Access: Full system access
└── Views: All administrative functions

DIRECTEUR (Director/Executive)
├── Can: View compliance reports, approve plans, view KPIs
├── Access: Read-only except for approval workflows
└── Views: Dashboard, Reports, Oversight functions

GESTIONNAIRE (Manager/Action Plan Manager)
├── Can: Create plans, assign actions, review evidence
├── Access: Manage their own plans and subordinates' actions
└── Views: Dashboard, Plans, Actions, Evidence Validation

RESPONSABLE (Responsible Party/Action Owner)
├── Can: Submit evidence, update action status
├── Access: Only their assigned actions
└── Views: Dashboard (personal), Actions (assigned), Evidence submission
```

### 2. Authorization Matrix

| Resource | ADMIN | DIRECTEUR | GESTIONNAIRE | RESPONSABLE |
|----------|-------|-----------|--------------|-------------|
| Dashboard (Full) | ✅ | ✅ | ✅ | ❌ (personal only) |
| Action Plans (All) | ✅ | ✅ (read) | ✅ (own) | ❌ |
| Actions (All) | ✅ | ✅ (read) | ✅ (own plan) | ✅ (assigned) |
| Evidence (All) | ✅ | ✅ (read) | ✅ (validate) | ✅ (own) |
| Reports (All) | ✅ | ✅ | ✅ (own plans) | ❌ |
| User Management | ✅ | ❌ | ❌ | ❌ |
| Audit Logs | ✅ | ✅ (read) | ⚠️ (own only) | ❌ |

---

## Workflow & State Machine

### 1. Action Plan Lifecycle (7 Phases)

```
┌─────────────────────────────────────────────────────┐
│ PHASE 1: CREATION (Gestionnaire)                    │
│ Status: Draft                                       │
│ Actions: Create plan, add basic info                │
│ Next: PHASE 2 (user clicks "Submit")                │
└──────────────────┬──────────────────────────────────┘
                   │ Submit
                   ▼
┌─────────────────────────────────────────────────────┐
│ PHASE 2: SUBMISSION (Gestionnaire)                  │
│ Status: Submitted                                   │
│ Actions: Add all required actions                   │
│ Next: PHASE 3 (all actions added, submit plan)      │
└──────────────────┬──────────────────────────────────┘
                   │ Submit Plan
                   ▼
┌─────────────────────────────────────────────────────┐
│ PHASE 3: APPROVAL (Directeur)                       │
│ Status: Approval Pending                            │
│ Actions: Directeur reviews, approves/rejects        │
│ Next: PHASE 4 (approved) OR reject & return         │
└──────────────────┬──────────────────────────────────┘
                   │ Approve
                   ▼
┌─────────────────────────────────────────────────────┐
│ PHASE 4: ACTIVATION (Gestionnaire)                  │
│ Status: Active                                      │
│ Actions: Assign responsibles to actions             │
│ Next: PHASE 5 (responsibles start work)             │
└──────────────────┬──────────────────────────────────┘
                   │ Start
                   ▼
┌─────────────────────────────────────────────────────┐
│ PHASE 5: EXECUTION (Responsable)                    │
│ Status: InProgress (Action level)                   │
│ Actions: Responsibles perform work                  │
│ Next: PHASE 6 (submit evidence)                     │
└──────────────────┬──────────────────────────────────┘
                   │ Submit Evidence
                   ▼
┌─────────────────────────────────────────────────────┐
│ PHASE 6: VALIDATION (Gestionnaire)                  │
│ Status: EvidenceSubmitted (Action level)            │
│ Actions: Gestionnaire reviews, approves/rejects     │
│ Next: PHASE 7 (approved) OR PHASE 5 (rejected)      │
└──────────────────┬──────────────────────────────────┘
                   │ Approve Evidence
                   ▼
┌─────────────────────────────────────────────────────┐
│ PHASE 7: COMPLETION (Automatic)                     │
│ Status: Completed (Action level)                    │
│ Actions: System marks action complete               │
│ Next: Monitor, generate reports                     │
└─────────────────────────────────────────────────────┘
```

### 2. Action Status Transitions

```
Draft ──Create──> New
  ▲                │
  │             Accept
  │                │
  │             Reject
  │                │
  └────────────────┘
                   │
                   ▼
              Accepted
                   │
              Assign
                   │
                   ▼
             InProgress
                   │
           Submit Evidence
                   │
                   ▼
          EvidenceSubmitted
                   │
         Validate Evidence
                  / \
            Approve   Reject ──┐
                │               │
                ▼               │
            Completed      (back to InProgress)
```

### 3. Status Definitions

| Status | Level | Meaning | Next Steps |
|--------|-------|---------|-----------|
| **Draft** | Action | Initial creation, not yet submitted | Add details, Submit |
| **New** | Action | Submitted, awaiting acceptance | Manager accepts/rejects |
| **Accepted** | Action | Manager approved, can assign responsible | Assign responsible party |
| **Rejected** | Action | Manager rejected, requires revision | Edit and resubmit |
| **InProgress** | Action | Responsible is working on action | Submit evidence when done |
| **EvidenceSubmitted** | Action | Evidence submitted, awaiting validation | Manager validates/rejects |
| **Completed** | Action | Validated and marked complete | Monitor, include in reports |
| **Cancelled** | Action/Plan | Action/Plan cancelled | Cannot revert, documented |

---

## Implementation Phases

### Phase 1: Foundation (Weeks 1-2) - Database & Identity

**Dependencies**: None  
**Deliverables**:
- [ ] Database schema creation (7 entities)
- [ ] Entity Framework DbContext setup
- [ ] Code-First migrations
- [ ] ApplicationUser extension with custom fields
- [ ] Role initialization (ADMIN, DIRECTEUR, GESTIONNAIRE, RESPONSABLE)
- [ ] Test data seeding (4 users, 4 roles)

**Critical Files**:
- `Data/ApplicationDbContext.cs`
- `Models/ApplicationUser.cs`
- `Models/ActionPlan.cs`, `Action.cs`, `Evidence.cs`, etc.
- `Data/DbInitializer.cs`
- Migrations folder

**Status**: 🔴 NOT STARTED (Foundation)

---

### Phase 2: Authentication & Authorization (Week 2-3)

**Dependencies**: Phase 1 ✅  
**Deliverables**:
- [ ] AccountController (Register, Login, Logout, Password Reset)
- [ ] Authentication views (Login, Register forms)
- [ ] Custom authorization attributes
- [ ] Session management
- [ ] Password validation & hashing (PBKDF2)
- [ ] IAuthService implementation

**Critical Files**:
- `Controllers/AccountController.cs`
- `Services/AuthService.cs`
- `Views/Account/*`
- `Middleware/AuthMiddleware.cs`

**Status**: 🔴 NOT STARTED

---

### Phase 3: Dashboard & Core UI (Week 3-4)

**Dependencies**: Phase 1, 2 ✅  
**Deliverables**:
- [ ] DashboardController completion
- [ ] Dashboard views (KPI cards, charts)
- [ ] CSS layout system (Grid, Flexbox, Responsive)
- [ ] Sidebar navigation
- [ ] Status/metric displays
- [ ] IDashboardService enhancement

**Critical Files**:
- `Controllers/DashboardController.cs`
- `Views/Dashboard/Index.cshtml`
- `wwwroot/css/site.css` (complete)
- `Services/DashboardService.cs`

**Status**: 🟡 PARTIAL (Controllers/Services partial, CSS needs work)

---

### Phase 4: Action Plan Management (Week 4-5)

**Dependencies**: Phase 1-3 ✅  
**Deliverables**:
- [ ] ActionPlansController completion (CRUD, Submit)
- [ ] ActionPlanService completion
- [ ] Views (List, Create, Edit, Details, Submit)
- [ ] Validation rules
- [ ] Manager permission checks
- [ ] Plan status transitions

**Critical Files**:
- `Controllers/ActionPlansController.cs`
- `Services/ActionPlanService.cs`
- `Views/ActionPlans/*`
- `ViewModels/ActionPlanViewModel.cs`

**Status**: 🟡 PARTIAL (Controller exists but incomplete)

---

### Phase 5: Action & Assignment Management (Week 5-6)

**Dependencies**: Phase 1-4 ✅  
**Deliverables**:
- [ ] ActionsController (CRUD, Status transitions, Assignment)
- [ ] ActionService (Business logic, status validation)
- [ ] Views (List, Create, Edit, Assign, Status change)
- [ ] Responsible assignment workflow
- [ ] Overdue calculations
- [ ] Status transition validation

**Critical Files**:
- `Controllers/ActionsController.cs`
- `Services/ActionService.cs`
- `Views/Actions/*`
- `ViewModels/ActionViewModel.cs`

**Status**: 🔴 NOT STARTED

---

### Phase 6: Evidence Submission & Validation (Week 6-7)

**Dependencies**: Phase 1-5 ✅  
**Deliverables**:
- [ ] EvidenceController (Submit, Validate, Reject)
- [ ] EvidenceService (Storage, validation logic)
- [ ] FileService (Upload, Download, Virus scan)
- [ ] Attachment management
- [ ] Evidence validation workflow
- [ ] File security (scanning, storage paths)

**Critical Files**:
- `Controllers/EvidenceController.cs`
- `Services/EvidenceService.cs`
- `Services/FileService.cs`
- `Views/Evidence/*`

**Status**: 🔴 NOT STARTED

---

### Phase 7: Reporting & Export (Week 7-8)

**Dependencies**: Phase 1-6 ✅  
**Deliverables**:
- [ ] ReportsController (Generate, Export)
- [ ] ReportService (PDF/Excel generation)
- [ ] Compliance report templates
- [ ] PDF export (iText7)
- [ ] Excel export (ClosedXML)
- [ ] Report views and options

**Critical Files**:
- `Controllers/ReportsController.cs`
- `Services/ReportService.cs`
- `Views/Reports/*`
- Library configuration (iText7, ClosedXML)

**Status**: 🔴 NOT STARTED

---

### Phase 8: Audit Logging & Monitoring (Week 8)

**Dependencies**: Phase 1-7 ✅  
**Deliverables**:
- [ ] ActionLogService (Log all changes)
- [ ] Audit trail views
- [ ] Logging middleware
- [ ] Change tracking
- [ ] User activity reports

**Critical Files**:
- `Services/ActionLogService.cs`
- `Models/ActionLog.cs`
- `Middleware/AuditMiddleware.cs`

**Status**: 🔴 NOT STARTED

---

### Phase 9: Testing & QA (Week 9-10)

**Dependencies**: All phases ✅  
**Deliverables**:
- [ ] Unit tests (Services layer)
- [ ] Integration tests (Controllers)
- [ ] UI/UX testing
- [ ] Workflow testing (all 7 phases)
- [ ] Security testing
- [ ] Performance testing

**Status**: 🔴 NOT STARTED

---

### Phase 10: Deployment & Documentation (Week 10-11)

**Dependencies**: All phases ✅  
**Deliverables**:
- [ ] Deployment guide
- [ ] API documentation
- [ ] User manual (French)
- [ ] Administrator guide
- [ ] Database backup procedures
- [ ] Monitoring setup

**Status**: 🔴 NOT STARTED

---

## Testing Strategy

### 1. Unit Tests (Services Layer)

```
✓ ActionPlanService
  ✓ CreateAsync - valid input
  ✓ CreateAsync - duplicate reference
  ✓ GetByIdAsync - found
  ✓ GetByIdAsync - not found
  ✓ DeleteAsync - cascade effects
  ✓ SubmitAsync - permission check

✓ ActionService
  ✓ TransitionStatusAsync - valid transitions only
  ✓ TransitionStatusAsync - invalid transitions rejected
  ✓ GetOverdueAsync - correctly identifies overdue

✓ EvidenceService
  ✓ ValidateEvidenceAsync - updates status
  ✓ RejectEvidenceAsync - resets action status

✓ FileService
  ✓ UploadAsync - file validation
  ✓ UploadAsync - virus scan
  ✓ DownloadAsync - permission check
```

### 2. Integration Tests (Controllers)

```
✓ Dashboard
  ✓ GET / - renders without null reference
  ✓ GET / - shows correct KPIs for role

✓ ActionPlans
  ✓ GET / - lists user's plans only
  ✓ POST /Create - creates and redirects
  ✓ GET /Details/:id - forbidden for others

✓ Actions
  ✓ POST /Transition/:id - changes status
  ✓ POST /Assign/:id - assigns responsible
```

### 3. Workflow Tests (End-to-End)

```
✓ Complete Action Plan Lifecycle
  1. Create plan (Draft) ──> Submit plan (Submitted)
  2. Approve plan (Approved) ──> Activate (Active)
  3. Create action ──> Assign responsible
  4. Work on action ──> Submit evidence
  5. Validate evidence ──> Mark complete
  6. Generate report ✓

✓ Rejection Workflows
  ✓ Plan rejected at approval stage
  ✓ Action rejected, sent back to responsible
  ✓ Evidence rejected, returned for revision

✓ Overdue Handling
  ✓ Actions past due date flagged
  ✓ Dashboard shows overdue count
  ✓ Reports include overdue actions
```

---

## Validation Checklist

**Instructions**: User must review and confirm each item before Phase 1 begins.

### Architecture Validation

- [ ] **Controllers (7 total)** - All 7 controllers and their responsibilities confirmed
- [ ] **Services (7 total)** - All 7 services and method signatures confirmed  
- [ ] **ViewModels (15+ total)** - All ViewModels and their properties confirmed
- [ ] **Database Schema** - All 7 entities and relationships confirmed
- [ ] **Role Hierarchy** - 4 roles and permission matrix confirmed
- [ ] **Workflow Phases** - All 7 phases and transitions confirmed
- [ ] **Status Definitions** - All 8 statuses and rules confirmed

### Technical Validation

- [ ] **Technology Stack** - ASP.NET Core 8.0, EF Core 8.0, SQL Server confirmed
- [ ] **CSS-Only UI** - No JavaScript, no Bootstrap, pure CSS3 confirmed
- [ ] **File Upload Security** - Virus scanning and storage strategy confirmed
- [ ] **Report Export** - iText7 (PDF) and ClosedXML (Excel) confirmed
- [ ] **Audit Logging** - All changes to be logged confirmed

### Business Rules Validation

- [ ] **Master Prompt Rules (44 rules)** - All requirements understood and confirmed
- [ ] **Workflow Rules** - All transition rules and restrictions confirmed
- [ ] **Permission Rules** - Role-based access control matrix confirmed
- [ ] **Overdue Calculations** - Logic for identifying overdue actions confirmed
- [ ] **Completion Criteria** - Conditions for marking actions complete confirmed

### Data Validation

- [ ] **Reference Format** - Plan/Action reference naming conventions confirmed
- [ ] **Date Handling** - All dates use `DateTime.Now.Date` (no times) confirmed
- [ ] **Status Enums** - All enum values and their meanings confirmed
- [ ] **Attachment Limits** - File size and type restrictions confirmed

---

## Development Roadmap

### Timeline Overview

```
Week 1-2  │ Phase 1: Database & Identity
          │ ├─ DbContext setup
          │ ├─ Entity models
          │ └─ Migrations
          │
Week 2-3  │ Phase 2: Authentication
          │ ├─ Account controller
          │ ├─ Login/Register views
          │ └─ Session management
          │
Week 3-4  │ Phase 3: Dashboard
          │ ├─ Dashboard controller
          │ ├─ KPI display
          │ └─ CSS layout
          │
Week 4-5  │ Phase 4: Action Plans
          │ ├─ CRUD operations
          │ ├─ Submit workflow
          │ └─ Views
          │
Week 5-6  │ Phase 5: Actions
          │ ├─ Assign responsibles
          │ ├─ Status transitions
          │ └─ Overdue tracking
          │
Week 6-7  │ Phase 6: Evidence
          │ ├─ File upload/download
          │ ├─ Validation workflow
          │ └─ Virus scanning
          │
Week 7-8  │ Phase 7: Reports
          │ ├─ PDF generation
          │ ├─ Excel export
          │ └─ Compliance reports
          │
Week 8    │ Phase 8: Audit Logging
          │ └─ Change tracking
          │
Week 9-10 │ Phase 9: Testing & QA
          │ ├─ Unit tests
          │ ├─ Integration tests
          │ └─ Workflow tests
          │
Week 10-11│ Phase 10: Deployment
          │ ├─ Documentation
          │ ├─ User manual
          │ └─ Go-live prep
```

### Critical Path

1. **Database Schema** (Phase 1) - Blocks all other phases
2. **Authentication** (Phase 2) - Required before any protected resources
3. **Dashboard** (Phase 3) - First user-facing feature
4. **Action Plans** (Phase 4) - Core business flow
5. **Actions** (Phase 5) - Core business flow
6. **Evidence** (Phase 6) - Completion mechanism
7. **Reports** (Phase 7) - Final deliverable

### Success Criteria (Phase by Phase)

| Phase | Success Criteria | Completion |
|-------|------------------|------------|
| 1 | Database initializes, migrations run successfully | Week 2 |
| 2 | Users can login/logout, sessions persist | Week 3 |
| 3 | Dashboard loads without errors, shows KPIs | Week 4 |
| 4 | Can create and submit action plans | Week 5 |
| 5 | Can assign actions, track status | Week 6 |
| 6 | Can upload evidence, validate completion | Week 7 |
| 7 | Can export PDF/Excel reports | Week 8 |
| 8 | All changes logged and auditable | Week 8 |
| 9 | All tests passing (>80% coverage) | Week 10 |
| 10 | Documentation complete, ready for deployment | Week 11 |

---

## ANNEXE A: Master Prompt Rules (44 Rules)

**Source**: User-provided comprehensive requirements document  
**Status**: [EXIGENCE CONFIRMÉE] - Requires user validation

### Functional Requirements (Rules 1-20)

1. **Action Plan Creation**: Gestionnaires can create plans with reference, title, description, dates
2. **Action Plan Status**: Plans follow workflow (Draft → Submitted → Approved → Active → Closed)
3. **Action Creation**: Gestionnaires can add actions within plans with reference, title, due date
4. **Action Assignment**: Gestionnaires assign actions to Responsables
5. **Responsible Status Update**: Responsables can update action status (In Progress)
6. **Evidence Submission**: Responsables submit evidence with description and attachments
7. **Evidence Validation**: Gestionnaires validate/reject evidence with notes
8. **Action Completion**: Actions marked complete after evidence validation
9. **Overdue Tracking**: System calculates overdue actions (DueDate < Today)
10. **Dashboard Metrics**: KPI display (Total, Completed, Overdue, By Status)
11. **Action History**: All changes logged with user, timestamp, old/new values
12. **Plan Hierarchy**: Multiple actions per plan, actions belong to single plan
13. **User Roles**: System supports 4 roles with specific permissions
14. **Directeur Approval**: Directeur approves/rejects submitted plans
15. **Permission Enforcement**: Users see only their accessible resources
16. **Report Generation**: Export plans and actions to PDF/Excel
17. **Compliance Reports**: Summary of completed vs. overdue actions
18. **Evidence Storage**: Attachments stored securely with virus scanning
19. **Password Reset**: Users can reset forgotten passwords
20. **Session Timeout**: Sessions expire after inactivity

### Technical Requirements (Rules 21-30)

21. **ASP.NET Core 8.0**: Mandatory framework (no alternatives)
22. **C# 12.0**: Language version requirement
23. **Entity Framework Core 8.0**: ORM for data access
24. **SQL Server**: Database technology (Code-First migrations)
25. **Pure CSS3**: No JavaScript, no Bootstrap, responsive design only
26. **PBKDF2 Hashing**: Password security using ASP.NET Core Identity
27. **HTTPS**: All communications encrypted
28. **CORS**: Cross-origin policies configured
29. **Dependency Injection**: All services registered in DI container
30. **Async/Await**: All database operations are async

### Data & Security (Rules 31-40)

31. **Date Handling**: All dates use `DateTime.Now.Date` (no time component)
32. **Unique References**: Plan and Action references must be globally unique
33. **Audit Trail**: Every change logged in ActionLog table
34. **User IP Tracking**: IP address captured in audit logs
35. **File Validation**: File type/size validation on upload
36. **Virus Scanning**: Uploaded files scanned for malware
37. **Cascading Deletes**: Related records deleted when parent deleted
38. **Soft Deletes**: Optional soft-delete support for audit compliance
39. **Concurrent Access**: Optimistic locking on sensitive updates
40. **Rate Limiting**: API endpoints rate-limited to prevent abuse

### Workflow & Business Logic (Rules 41-44)

41. **Plan Submission**: Gestionnaire submits complete plan to Directeur
42. **Approval Workflow**: Directeur approves/rejects, gestionnaire revises
43. **Status Transitions**: Only valid transitions allowed (no skipping phases)
44. **Evidence Requirement**: Action cannot complete without validated evidence

---

## Next Steps

### For User Validation (IMMEDIATE)

1. **Review this document** and confirm all architecture is correct
2. **Validate the 20 components** (Controllers, Services, ViewModels, etc.)
3. **Confirm the workflow phases** and state transitions
4. **Approve the implementation roadmap** and timeline

### For Development (After Validation)

1. **Clone new project** or create new ASP.NET Core 8.0 solution
2. **Execute Phase 1** (Database & Identity)
3. **Execute Phase 2** (Authentication)
4. **Continue through all 10 phases** per roadmap

### Deliverables This Document Provides

- ✅ Complete architecture breakdown
- ✅ Implementation phases with dependencies
- ✅ Validation checklist for user review
- ✅ Testing strategy
- ✅ Development roadmap
- ✅ 44-rule master prompt documentation
- ✅ Status tracking for all components

---

## Document Metadata

| Field | Value |
|-------|-------|
| Version | 0.1 WIP |
| Created | 2026-09-11 |
| Last Updated | 2026-09-11 |
| Status | PENDING USER VALIDATION |
| Project Name | GPA NOVEC |
| Technology | ASP.NET Core 8.0 MVC |
| Language | French (UI) / English (Code) |
| Owner | Habiba Arouada |
| Next Review | After Phase 1 Completion |

---

**END OF WIP PROJECT DOCUMENT**

*This document is a living specification and will evolve as the project progresses. All validation checkboxes must be reviewed and confirmed before Phase 1 implementation begins.*
