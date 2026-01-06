# Property Management Application

A full-stack **Property Management Application** built using **Angular** and **ASP.NET Core Web API**, designed to manage property portfolios and generate detailed Excel & PDF reports.

🌐 **Live Application URL:** 
https://property-management-application.netlify.app/

🌐 **Live API (Swagger):**  
https://property-management-api-6j5v.onrender.com/swagger/index.html

---

## ✨ Features Overview
- User Registration & Login (JWT Authentication)
- Create, Update, Delete Users & Properties
- Secure APIs using Role-based Authorization
- Generate Excel & PDF reports
- SQL Server integration using EF Core
- Stored-procedure-based reporting
- Dockerized and deployed on Render (Linux)

---

## 🛠 Tech Stack

### Frontend
- Angular
- HTML, CSS, TypeScript, Bootstrap

### Backend
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- ASP.NET Identity
- JWT Authentication
- EPPlus - Excel report generation
  https://www.epplussoftware.com/
- Syncfusion - Excel to PDF conversion
  https://www.syncfusion.com/
- SkiaSharp - Load images to Excel
  https://skiasharp.com/

### Database
- MS SQL Server (Somee)  
  https://somee.com/

### Cloud & Deployment
- Netlify (Angular Application)
  https://www.netlify.com/
- Render (Linux container)  
  https://render.com/
- Cloudinary (Files storage)  
  https://cloudinary.com/

---

## 🗂 ER Diagram

Users ------ (1 - M) ------ Properties

---

## 👥 Users Table

| Column Name      | Type                 | Nullable? | Notes                      |
| ---------------- | -------------------- | --------- | -------------------------- |
| Id               | GUID                 | N         | Primary Key                |
| FullName         | NVARCHAR(150)        | N         | User's full name           |
| Email            | NVARCHAR(150)        | N         | Used's Email               |

---

## 🏠 Properties Table

| Column Name      | Type                 | Nullable? | Notes                      |
| ---------------- | -------------------- | --------- | -------------------------- |
| Id               | GUID                 | N         | Primary Key                |
| UserId           | GUID                 | N         | FK → Users.Id (1-to-1)     |
| Title            | NVARCHAR(500)        | N         | Property title             |
| Price            | DECIMAL(18,2)        | N         | Property selling price     |
| City             | NVARCHAR(100)        | N         | City                       |
| State            | NVARCHAR(100)        | N         | State                      |
| Locality         | NVARCHAR(150)        | N         | Locality                   |
| Pincode          | NVARCHAR(6)          | N         | Area pincode               |
| NoOfRooms        | INT                  | N         | No. of rooms               |
| CarpetAreaSqft   | FLOAT                | N         | Carpet area in sq. feet    |
| BuiltYear        | INT                  | N         | Built year                 |
| Balcony          | BIT                  | N         | Yes / No                   |
| Parking          | BIT                  | N         | Yes / No                   |
| PropertyImageUrl | NVARCHAR(260)        | Y         | Property main image        |
| HallImageUrl     | NVARCHAR(260)        | Y         | Hall / Living room image   |
| KitchenImageUrl  | NVARCHAR(260)        | Y         | Kitchen image              |
| BathroomImageUrl | NVARCHAR(260)        | Y         | Bathroom image             |
| BedroomImageUrl  | NVARCHAR(260)        | Y         | Bedroom image              |
| ParkingImageUrl  | NVARCHAR(260)        | Y         | Parking image              |

---

## 🔄 Application Flow

1. **Application User registration**
- Creates a user in `AspNetUsers`
- Assigns default role: `Writer`
- Login
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/45bcbd30-2403-4f94-933b-186e7c03a3a4" />
- Register
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/248a6fa9-caaf-4432-9294-568d67dfb09f" />

2. **Create users & properties**
- Within application, create user with name & email
- Create user
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/64b1b7e8-c67c-42d7-9c8d-93ae418e9651" />

- User dashboard with list of users created
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/fbaf7851-7536-4cec-8e1f-c7171cdf5126" />
- <img width="540" height="870" alt="image" src="https://github.com/user-attachments/assets/68edf568-56d9-4092-b0c6-5363c51299fd" />
- We can create new user, delete existing user, view properties for that user and generate detailed Property Portfolio Report in Excel/PDF format
- Before we can generate report, user must have properties

- Property list for particular user
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/f820f123-db5b-4dba-b351-c7c5b678d822" />
- <img width="540" height="870" alt="image" src="https://github.com/user-attachments/assets/dbb0facb-8f3f-49c1-b555-dadc28f29359" />

- Create/Update property
- User can create/update property along with property images
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/fe389ecb-93ef-4550-9aa7-9f8bbc7c6baa" />

- View property info
- <img width="1920" height="1000" alt="image" src="https://github.com/user-attachments/assets/5dbe15f2-732f-42b4-a043-653090328e8a" />

- Generate report for user
- <img width="1185" height="777" alt="image" src="https://github.com/user-attachments/assets/f6a25501-db11-43c1-9752-1b1aa951531a" />
- <img width="1179" height="770" alt="image" src="https://github.com/user-attachments/assets/741703b6-7b73-4bc0-b8e4-57dbd293d524" />

- Add multiple properties under a user
- Generate detailed report in Excel/PDF which highlights the user's portfolio & property details

---

## 🔐 Environment Configuration

All sensitive values are managed securely using **Environment Variables**:

- JWT Secret Key
- Database Connection Strings
- Cloudinary API Keys

---

## 👤 Author

**Rajat Jadhav**  
Software Developer
