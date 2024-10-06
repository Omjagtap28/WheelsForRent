# Car Rental Web Application

## Overview
This is a full-stack Car Rental Web Application built as an academic project. It allows users to search, rent, and manage car bookings, while providing administrators with a panel for managing cars, bookings, and users. The project demonstrates expertise in both frontend and backend technologies, ensuring a smooth and interactive user experience.

## Features
- **User Features**:  
  - Search for available cars.
  - Book cars for rental.
  - Manage bookings (view or cancel).
  
- **Admin Features**:  
  - Add, update, and delete car listings.
  - Manage user accounts.
  - Manage bookings.

## Technologies Used
### Frontend
- HTML
- CSS
- JavaScript

### Backend
- C# (.NET Framework)
- MSSQL Server

### Tools
- **Visual Studio Code**: Used for frontend coding and debugging.
- **Visual Studio 2022**: Used for backend development.
- **SQL Server Management Studio**: Used for database management.

### Important Note
This Car Rental Web Application is optimized for use on desktop and laptop devices. While it provides a functional experience on mobile devices, the layout and design elements are best viewed on larger screens. We recommend accessing the application through a laptop or desktop for the best user experience.

## Installation
To set up the project locally:

1. Clone the repository:
    ```bash
    git clone https://github.com/Omjagtap28/WheelsForRent.git
    ```

2. Open the project in **Visual Studio 2022**.

3. Update the connection string in the `web.config` file to point to your local or remote database:
    ```xml
    <connectionStrings>
        <add name="WheelsForRentConnectionString" connectionString="your_connection_string_here" />
    </connectionStrings>
    ```

4. Restore the necessary packages:
    - In **Visual Studio**, go to `Tools` -> `NuGet Package Manager` -> `Manage NuGet Packages for Solution`, and restore the required dependencies.

5. Build and run the project using **Visual Studio**.

## Database Setup
1. Use **SQL Server Management Studio** to create the database for the project.
2. Run the provided SQL scripts in the `DatabaseScripts` folder (if available) to set up tables, stored procedures, and initial data.

## How to Use
1. **User**: 
   - Navigate to the homepage.
   - Browse available cars, choose a rental period, and book a car.
   - View and manage your bookings under the "My Rides" section.
   
2. **Admin**:
   - Log in as an administrator.
   - Add new car listings, view all bookings,Feedbacks,contact history and manage user accounts via the admin panel.

## Future Enhancements
- Improve UI responsiveness for mobile devices.
- Add additional features like payment gateway integration.
- Implement a notification system for bookings.

## Contact
For any queries or issues, please contact:
- **Your Name**  
- Email: jagtapom39@gmail.com
- GitHub: [Omjagtap28](https://github.com/Omjagtap28)
