# Government Portal – ASP.NET Core Starter

This is a foundational project for a government licensing portal.

## Features (Not Implemented Yet)
- Structured logging with user ID, transaction ID, and timestamp
- Configurable log levels (Information, Warning, Error)
- Audit logging for sensitive operations (e.g., citizen record updates)

> ⚠️ **None of the above features are implemented.** This project is intentionally minimal to serve as a clean base for your tasks.

## Getting Started
1. Clone this repo
2. Run \`dotnet run\` in the \`src/GovernmentPortal\` folder
3. Use the API endpoints to simulate user actions

Endpoints:
- \`POST /api/account/login\`
- \`POST /api/licensing\`
- \`PUT /api/licensing/{id}\`
- \`GET /api/licensing/{id}\`
