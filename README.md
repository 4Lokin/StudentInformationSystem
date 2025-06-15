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

### Default.aspx

- **Greeting**: Enter your name in `t_entername` and click **Click me** (`mybutton_Click`).  
  Displays a sanitized welcome message in `msg_box`.

- **Calculator**:  
  - Inputs: `txtNumber1`, `txtNumber2`  
  - Operator selector: `ddlOperator`  
  - Button: `btnCalculate` (`btnCalculate_Click`)  
  - Output: `lblResult` inside `ResultPanel`  
  - Error handling for invalid numbers and division by zero  