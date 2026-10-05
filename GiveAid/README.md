# Give-AID NGO Website

Welcome to the **Give-AID** project! This is an ASP.NET Core MVC web application for a Non-Governmental Organization that helps various NGOs raise funds and coordinate welfare activities.

## 📋 Project Requirements
* **Admin Module:** Manage website content, NGOs, causes, programmes, queries, and view registered members.
* **User Module:** Register, login, donate to specific causes, and invite friends.

The complete original requirement document is included in this repository: `NGO-website.doc`.

## 🚀 Getting Started (For Team Members)

1. **Clone the repository:**
   ```bash
   git clone <YOUR_GITHUB_REPO_URL_HERE>
   ```
2. **Setup the Database:**
   Ensure you have SQL Server installed. Open the project in Visual Studio or VS Code and run:
   ```bash
   dotnet ef database update
   ```
3. **Run the Application:**
   ```bash
   dotnet run
   ```

## 📝 Team Tasks (Day 1 & 2)

* **Main Developer (Architecture):** 
  Backend setup, DB Models, and logic integration (Completed Day 1 DB scaffolding).
* **Member 1 (Content Writer):** 
  Write dummy content for the 7 "About Us" sub-pages (Mission, Team, Achievements, etc.).
* **Member 2 (Designer/Images):** 
  Download high-quality images for the Gallery and 5-6 dummy logos for "Our Partners".
* **Member 3 (Frontend/Payment UI):** 
  Create the HTML/CSS for the Credit/Debit Card Payment form (validations will be added later).
* **Member 4 (Documentation):** 
  Start creating the presentation and project report based on `NGO-website.doc`.

## 🛠 Tech Stack
* ASP.NET Core MVC (C#)
* Entity Framework Core
* SQL Server
* Bootstrap (Frontend)
