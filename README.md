# **SpotMe - ASP.NET Core Web API**  
 *Live Tracking of Friends on a Real-Time Map*

## 📌 **Description**
**SpotMe** is a **RESTful API** built with **ASP.NET Core Web API** and **Entity Framework Core**.  
It provides the backend functionality for the SpotMe application, allowing users to:  

✅ Register and authenticate using **JWT-based authentication** with **bcrypt password hashing**  
✅ Send, accept, and reject **friend requests**  
✅ Track friends' locations in **real-time**  
✅ Search users by **username**  
✅ Manage friend lists and remove friends  


---

## ⚡ **Tech Stack**
- **Backend:** ASP.NET Core Web API (.NET 8.0)
- **Database:** PostgreSQL with Entity Framework Core
- **Authentication:** JWT (JSON Web Token) + **bcrypt** for password hashing
- **Security:** Encrypted passwords using **BCrypt.Net-Next**

---

## 🔒 **Security Features**
- Passwords are securely hashed using bcrypt
- JWT tokens for secure authentication
- CORS policy to prevent unauthorized access
- Environment variables for storing sensitive information

---

## 🛠 **Setup & Installation**
### **1️⃣ Clone the Repository**
```bash
git clone https://github.com/omaar414/SpotMe-ASPNetCoreAPI.git
cd SpotMe-ASPNetCoreAPI

2️⃣ Install Dependencies
dotnet restore

3️⃣ Set Up Environment Variables
Create an appsettings.Development.json file in the root of your project and add:

json
Copy
Edit
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=spotme_db;Username=your_username;Password=your_password"
  },
  "JwtSettings": {
    "Secret": "your_secret_key",
    "Issuer": "SpotMeAPI",
    "Audience": "SpotMeAppUsers",
    "ExpirationInMinutes": 60
  },
  "AllowedHosts": "*"
}
📌 Make sure to replace your_username, your_password, and your_secret_key with actual values.

4️⃣ Apply Database Migrations
dotnet ef migrations add init
dotnet ef database update

5️⃣ Run the API Locally
dotnet watch run

📌 The API will be available at http://localhost:5281/api.

🧪 API Endpoints
📌 Base URL: http://localhost:5281/api

🔐 Authentication
POST /user/register – Register a new user
POST /user/login – Authenticate and receive JWT
👥 Friends Management
GET /friendship/friends – Get the list of friends
POST /friendship/send-request – Send a friend request
POST /friendship/accept-request/{id} – Accept a friend request
POST /friendship/deny-request/{id} – Deny a friend request
DELETE /friendship/delete-friend/{username} – Remove a friend
📍 Live Tracking
POST /map/location/update – Update user’s location
GET /map/location/friends – Get friends' live locations
🔍 Search
GET /friendship/search-user/{username} – Search for users

