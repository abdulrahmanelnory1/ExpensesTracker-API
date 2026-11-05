# Expenses Tracker - Personal Finance Management System
**A comprehensive personal finance management application built with ASP.NET Core Web API that helps users track their expenses, manage budgets, and maintain financial discipline. Features enterprise-grade architecture patterns and robust security implementation. ASP.NET Core API and SQL Server 2022 — is fully containerized and orchestrated with Docker Compose, using a multi-stage Dockerfile, an explicit bridge network for service isolation, and a named volume for data persistence. The image is published publicly on GitHub Container Registry and can be pulled and run.**

## Run the Application

### Step 1 - Retrieve the Application

You have two options. Choose one.
 
#### Option A - Pull the Pre-Built Image (fastest, no source code needed)
 
The image is published publicly on GitHub Container Registry, so no build step is required.
 
```bash
docker pull ghcr.io/abdulrahmanelnory1/expenses-tracker-api:1.0.0
```
 
Verify the pull succeeded:

```bash
docker image ls --filter=reference="ghcr.io/abdulrahmanelnory1/expenses-tracker-api"
```
Expected output (the ID and size will differ):

```text
REPOSITORY                                         TAG       IMAGE ID       SIZE
ghcr.io/abdulrahmanelnory1/expenses-tracker-api    1.0.0     <image-id>     a few hundred MB
```

Because you are not cloning the repository, you also need the Compose file that describes how the two containers work together. Create an empty folder (for example `expenses-tracker`) and save this as `docker-compose.yml` inside it:
 
```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    restart: unless-stopped
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: ${SA_PASSWORD:?Set SA_PASSWORD in your .env file}
    volumes:
      - sqlserver_data:/var/opt/mssql
    healthcheck:
      test:
        [
          "CMD-SHELL",
          "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P \"$$MSSQL_SA_PASSWORD\" -C -Q 'SELECT 1' || exit 1"
        ]
      interval: 10s
      timeout: 5s
      retries: 10
      start_period: 20s
    networks:
      - backend
 
  api:
    image: ghcr.io/abdulrahmanelnory1/expenses-tracker-api:1.0.0
    restart: unless-stopped
    ports:
      - "8080:8080"
    environment:
      ConnectionStrings__ExpTracCon: "Server=sqlserver;Database=ExpensesTrackerDB;User Id=sa;Password=${SA_PASSWORD:?Set SA_PASSWORD in your .env file};TrustServerCertificate=True"
    depends_on:
      sqlserver:
        condition: service_healthy
    networks:
      - backend
 
networks:
  backend:
    driver: bridge
 
volumes:
  sqlserver_data:
```
 
#### Option B - Clone the Source and Build Locally
 
```bash
git clone https://github.com/abdulrahmanelnory1/ExpensesTracker-API.git
cd ExpensesTracker-API
```
 
Build the image from the multi-stage Dockerfile:
 
```bash
docker build -t expensestracker-api .
```
 
---
 
### Step 2 - Create the Environment File
 
The application reads the SQL Server password from an environment variable. It is **never baked into the image**.
 
Create a file named `.env` in the project root, **next to `docker-compose.yml`**:
 
```env
SA_PASSWORD=YourStrong!Passw0rd123
```
 
> On Windows, make sure the file is named exactly `.env` and not `.env.txt`.


```bash
# .gitignore
.env
 
# .dockerignore
.env
```

### Step 3 - Start the Full Stack
 
```bash
docker compose up -d
```
 
If you chose Option B and want Compose to build the image from your local source, use:
 
```bash
docker compose up -d --build
```

### Step 4 - Verify the Stack Is Healthy

```bash
docker compose ps
```

#### Verify the API is responding

```bash
curl http://localhost:8080/swagger
```


## Core Features

### Docker & Containerization
- **Fully containerized stack** - the ASP.NET Core API and SQL Server 2022 run as two containers orchestrated with Docker Compose
- **Multi-stage Dockerfile** - compiles the app in an SDK stage and ships only the runtime output, keeping the final image small
- **Explicit bridge network** - both services share a dedicated backend network for DNS-based service discovery and isolation
- **Named volume** - SQL Server data is persisted in sqlserver_data, surviving docker compose down (destroyed only with down -v)

### Intelligent Expense Management
- **Multi-item Transaction Recording** - Capture complex purchases with multiple items in a single expense entry
- **Automated Total Calculation** - System automatically sums individual item prices to calculate expense totals
- **Date-based Organization** - Filter and retrieve expenses by specific dates or date ranges
- **Chronological Tracking** - View expenses sorted by transaction date for spending pattern analysis
- **Expense Analytics** - Smart grouping and insights into spending habits over different time periods

### Dynamic Category System
- **Default Category Library** - Pre-defined categories for immediate item classification
- **Personalized Category Creation** - Users can design custom categories for their ususal spends
- **Category Modification** - Update category names and properties with real-time item synchronization
- **Intelligent Category Deletion** - Safe removal with automatic item reassignment to prevent orphaned records

### Advanced Budget Control System
- **Category-specific Spending Limits** - Assign individual budget constraints to different expense categories
- **Time-bound Financial Planning** - Define budget periods with precise start and end dates
- **Real-time Budget Tracking** - Automatic deduction from budget limits as expenses are recorded

### Background service: Automated Recurring Transactions
- **Flexible Scheduling System** - Configure recurring expenses for daily, weekly, monthly, or yearly intervals
- **Background Service Processing** - Automated execution of scheduled transactions without user intervention
- **Intelligent Date Management** - Smart handling of month-end variations and special date scenarios
- **Manual Transaction Override** - Flexibility to skip, modify, or pause individual recurring instances

### Comprehensive User Financial Management
- **Real-time Balance Synchronization** - Automatic balance updates with each financial transaction
- **Dynamic Income Management** - Adjust income levels with historical tracking and reporting
- **Secure Profile Management** - Protected email and password updates with verification workflows
- **Financial Health Dashboard** - Consolidated overview of income, expenses, and budget performance

### Enterprise Security Implementation
- **JWT Bearer Token Authentication** - Secure user authentication using HMAC-SHA256 algorithm
- **User Registration System** - Complete sign-up process with email verification capabilities
- **Claims-based Authorization** - Fine-grained access control for all financial operations
- **Protected API Endpoints** - Comprehensive security coverage across all financial data access points

## Architecture & Design Patterns

### Repository Pattern + Unit of Work
The application implements a robust **Repository Pattern** combined with **Unit of Work** to ensure clean separation of concerns and maintainable code structure:

#### Repository Pattern Implementation
- **Data Access Abstraction** - Complete decoupling of business logic from data persistence layers
- **Centralized Data Operations** - Consistent data access patterns across all entities
- **Testability** - Easy mocking of repositories for comprehensive unit testing

#### Unit of Work Pattern Implementation
- **Atomic Transaction Management** - Ensures consistent operations across multiple repositories
- **Single Database Context** - Shared context across all repositories within a business transaction
- **Performance Optimization** - Single `SaveChanges()` call for multiple operations

## API Endpoints

### Authentication Endpoints
`POST   /api/auth/login`        - Authenticate user and return JWT token  
`POST   /api/auth/register`     - Create new user account  
`PUT    /api/auth/email`        - Update user email address  
`PUT    /api/auth/password`     - Change user password

### Expense Endpoints
`GET    /api/expenses`                   - Retrieve all expenses  
`GET    /api/expenses/by-date`           - Get expenses by specific date  
`GET    /api/expenses/sorted-by-date`    - Get expenses sorted chronologically  
`POST   /api/expenses`                   - Create new expense with items  
`PUT    /api/expenses/{id}`              - Update existing expense  
`DELETE /api/expenses/{id}`              - Delete expense and all associated items

### Category Endpoints
`GET    /api/categories`                 - Get all categories (default + custom) with their associated budgets                  
`GET    /api/categories/{id}`            - Get specific category details with its associated budget                             
`POST   /api/categories`                 - Create new custom category with optional budget association                          
`PUT    /api/categories/{id}`            - Update category information with to ability to add or remove budget association       
`DELETE /api/categories/{id}`            - Delete category with item reassignment

### User Endpoints
`PUT    /api/users/change-income`        - Update user income amount (Requires Authorization)  
`PUT    /api/users/income`               - Add additional income to user balance (Requires Authorization)
