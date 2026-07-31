# PetHealth API

API REST de gestion de la santé animale. Elle permet de gérer des animaux de compagnie, leurs propriétaires, les rendez-vous vétérinaires, les vaccinations, le suivi de poids ainsi que les utilisateurs et leurs rôles.

Le projet est construit selon une **architecture en couches (Clean Architecture)** avec une séparation stricte des responsabilités, un pattern **CQS** (Command Query Separation) et un accès aux données via **Dapper** sur des procédures stockées SQL Server.

---

## 🧰 Technologies utilisées

| Domaine | Technologie / Librairie | Version |
|---|---|---|
| Plateforme | .NET | 10.0 |
| Framework web | ASP.NET Core Web API | net10.0 |
| Langage | C# (Nullable + ImplicitUsings activés) | — |
| Accès aux données | [Dapper](https://github.com/DapperLib/Dapper) (micro-ORM) | 2.1.79 |
| Base de données | Microsoft SQL Server (procédures stockées) | — |
| Driver SQL | Microsoft.Data.SqlClient | 7.1.0-preview1 |
| Authentification | JWT — Microsoft.AspNetCore.Authentication.JwtBearer | 10.0.9 |
| Documentation API | Microsoft.AspNetCore.OpenApi + [Scalar](https://scalar.com/) | 10.0.8 / 2.14.14 |
| Logging | [Serilog](https://serilog.net/) (Console + fichiers journaliers) | 4.x / 10.0.0 |
| Secrets | User Secrets (.NET Secret Manager) | — |

---

## 🏗️ Architecture

La solution est découpée en **4 projets** suivant le principe de dépendance vers l'intérieur (les couches externes dépendent des couches internes, jamais l'inverse) :

```
PetHealth.API            →  Couche présentation (Controllers, Program.cs, configuration)
   │
   ├── PetHealth.Application   →  Couche métier (CQS, DTOs, interfaces de repository, mapping, Result/Error, JWT settings)
   │       │
   │       └── PetHealth.Domain   →  Entités du domaine (modèle métier pur, sans dépendance)
   │
   └── PetHealth.Infrastructure  →  Implémentations (services Dapper, accès SQL, DI, type handlers)
```

### Couche `PetHealth.Domain`
Modèle métier pur (aucune dépendance externe). Entités : `AppUser`, `AppUserRole`, `Role`, `Pet`, `Appointment`, `Vaccination`, `WeightRecord`, `Medicine`, `MedicalDocument`, `Prescription`, `PrescriptionItem`, `Treatment`.

### Couche `PetHealth.Application`
- **CQS** : interfaces `ICommandDefinitionAsync`, `IQueryDefinitionAsync`, `ICommandHandlerAsync`, `IQueryHandlerAsync` qui séparent clairement les opérations d'écriture (Commands) des opérations de lecture (Queries).
- **DTOs** : objets de transfert dédiés à chaque cas d'usage (Insert, Update, Login, etc.).
- **Mapping** : mappeurs manuels (`PetMapper`, `AppUserMapper`, `AppointmentMapper`, …) Entité ↔ DTO.
- **Result / Error** : pattern *Result* (`Result`, `Result<T>`, `Error`) pour un retour d'erreur explicite sans exceptions remontant jusqu'au contrôleur.
- **Repositories** : interfaces (`IPetRepository`, `IAppUserRepository`, …) composées des handlers CQS correspondants.
- **Security** : `JwtSettings`.

### Couche `PetHealth.Infrastructure`
- **Services** : implémentations des repositories via **Dapper**, appelant des **procédures stockées** (`Usp_Pet_GetAll`, `Usp_Pet_Insert`, …).
- **`CurrentUserRepository`** : implémentation de `ICurrentUserRepository` qui lit l'`Id` utilisateur (claim `sub`) depuis le `ClaimsPrincipal` courant via `IHttpContextAccessor`.
- **TypeHandlers** : `DateOnlyTypeHandler` pour mapper `DateOnly` ↔ `DATE` SQL.
- **Extensions** :
  - `DependencyInjectionExtensions` : enregistrement de la connexion DB et des services (scoped).
  - `CurrentUserExtensions` : injection de l'`UserId` courant dans les paramètres Dapper.
- **Configuration** : `DapperConfig` (enregistrement des type handlers).

### Couche `PetHealth.API`
- Contrôleurs REST minces : ils délèguent aux repositories et traduisent le `Result` en réponse HTTP appropriée (`200`, `201`, `204`, `400`, `404`, `500`).
- **Middlewares** : `GlobalExceptionHandler` (`IExceptionHandler`) capture toute exception non gérée par les contrôleurs et retourne une réponse `ProblemDetails` uniforme.
- Configuration de l'authentification JWT, de Serilog, d'OpenAPI/Scalar et de la connexion BDD dans `Program.cs`.

---

## ⚠️ Gestion des erreurs

Deux niveaux complémentaires :
- **Result pattern** (`PetHealth.Application`) : les échecs métier attendus (ressource introuvable, validation, conflit) sont retournés explicitement par les services sous forme de `Result` / `Result<T>`, puis traduits par les contrôleurs en réponse HTTP appropriée.
- **`GlobalExceptionHandler`** (`PetHealth.API/Middlewares`) : capture toute exception non gérée remontant dans le pipeline ASP.NET Core (`SqlException`, `ValidationException`, `UnauthorizedAccessException`, `KeyNotFoundException`, etc.), la journalise via Serilog et retourne une réponse `ProblemDetails` avec le code HTTP correspondant.

---

## 🔐 Sécurité & Authentification

- Authentification par **JWT Bearer** (HMAC-SHA256).
- Claims embarqués dans le token : `sub` (Id), `name` (prénom), `email` (email) et le claim personnalisé `Role`.
- `MapInboundClaims = false` sur le handler JWT (`Program.cs`) : les claims du token conservent leur type d'origine (`sub`, `name`, `email`, `Role`) au lieu d'être réécrits vers les URI longues `ClaimTypes.*` par le mapping par défaut d'ASP.NET Core. Nécessaire pour que `CurrentUserRepository` retrouve bien le claim `sub`.
- Autorisation par rôle au niveau des contrôleurs / actions (`[Authorize]` / `[Authorize(Roles = "...")]`) — la quasi-totalité des contrôleurs est désormais protégée :
  - `AppUserRoleController` → `Admin` uniquement.
  - `MedicineController` → `Admin, Vet`.
  - `AppointmentController`, `MedicalDocumentController`, `PrescriptionController`, `TreatmentController`, `VaccinationController`, `PetController`, `WeightRecordController` → utilisateur authentifié, avec restrictions `Admin` / `Vet` selon l'action (lecture globale / écriture).
  - `AppUserController` → utilisateur authentifié, avec restrictions `Admin` / `Vet` selon l'action.
- Génération du token via `LoginController` (validité 30 min). Un endpoint `POST /Login/Logout` existe mais ne fait pour l'instant qu'invalider le token côté client — pas de révocation/blacklist côté serveur (JWT stateless).

> ⚠️ **Note de sécurité** : la chaîne de connexion et l'intégralité des paramètres `Jwt` (`SecretKey`, `Issuer`, `Audience`, `ExpirationInMinutes`) sont exclusivement fournis via les **User Secrets** en développement — `appsettings.json` ne contient plus aucune valeur sensible en clair. En production, utilisez des variables d'environnement ou un coffre-fort de secrets (Azure Key Vault, etc.), jamais de valeurs committées.

---

## 🌐 CORS

Une politique CORS nommée `AngularClient` est déclarée dans `Program.cs` pour autoriser un client front-end Angular en développement local :

- Origine autorisée : `http://localhost:4200`
- En-têtes et méthodes : tous autorisés (`AllowAnyHeader`, `AllowAnyMethod`)
- Pas de `AllowCredentials()` : le token JWT transite par l'en-tête `Authorization`, pas par un cookie.

---

## 📡 Endpoints principaux

| Ressource | Route de base | Verbes | Accès |
|---|---|---|---|
| Authentification | `POST /Login` | POST | Public |
| Inscription | `POST /Login/Register` | POST | Public |
| Déconnexion | `POST /Login/Logout` | POST | Authentifié |
| Animaux | `/api/Pet` | GET, POST, PUT, PATCH, DELETE | Authentifié (liste complète / écriture : Admin, Vet) |
| Utilisateurs | `/api/AppUser` | GET, POST, PUT, PATCH, DELETE | Authentifié (lecture : Admin, Vet ; écriture/suppression : Admin) |
| Rôles utilisateurs | `/api/AppUserRole` | GET, POST, DELETE | Admin |
| Rendez-vous | `/api/Appointment` | GET, POST, PUT, DELETE | Authentifié (liste complète / écriture : Admin, Vet) |
| Documents médicaux | `/api/MedicalDocument` | GET, POST, PUT, DELETE | Admin, Vet (suppression : Admin) |
| Médicaments | `/api/Medicine` | GET | Admin, Vet |
| Prescriptions | `/api/Prescription` (+ `/api/Prescription/Item` pour les lignes de prescription) | GET, POST, PUT, DELETE | Admin, Vet |
| Traitements | `/api/Treatment` (+ `/api/Treatment/User` pour la liste de l'utilisateur courant) | GET, POST, PUT, DELETE | Authentifié (liste complète / écriture : Admin, Vet ; suppression : Admin) |
| Vaccinations | `/api/Vaccination` | GET, POST, PUT, DELETE | Admin, Vet |
| Suivi de poids | `/api/WeightRecord` | GET, POST, PUT, DELETE | Authentifié (suppression : Admin, Vet) |

La documentation interactive complète est disponible via **Scalar** en environnement de développement (voir ci-dessous).

---

## 🚀 Démarrage

### Prérequis
- [.NET SDK 10.0](https://dotnet.microsoft.com/)
- Une instance **SQL Server** avec les procédures stockées (`Usp_*`) et le schéma attendus.

### Configuration

La chaîne de connexion et les paramètres JWT sont lus depuis la configuration. `Jwt:Issuer`, `Jwt:Audience` et `Jwt:SecretKey` sont **requis** (`JwtSettings`) — l'application ne démarre pas sans eux. En développement, utilisez les **User Secrets** :

```bash
cd PetHealth.API
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=PetHealthRecord;User Id=...;Password=...;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:SecretKey" "votre_cle_secrete_suffisamment_longue"
dotnet user-secrets set "Jwt:Issuer" "pethealth-api"
dotnet user-secrets set "Jwt:Audience" "pethealth-clients"
dotnet user-secrets set "Jwt:ExpirationInMinutes" "30"
```

> ⚠️ Ce sont des exemples de structure, pas les vraies valeurs du projet — la chaîne de connexion et la `SecretKey` ne doivent jamais apparaître en clair dans un fichier suivi par git (voir l'incident corrigé dans l'historique du repo).

### Lancement

```bash
dotnet restore
dotnet run --project PetHealth.API
```

L'API démarre sur :
- HTTP : `http://localhost:5126`
- HTTPS : `https://localhost:7189`

### Documentation API (développement)

Une fois lancée en mode `Development`, la référence API Scalar est disponible :

```
https://localhost:7189/scalar/v1
```

Grâce à `BearerSecuritySchemeTransformer` (`PetHealth.API/ScalarExtension`), un schéma de sécurité Bearer est injecté dans le document OpenAPI : un bouton d'authentification est disponible directement dans l'UI Scalar pour renseigner le token JWT et tester les endpoints protégés.

Le document OpenAPI brut est exposé sur `/openapi/v1.json`.

---

## 🪵 Logging

Serilog est configuré dans `Program.cs` avec :
- **Console** (thème coloré).
- **Fichiers journaliers** : `logs/application.log` (niveau ≥ Information) et `logs/Fatal-application.log` (niveau Fatal).
- Rotation quotidienne, 10 fichiers conservés, limite de 40 Mo par fichier.

---

## 📦 Structure de la solution

```
API_PetHealth/
├── API_PetHealth.slnx
├── PetHealth.API/              # Présentation : Controllers, Middlewares, ScalarExtension, Program.cs, appsettings
├── PetHealth.Application/      # Métier : CQS, DTOs, Repositories, Mapping, Results, Security
├── PetHealth.Domain/          # Entités du domaine
└── PetHealth.Infrastructure/  # Accès données (Dapper), Services, Extensions, TypeHandlers
```

---

## 🧩 Patterns & conventions

- **Clean Architecture** : dépendances dirigées vers le domaine.
- **CQS** : séparation Commands (écriture) / Queries (lecture).
- **Result pattern** : gestion d'erreur explicite (`Result` / `Result<T>` + `Error`), pas d'exceptions non maîtrisées dans les contrôleurs.
- **Repository pattern** : abstraction de l'accès aux données via interfaces dans la couche Application.
- **Stored procedures** : toute la logique d'accès SQL passe par des procédures stockées appelées avec Dapper.
- **DTO + Mapping manuel** : aucune fuite des entités du domaine vers l'extérieur.
