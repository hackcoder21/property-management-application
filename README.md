# Property Management Application

A full-stack **Property Management Application** built using **Angular** and **ASP.NET Core Web API**, designed to manage property portfolios and generate detailed Excel & PDF reports.

🔗 **Live API (Swagger):**  
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
- HTML, CSS, TypeScript

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
- Render (Linux container)  
  https://render.com/
- Cloudinary (Files storage)  
  https://cloudinary.com/

---

## 🗂 ER Diagram

Users ------ (1 - M) ------ Properties

---

## 🧑 Users Table

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

## 🔐 Authentication Flow

1. User registers using **Email + Password**
2. ASP.NET Identity creates the user
3. On login, a **JWT token** is generated
4. JWT token is required for all secured APIs

---

## 🔄 Application Flow

1. **Application User registration**
- Creates a user in `AspNetUsers`
- Assigns default role: `Writer`

2. **Create users & properties**
- Within application, create user with name & email
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
