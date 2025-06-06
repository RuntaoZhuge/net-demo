# MySQL Database Integration Demo for .NET Weather Forecast API

This guide walks you through connecting a .NET 8+ minimal API to a MySQL database, using Entity Framework Core, and performing CRUD operations with a `WeatherForecast` table.

---

## 1. Setting Up MySQL

### On Windows
1. **Download MySQL Installer:**
   - Visit the [MySQL Downloads page](https://dev.mysql.com/downloads/installer/) and download the MySQL Installer.
2. **Run the Installer:**
   - Follow the prompts to install MySQL Server and MySQL Workbench (optional).
3. **Configure MySQL:**
   - During installation, set a root password and ensure the MySQL service is set to start automatically.
4. **Verify Installation:**
   - Open Command Prompt and run:
     ```sh
     mysql -u root -p
     ```
   - Enter your root password to access the MySQL shell.

### On macOS
1. **Install Homebrew (if not installed):**
   - Open Terminal and run:
     ```sh
     /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
     ```
2. **Install MySQL:**
   - Run:
     ```sh
     brew install mysql
     ```
3. **Start MySQL Service:**
   - Run:
     ```sh
     brew services start mysql
     ```
4. **Set Root Password:**
   - Run:
     ```sh
     mysql_secure_installation
     ```
   - Follow the prompts to set a root password and secure your installation.
5. **Verify Installation:**
   - Open Terminal and run:
     ```sh
     mysql -u root -p
     ```
   - Enter your root password to access the MySQL shell.

---

## 2. Prerequisites
- .NET 8 SDK or newer
- MySQL server running locally
- MySQL root access (or a user with privileges to create databases and users)

---

## 3. Create the Database and User

Login to MySQL as root:
```sh
mysql -u root -p
```

Run the following SQL to create the database and a new user:
```sql
CREATE DATABASE IF NOT EXISTS MyWebService;
CREATE USER 'webapp_user'@'localhost' IDENTIFIED BY 'admin';
GRANT ALL PRIVILEGES ON MyWebService.* TO 'webapp_user'@'localhost';
FLUSH PRIVILEGES;
```

---

## 4. Add Required NuGet Packages

In your project directory, run:
```sh
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.13
```

---

## 5. Add and Apply Entity Framework Migrations

Generate the initial migration:
```sh
dotnet ef migrations add InitialCreate
```

Apply the migration to create the table:
```sh
dotnet ef database update
```

---

## 6. (Optional) Add Sample Data via SQL

You can insert fake data directly in MySQL:
```sql
INSERT INTO WeatherForecasts (Date, TemperatureC, Summary) VALUES
  (CURDATE(), 22, 'Mild'),
  (CURDATE() + INTERVAL 1 DAY, 28, 'Warm'),
  (CURDATE() + INTERVAL 2 DAY, 15, 'Chilly'),
  (CURDATE() + INTERVAL 3 DAY, 30, 'Hot'),
  (CURDATE() + INTERVAL 4 DAY, 10, 'Freezing'),
  (CURDATE() + INTERVAL 5 DAY, 18, 'Cool'),
  (CURDATE() + INTERVAL 6 DAY, 25, 'Balmy'),
  (CURDATE() + INTERVAL 7 DAY, 35, 'Sweltering'),
  (CURDATE() + INTERVAL 8 DAY, 40, 'Scorching'),
  (CURDATE() + INTERVAL 9 DAY, 12, 'Bracing');
```

---

## 7. Run the Application

From the project directory:
```sh
dotnet run
```

The API will be available at the port shown in the output (commonly `https://localhost:5001` or `http://localhost:5000`).

---

## 8. Test the API

- **Add a forecast:**
  ```sh
  curl -X POST http://localhost:5000/weatherforecast
  ```
- **Get all forecasts:**
  ```sh
  curl http://localhost:5000/weatherforecast
  ```
- **Get by date:**
  ```sh
  curl http://localhost:5000/weatherforecast/2024-06-07
  ```

---

## 9. Troubleshooting
- If you see `Table 'mywebservice.weatherforecasts' doesn't exist`, make sure you ran the migrations.
- If you see `Access denied for user`, check your MySQL user privileges and connection string.
- If the app doesn't start, ensure you are in the correct directory and use `dotnet run`.

---

## 10. References
- [Pomelo.EntityFrameworkCore.MySql Docs](https://pomelo.readthedocs.io/en/latest/)
- [EF Core Docs](https://learn.microsoft.com/en-us/ef/core/) 