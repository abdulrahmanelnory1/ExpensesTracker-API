# Expenses Tracker - Personal Finance Management System

**A sophisticated RESTful API backend service built with ASP.NET Core that provides comprehensive personal finance management capabilities. This solution enables users to efficiently track expenses, manage budgets, and maintain financial discipline through secure, well-structured API endpoints. Features clean architecture patterns, JWT authentication, and robust data management.**

## Core Features

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

## Technical Stack

### Backend Framework
- **ASP.NET Core 7.0** - High-performance web API framework with modern development features
- **Entity Framework Core 7.0** - Advanced object-relational mapper with code-first database design
- **SQL Server** - Enterprise-grade relational database management system

### Security & Authentication
- **JWT Bearer Tokens** - Industry-standard HMAC-SHA256 algorithm for secure token generation
- **ASP.NET Core Identity** - Comprehensive user management and password security framework
- **Claims-based Authorization** - Granular access control using custom claim types
- **Secure API Protection** - End-to-end security implementation for all financial operations

### Additional Technologies
- **Swagger/OpenAPI** - Interactive API documentation with testing capabilities
- **Background Services** - Reliable recurring task processing for automated financial transactions
- **Dependency Injection** - Loosely coupled architecture promoting maintainability and testability

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

## Database Schema

### Core Entities
- **Users** - Comprehensive user profiles, authentication details, and financial preferences
- **Expenses** - Complete expense records with transaction dates and automatically calculated totals
- **Items** - Individual expense line items with detailed categorization and pricing
- **Categories** - Flexible classification system supporting both default and user-defined categories
- **Budgets** - Advanced budget planning with time periods, limits, and real-time tracking                                      
- **RecurringExpenses** - Automated transaction templates with scheduling configurations (weekly, monthly, yearly, daily intervals) inherits from Expense using TPT - Table Per Type inheritance

### Strategic Relationships
- **User → Expenses** (One-to-Many relationship)
- **Expense → Items** (One-to-Many hierarchical relationship) 
- **Item → Category** (Many-to-One classification relationship)
- **Category → Budget** (One-to-Many planning relationship)
- **RecurringExpense → Expense** (Inheritance relationship using TPT mapping strategy)
