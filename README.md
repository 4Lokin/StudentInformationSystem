# Student Information System (SIS)

**Course:** Programming with Generative AI  
**Author:** Nikola Kühne  
**Due Date:** 30.06.25  

---

## 📖 Table of Contents

1. [Overview](#overview)  
2. [Technology Stack](#technology-stack)  
3. [Setup & Installation](#setup--installation)  
4. [Project Structure](#project-structure)  
5. [Features & Pages](#features--pages)  
6. [Database Schema](#database-schema)  
7. [Deployment](#deployment)  
8. [Testing & Known Issues](#testing--known-issues)  
9. [Grading Alignment](#grading-alignment)  

---

## Overview

A VB.NET Web Forms application backed by Supabase (PostgreSQL), enabling admin and student roles to manage student records, courses, enrollments, and visualize data.

---

## Technology Stack

- **Backend:** VB.NET Web Forms (.NET Framework 4.8)  
- **Frontend:** ASP.NET Web Forms + Bootstrap 5  
- **Database:** Supabase (PostgreSQL) via Npgsql  
- **Charts:** Chart.js  
- **Hosting:** Azure App Service (or SmarterASP.NET)  
- **Version Control:** GitHub  

---

## Setup & Installation

1. **Clone repository**  
   ```bash
   git clone https://github.com/YourUsername/SISProject.git
   - Removed `<system.codedom>` from `web.config` so the default .NET Framework VB compiler is used (no bin\roslyn folder needed).  
    - Installed Npgsql v4.1.8 for PostgreSQL connectivity (compatible with .NET 4.7.2):
  ```powershell
  Install-Package Npgsql -Version 4.1.8
  - Added SupabaseConnection in Web.config under `<connectionStrings>`:
  ```xml
  <add name="SupabaseConnection"
       connectionString="Host=db.xvieysbmxkcndxmvkpax.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=…;SSL Mode=Require;Trust Server Certificate=true;Max Auto Prepare=0"
       providerName="Npgsql" />

### Default.aspx

- **Greeting**: Enter your name in `t_entername` and click **Click me** (`mybutton_Click`).  
  Displays a sanitized welcome message in `msg_box`.

- **Calculator**:  
  - Inputs: `txtNumber1`, `txtNumber2`  
  - Operator selector: `ddlOperator`  
  - Button: `btnCalculate` (`btnCalculate_Click`)  
  - Output: `lblResult` inside `ResultPanel`  
  - Error handling for invalid numbers and division by zero 

  ---
## Project Structure

```` ```text ````
/SISProject
│
├─ StudentInformationSystem.sln
├─ README.md
└─ StudentInformationSystem/
   ├─ Default.aspx
   ├─ Default.aspx.vb
   ├─ ManageStudents.aspx
   ├─ ManageStudents.aspx.vb
   ├─ ManageCourses.aspx
   ├─ ManageCourses.aspx.vb
   ├─ Enrollments.aspx
   ├─ Enrollments.aspx.vb
   ├─ Site.Master
   ├─ web.config
   └─ sql/
      ├─ schema.sql
      └─ data.sql
```` ``` ````

---


## Features & Pages

- **Default.aspx**  
  - **Greeting**: Enter your name in `t_entername`, click **Click me** (`mybutton_Click`), shows a sanitized welcome in `msg_box` and clears the textbox.  
  - **Calculator**:  
    - Inputs: `txtNumber1`, `txtNumber2`  
    - Operator selector: `ddlOperator`  
    - Button: `btnCalculate` (`btnCalculate_Click`)  
    - Output: `lblResult` inside `ResultPanel`  
    - Error handling for invalid inputs and division by zero  

- **ManageStudents.aspx**  
  - CRUD interface for student records (Create, Read via GridView, Update, Delete)  
  - Server‐side validation and Bootstrap styling  

- **ManageCourses.aspx**  
- Full **CRUD** on student records:
    - **Create:** Enter first name, last name, email, enrollment date → **Create**  
    - **Read:** GridView lists all students with **Select** button  
    - **Update:** After selecting a row, modify fields → **Update**  
    - **Delete:** After selecting a row → **Delete**  
  - Uses Npgsql to connect to Supabase via `SupabaseConnection`  
  - Shows feedback messages in `pnlMessage` with Bootstrap alerts    

- **Enrollments.aspx**  
  - Enroll students in courses via dropdowns  
  - View and delete existing enrollments  

- **Account/**  
  - ASP.NET Individual Accounts for login & registration  
  - Role‐based access control (Admin vs. Student)  

- **Dashboard** (integrated into Default.aspx)  
  - Chart.js bar chart showing the number of students per course  


---

## Database Schema

- **students**  
  - `id` INT PRIMARY KEY AUTO-INCREMENT  
  - `first_name` TEXT NOT NULL  
  - `last_name` TEXT NOT NULL  
  - `email` TEXT UNIQUE NOT NULL  
  - `enrollment_date` DATE  

- **courses**  
  - `course_id` INT PRIMARY KEY AUTO-INCREMENT  
  - `course_name` TEXT NOT NULL  
  - `ects` INT NOT NULL  
  - `hours` INT NOT NULL  
  - `format` TEXT NOT NULL  
  - `instructor` TEXT NOT NULL  

- **enrollments**  
  - `enrollment_id` INT PRIMARY KEY AUTO-INCREMENT  
  - `student_id` INT REFERENCES students(id)  
  - `course_id` INT REFERENCES courses(course_id)  
  - `enrollment_date` DATE NOT NULL  

SQL scripts live in `sql/schema.sql` and `sql/data.sql`.

---

## Deployment

- **Azure App Service**  
  1. Right-click project → **Publish** → **Azure App Service** (Windows)  
  2. Create new App Service or select existing  
  3. Click **Publish**  
  4. Visit `https://your-app.azurewebsites.net`  

- **SmarterASP.NET** (alternative)  
  1. Publish via FTP or Visual Studio to SmarterASP.NET  
  2. Ensure `web.config` connection strings match hosting environment  

---

## Testing & Known Issues

- **Test Accounts**  
  - Admin: `admin@example.com` / `Password123!`  
  - Student: `student@example.com` / `Password123!`  

- **Validation**  
  - Calculator checks numeric input and division by zero  
  - CRUD pages validate required fields  

- **Known Issues**  
  - None currently; please report bugs via GitHub Issues  

---

## Grading Alignment

| Requirement                    | Implementation                          |
|--------------------------------|-----------------------------------------|
| Project Setup                  | VS template, Supabase connection        |
| Student Management (CRUD)      | ManageStudents.aspx                    |
| Course Management              | ManageCourses.aspx                     |
| Course Enrollment              | Enrollments.aspx                       |
| Data Visualization             | Chart.js in Default.aspx                |
| Authentication & Roles         | ASP.NET Individual Accounts             |
| Responsive UI (Bootstrap)      | All pages / Site.Master                |
| Email Notifications (Bonus)    | To be added in Enrollments.aspx         |
| Documentation & Presentation   | README.md, XML comments, PPT            |