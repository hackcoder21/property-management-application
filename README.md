# Property Management Application
A Property Management app built with Angular and ASP.NET Core. Users can register, log in, add their property details, upload images, edit or delete them, and download a formatted Excel report generated using EPPlus. Data is stored in SQL Server with EF Core, secured using JWT authentication.

ER-Diagram
┌──────────────────────┐ ┌──────────────────────────┐
│ Users │ 1 ─── 1 │ Properties │
├──────────────────────┤ ───────────────▶├──────────────────────────┤
│ Id (PK) │ │ Id (PK) │
│ FullName │ │ UserId (FK → Users.Id) │
│ Email │ │ Title │
└──────────────────────┘ │ Price │
│ City │
│ State │
│ Locality │
│ Pincode │
│ NoOfRooms │
│ CarpetAreaSqft │
│ BuiltYear │
│ NoOfBathrooms │
│ Balcony │
│ Parking │
│ PropertyImageUrl │
│ HallImageUrl │
│ KitchenImageUrl │
│ BathroomImageUrl │
│ BedroomImageUrl │
│ ParkingImageUrl │
└──────────────────────────┘

Users Table
| Column Name      | Type                 | Nullable? | Notes                      |
| ---------------- | -------------------- | --------- | -------------------------- |
| Id               | INT                  | N         | Primary Key                |
| FullName         | NVARCHAR(150)        | N         | User's full name           |
| Email            | NVARCHAR(150)        | N         | Used's Email               |

Properties Table
| Column Name      | Type                 | Nullable? | Notes                      |
| ---------------- | -------------------- | --------- | -------------------------- |
| Id               | INT                  | N         | Primary Key                |
| UserId           | INT                  | N         | FK → Users.Id (1-to-1)     |
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
