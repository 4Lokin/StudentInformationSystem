# Student Information System (SIS)

**Course:** Programming with Generative AI  
**Author:** Nikola Kühne  
**Due Date:** 30.06.2025  

---

## Table of Contents

1. [Overview](#overview)  
2. [Technology Stack](#technology-stack)  
3. [Prerequisites](#prerequisites)  
4. [Setup & Installation](#setup--installation)  
5. [Project Structure](#project-structure)  
6. [Features & Pages](#features--pages)  
7. [Database Schema](#database-schema)  
8. [Deployment](#deployment)  
9. [Testing & Known Issues](#testing--known-issues)  
10. [Grading Alignment](#grading-alignment)  

---

## Overview

SIS is an ASP.NET Web Forms application in VB.NET backed by a PostgreSQL database (Supabase). It provides two user roles—admin and student—with role-based navigation and functionality:

- Admins can manage students, courses and view all enrollments and statistics  
- Students can enroll in courses and view their own enrollments  

A confirmation email is sent upon successful student creation.

---

## Technology Stack

- **Backend:** VB.NET Web Forms (.NET Framework 4.8)  
- **Frontend:** ASP.NET Web Forms, Bootstrap 5  
- **Database:** PostgreSQL via Supabase, accessed with Npgsql  
- **Charts:** Chart.js  
- **Hosting:** Azure App Service (or any IIS host)  
- **Version Control:** Git & GitHub  

---

## Prerequisites

- Microsoft Visual Studio 2019 or later  
- .NET Framework 4.7.2 or higher Developer Pack  
- PostgreSQL database (e.g. Supabase)  
- Npgsql ADO.NET provider (NuGet)  
- SMTP credentials for email notifications  

---

## Setup & Installation

1. **Clone the repository**  
   ```bash
   git clone https://github.com/4Lokin/StudentInformationSystem.git
   cd StudentInformationSystem
2.	Open solution
Open StudentInformationSystem.sln in Visual Studio.
3.	Configure connection string
In Web.config under <connectionStrings>
<add name="SupabaseConnection"
     connectionString="Host=YOUR_HOST;Port=5432;Database=YOUR_DB;Username=YOUR_USER;Password=YOUR_PASS;SSL Mode=Require;Trust Server Certificate=true"
     providerName="Npgsql" />
3.	
4.	Configure SMTP settings
In Web.config under <system.net>
<mailSettings>
  <smtp from="noreply@example.com">
    <network host="smtp.example.com"
             port="587"
             userName="smtp_user"
             password="smtp_pass"
             enableSsl="true" />
  </smtp>
</mailSettings>
4.	
5.	Run SQL scripts
Execute the SQL files in sql/schema.sql
CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  username VARCHAR(50) UNIQUE NOT NULL,
  password_hash VARCHAR(255) NOT NULL,
  role VARCHAR(20) NOT NULL
);

CREATE TABLE students (
  id SERIAL PRIMARY KEY,
  first_name VARCHAR(100) NOT NULL,
  last_name VARCHAR(100) NOT NULL,
  email VARCHAR(255) NOT NULL,
  enrollment_date DATE NOT NULL
);

CREATE TABLE courses (
  course_id SERIAL PRIMARY KEY,
  course_name VARCHAR(255) NOT NULL,
  ects INTEGER NOT NULL,
  hours INTEGER NOT NULL,
  format VARCHAR(50) NOT NULL,
  instructor VARCHAR(255) NOT NULL
);

CREATE TABLE enrollments (
  id SERIAL PRIMARY KEY,
  student_id INTEGER REFERENCES students(id),
  course_id INTEGER REFERENCES courses(course_id)
);
5.	
6.	Insert an admin user (optional)
INSERT INTO users(username,password_hash,role)
VALUES('admin','<hashed_password>','admin');

 
Project Structure
StudentInformationSystem/
├ Account/
│  ├ Login.aspx
│  ├ Login.aspx.vb
│  ├ Register.aspx
│  └ Register.aspx.vb
├ Default.aspx
├ Default.aspx.vb
├ ManageStudents.aspx
├ ManageStudents.aspx.vb
├ ManageCourses.aspx
├ ManageCourses.aspx.vb
├ EnrollCourses.aspx
├ EnrollCourses.aspx.vb
├ ManageEnrollments.aspx
├ ManageEnrollments.aspx.vb
├ CourseStatistics.aspx
├ CourseStatistics.aspx.vb
├ Site.Master
├ Site.Master.vb
├ Web.config
├ sql/
│  ├ schema.sql
│  └ data.sql
└ StudentInformationSystem.sln
 
Features & Pages

Default.aspx
•	Greeting: Enter name in t_entername, click Click me, output in msg_box
•	Calculator:
o	Inputs: txtNumber1, txtNumber2
o	Operator selector: ddlOperator
o	Button: btnCalculate
o	Output: lblResult inside ResultPanel
o	Validation for numeric input and division by zero
•	Dashboard: Bar chart (Chart.js) showing number of students per course

ManageStudents.aspx
•	CRUD for student records
•	GridView with HTML decoding to avoid XSS errors
•	Server-side validation (email via regex)
•	Email confirmation after student creation (SendEnrollmentEmail)

ManageCourses.aspx
•	CRUD for courses
•	GridView and Bootstrap styling
•	Role-based access in Page_Load (Session("role") = "admin")

EnrollCourses.aspx
•	Students enroll in courses via dropdowns
•	View and delete own enrollments

ManageEnrollments.aspx
•	Admin view of all enrollments
•	Delete enrollments

CourseStatistics.aspx
•	Course statistics visualization (Chart.js)

Site.Master
•	Navigation links in <asp:PlaceHolder> controls
•	Role-based visibility (phAdminEnrollments.Visible = (role = "admin"), phStudentEnroll.Visible = (role = "student"))

Account/Login & Register
•	Sets Session("username") and Session("role")
•	Roles: admin or student
 
Database Schema
Table	Columns
users	id, username, password_hash, role
students	id, first_name, last_name, email, enrollment_date
courses	course_id, course_name, ects, hours, format, instructor
enrollments	id, student_id → students.id, course_id → courses.course_id
SQL scripts are in the sql/ folder.
 
Deployment
•	Azure App Service
1.	Right-click project → Publish → Azure App Service
2.	Create or select an App Service → Publish
•	Alternative IIS Hosting
o	Adjust web.config for target environment
o	Deploy via FTP or WebDeploy
 
Testing & Known Issues
•	Test Accounts
o	Admin: admin / Password123!
o	Student: student / Password123!
•	Validation
o	Required fields, email format, date format
•	Known Issues
o	None currently; please report bugs via GitHub Issues
 
Grading Alignment
Requirement	Implementation
Project Setup	VS Web Forms template, Supabase connection
Student Management (CRUD)	ManageStudents.aspx
Course Management	ManageCourses.aspx
Course Enrollment	EnrollCourses.aspx, ManageEnrollments.aspx
Data Visualization	Chart.js in Default.aspx, CourseStatistics.aspx
Authentication & Roles	ASP.NET Session, role-based MasterPage
Responsive UI (Bootstrap 5)	All pages via Site.Master
Email Notifications	SendEnrollmentEmail in ManageStudents.aspx.vb
Documentation & Presentation	README.md, PowerPoint documentation

