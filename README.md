# 🎉 Event Management System - Frontend

This project is the frontend of an Event Management System built using React.js. It provides a user-friendly interface for managing events, RSVPs, attendees, and sending notifications. The frontend interacts with a .NET 8.0 backend using RESTful APIs.

## 🚀 Features

### ✅ 1. Event Creation
- **Admin** can create new events by providing details such as name, description, and date
- The event creation page is restricted to admin users

### ✅ 2. Event List (View & Delete)
- All users can view a list of available events in a tabular format
- **Admin** can delete events from the list

### ✅ 3. RSVP to Events
- Logged-in users can RSVP to events

### ✅ 4. Attendees Management
- **Admin** can view the list of attendees for each event

### ✅ 5. Notifications
- **Admin** can send reminder notifications to attendees about upcoming events
- Users receive notifications when they log in, reminding them of events they RSVP'd to

### ✅ 6. Role-Based Access
- **Admin-exclusive routes**: Event Creation and Attendee Management
- **General users**: Can view events and RSVP

## 🛠 Installation and Setup

### 🔹 1. Clone the Repository
```bash
git clone https://github.com/your-username/event-management-system.git
🔹 2. Install Dependencies
bash
Copy
cd frontend
npm install
🔹 3. Run the Frontend
bash
Copy
npm start
🏗 Backend Setup (.NET 8.0)
The backend is built using .NET 8.0 and contains controllers, models, and migrations.

🔹 1. Configure App Settings
Replace the following values in appsettings.json:

json
Copy
"JWT": {
  "Audience": "your-audience",
  "Issuer": "your-issuer",
  "Key": "your-secret-key"
}
🔹 2. Run Migrations
bash
Copy
dotnet ef database update
🔹 3. Start the Backend Server
bash
Copy
dotnet run
📌 API Endpoints (Backend)
🔑 Authentication (AuthController)
POST /api/auth/login - User login

POST /api/auth/register - User registration

🎟 Event Management (EventController)
GET /api/events - Get all events

POST /api/events - Create new event (Admin only)

DELETE /api/events/{id} - Delete event (Admin only)

📝 RSVP Management (RSVPController)
POST /api/rsvp - RSVP to an event

GET /api/rsvp/{eventId} - Get attendees of an event (Admin only)

💻 Technologies Used
Frontend: React.js

Backend: .NET 8.0, ASP.NET Core

Database: SQL Server

Authentication: JWT

State Management: Context API

🤝 Contributing
Feel free to fork the repository and submit pull requests with improvements! 🚀

📜 License
This project is licensed under the MIT License.

📩 Contact
For any issues or questions, reach out to Mukesh via:

GitHub: @mukesh-843

Email: gmukesh843@gmail.com
