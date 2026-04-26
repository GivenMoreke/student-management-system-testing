
# Student Management System — Software Testing Project (SWT262)

A comprehensive software testing project applying both **White-box** and **Black-box** testing methodologies to a Student Course Management System. Developed as the SWT262 Software Testing assessment at Belgium Campus iTVersity.

The system under test is a Windows Forms desktop application built with a 3-tier architecture (Presentation, Business Logic, Data Access layers) connected to a SQL Server database.

---

## Project Structure

```
SWT262-Testing-Project/
├── PRG282_Project/              # Application under test
│   ├── BussinessLogicLayer/
│   │   └── BLL.cs               # Business logic & validation
│   ├── DataLayer/
│   │   ├── DataHandler.cs       # ADO.NET database operations
│   │   └── FileHandler.cs       # File I/O & user authentication
│   ├── Login.cs                 # Login form
│   ├── Main.cs                  # Main CRUD form
│   ├── Courses.cs               # Course management form
│   ├── Student.cs               # Student model
│   └── Course.cs                # Course model
├── Databases/
│   └── Project_DB_Script.sql    # Database setup script
└── Tests/                       # xUnit test project
```

---

## Application Under Test

The application manages student records and course enrolments for a campus. It includes:

- **Login system** — Username/password authentication stored in a text file
- **Student Management** — Full CRUD operations (Create, Read, Update, Delete, Search)
- **Course Management** — Full CRUD operations for module records
- **Enrolment Management** — Assign and manage student-course relationships

### Architecture

| Layer | Component | Responsibility |
|-------|-----------|---------------|
| Presentation | Windows Forms (Login, Main, Courses) | UI and user interaction |
| Business Logic | BLL.cs | Input validation and business rules |
| Data Access | DataHandler.cs, FileHandler.cs | Database and file operations |

---

## Testing Approach

### Task 1 — Test Plan
A formal test plan was created identifying all test suites, test cases, entry/exit criteria, and scope of testing based on the system requirements.

---

### Task 2 — White-box Testing (3 Test Suites)

Testing based on internal code structure and logic paths.

**Suite 1: Statement Coverage**
Ensures every executable statement in the code is executed at least once.

**Suite 2: Branch Coverage**
Ensures every decision point (if/else, loops) takes both true and false paths.

**Suite 3: Condition Coverage**
Ensures every individual boolean condition within decision statements evaluates to both true and false.

Each suite contains a minimum of 3 test cases covering both positive and negative scenarios.

---

### Task 3 — Black-box Testing (3 Test Suites)

Testing based on system requirements without knowledge of internal implementation.

**Suite 1: Boundary Value Analysis (BVA)**
Tests input values at the exact boundaries of valid ranges — minimum, maximum, and just outside the boundary.

**Suite 2: Equivalence Partitioning**
Divides input data into valid and invalid equivalence classes and tests a representative value from each partition.

**Suite 3: Decision Table Testing**
Tests combinations of input conditions and their corresponding system behaviour.

---

### Task 4 — Automated Testing with xUnit

Automated unit tests written using the **xUnit** framework targeting the `FileHandler` class.

#### Methods Tested

**`AddPicture(string FilePath)`**
Copies an image from a given file path to the application's Images folder and returns a unique filename.

**`CheckUser(string username, string password)`**
Reads `LoginDetails.txt` and verifies whether the given credentials match a stored user.

**`AddUser(string username, string password, string confirmPass)`**
Validates and registers a new user, returning error codes:
- `0` — Success
- `1` — Invalid username (contains space or comma)
- `2` — Invalid password (contains space or comma)
- `3` — Passwords do not match
- `4` — Username already exists

#### Test Structure (Arrange, Act, Assert)

```csharp
[Fact]
public void CheckUser_ValidCredentials_ReturnsTrue()
{
    // Arrange
    var fileHandler = new FileHandler();
    string username = "admin";
    string password = "1234";

    // Act
    bool result = fileHandler.CheckUser(username, password);

    // Assert
    Assert.True(result);
}
```

---

## Tech Stack

| Category | Technology |
|----------|-----------|
| Language | C# |
| Framework | .NET Framework / Windows Forms |
| Database | SQL Server |
| Data Access | ADO.NET with Stored Procedures |
| Testing Framework | xUnit |
| IDE | Visual Studio 2022 |

---

## Getting Started

### Prerequisites
- Visual Studio 2022
- SQL Server (local instance)
- .NET Framework
- xUnit NuGet package

### Setup

1. Clone the repository
2. Run `Databases/Project_DB_Script.sql` in SQL Server Management Studio
3. Open the solution in Visual Studio
4. Update the connection string in `App.config` to match your SQL Server instance
5. Build the solution
6. Run the xUnit test project via Test Explorer

---

## Key Concepts Demonstrated

- Formal test plan creation and documentation
- White-box testing: Statement, Branch, and Condition Coverage
- Black-box testing: BVA, Equivalence Partitioning, Decision Table Testing
- Automated unit testing using xUnit (FACT, Arrange, Act, Assert)
- Testing a 3-tier architecture application
- Identifying positive and negative test cases
- Testing file I/O operations and authentication logic

---



**Reitumetse Given Moreke**
Diploma in Information Technology — Belgium Campus iTVersity
[LinkedIn](#) | [GitHub](#)
