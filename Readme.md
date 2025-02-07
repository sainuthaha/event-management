# Event Managemet

- The project comprises of an Event Management API and an Event Management WebApp.

## Event Management API

- The Event Management API is a RESTful web service for managing events and registrations.
- Provides endpoints for creating, updating, retrieving, and deleting events.
- Manages event registrations.
- Stores data to Azure sql tables.

### Features

- Event creators can create events and view registrations for an event, once they login.
- Users can view all available events and register for the same.
- JWT-based authentication and authorization.
- CORS support.
- OpenAPI (Swagger) documentation.

![alt text](<Screenshot 2025-02-04 at 15.46.55.png>)

## Event Management WebApp

- The Event Management WebApp is a user-friendly interface for interacting with the Event Management API.
- Allows event creators to easily manage events.
- Users can manage registrations through a web browser.

### Features

- User registration and login.
- Browse and search for events.
- View event details.
- Register for events.
- Manage user profile and registrations.

![alt text](image.png)

# Authorization & Authentication

![alt text](<Screenshot 2025-02-04 at 16.01.36.png>)

### Authentication & Authorization Flow

- User logs in to the React app using Microsoft OAuth (Azure AD).
- The user is redirected to the Azure AD login page.
- Azure AD authenticates the user and returns a JWT access token.
- After successful authentication, Azure AD redirects the user back to the React app with an access token that includes the necessary scope to access the backend API.
- React app stores the token and uses it for API calls.
- The access token is stored in the React app
- React app sends API requests to the backend, including the JWT token in the Authorization header.
- The access token is included in the Authorization header of each API request.
- Backend API validates the token using Azure AD and processes the request if valid.
- The backend API validates the token using the Azure AD configuration specified in Program.cs.
- Backend responds with requested data.
- If the token is valid, the backend API processes the request and responds with the requested data.

## EventManagement.Tests

 * This test project is designed to validate the functionality and performance of the main application.
 * It includes a series of unit tests, to ensure that all components
 * of the application work as expected. The tests are written using a testing framework such as MsTest, and they cover various scenarios including edge cases and error conditions.
 

## Interaction Flow Diagram

```mermaid
graph TD
    A[User] -->|Login| B[React Web App]
    B -->|Redirect to| C[Azure AD]
    C -->|Authenticate| B
    B -->|JWT Token| D[Event Management API]
    D -->|Validate Token| C
    D -->|Access Data| E[SQL Database]
    E -->|Return Data| D
    D -->|Response| B
    B -->|Display Data| A
```

### Explanation

- **User**: Initiates the process by logging into the React Web App.
- **React Web App**: Redirects the user to Azure AD for authentication.
- **Azure AD**: Authenticates the user and returns a JWT token to the React Web App.
- **Event Management API**: Receives the JWT token from the React Web App, validates it with Azure AD, and accesses the SQL Database to retrieve or store data.
- **SQL Database**: Stores and returns the requested data to the Event Management API.
- **React Web App**: Displays the data to the user.
