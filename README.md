# RentACar API

RentACar API is a multi-layered ASP.NET Core Web API project designed for an online rent-a-car platform. The application supports essential functionalities like user authentication, car rental management, and payment processing, all structured with a focus on maintainability and scalability. The project follows a layered architecture, leveraging Entity Framework Code First for database interactions and includes secure practices like JWT authentication and password encryption.

## Project Structure

The project is organized into three primary layers:

- **Presentation Layer (API Layer):** Contains controllers for handling HTTP requests.
- **Business Layer:** Includes business logic and service layer.
- **Data Access Layer:** Manages database operations through repositories and Unit of Work pattern using Entity Framework.

## Technologies Used

- **ASP.NET Core Web API**
- **Entity Framework Core (Code First)**
- **JWT (JSON Web Token) for Authentication**
- **ASP.NET Core Identity**
- **Middleware**
- **Action Filters**
- **Dependency Injection**
- **Data Protection**

## Features

- **Authentication & Authorization:** Uses JWT tokens to secure API endpoints, with roles for "Admin" and "Customer" to manage access control.
- **CRUD Operations:** Full support for Create, Read, Update, Patch, and Delete operations on core entities (Cars, Reservations, Payments, and Users).
- **Middleware:** Custom middleware for logging request details and maintenance mode.
- **Action Filter:** Restrict API access to specific timeframes as needed.
- **Model Validation:** Ensures data integrity with validation rules on User and Car models.
- **Global Exception Handling:** Catches and handles exceptions globally to provide consistent responses.
- **Data Protection:** Password encryption and secure data handling.

## Entities and Relationships

- **User:** Stores customer details, including email and encrypted password.
- **Car:** Information on rentable cars.
- **Feature:** Describes additional features for cars.
- **Reservation:** Tracks car rental reservations, linking cars and users.
- **Payment:** Stores payment details, linking one-to-one with reservations.
- **CarFeature:** Defines a many-to-many relationship between cars and features.

## Getting Started

### Prerequisites

- .NET SDK (6.0 or later)
- SQL Server (or another compatible database)
- Visual Studio or another preferred IDE

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/zeynep-png/RentACar.git
   ```
2. Navigate to the project directory:
   ```bash
   cd rentacar-api
   ```
3. Set up the database by running migrations:
   ```bash
   dotnet ef database update
   ```

### Running the Project

1. In the root directory, start the API:
   ```bash
   dotnet run
   ```
2. Access the API documentation (if Swagger is configured) at `http://localhost:{port}/swagger`.

## API Endpoints

- **User Authentication**  
  - Register: `POST /api/auth/register`
  - Login: `POST /api/auth/login`

- **Cars**  
  - List all cars: `GET /api/cars`
  - Create car: `POST /api/cars`
  - Update car: `PUT /api/cars/{id}`
  - Delete car: `DELETE /api/cars/{id}`

- **Reservations**  
  - Create reservation: `POST /api/reservations`
  - View reservation: `GET /api/reservations/{id}`

- **Payments**  
  - Record payment: `POST /api/payments`

## Additional Information

### Middleware and Filters

- **Request Logging Middleware:** Logs request information for every API call.
- **Maintenance Mode Middleware:** Toggles API availability for maintenance.
- **Time-Based Access Filter:** Restricts access to selected APIs based on the specified schedule.

### Security

- **Data Protection:** User passwords are encrypted and stored securely.
- **Role-Based Access Control:** Ensures appropriate access levels for Admins and Customers.
