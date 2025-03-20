Event Management System - Frontend

This project is the frontend of an Event Management System built using React.js. It provides a user-friendly interface for managing events, RSVPs, attendees, and sending notifications. The frontend interacts with a .NET 8.0 backend using RESTful APIs.

Features

1. Event Creation

Admin can create new events by providing details such as name, description, and date.

The event creation page is restricted to admin users.

2. Event List (View & Delete)

All users can view a list of available events in a tabular format.

Admin can delete events from the list.

3. RSVP to Events

Logged-in users can RSVP to events.

4. Attendees Management

Admin can view the list of attendees for each event.

5. Notifications

Admin can send a reminder notification to attendees about an upcoming event.

Users receive a notification when they log in, reminding them of events they RSVP'd to.

6. Role-Based Access

Admin-exclusive routes: Event Creation and Attendee Management.

General users: Can view events and RSVP.

Installation and Setup

1. Clone the Repository

2. Install Dependencies

3. Run the Frontend

Backend Setup (.NET 8.0)

The backend is built using .NET 8.0 and contains controllers, models, and migrations. Follow these steps to set up the backend:

1. Configure App Settings

Replace the following values in appsettings.json:

JWT

Audience

Issuer

2. Run Migrations

3. Start the Backend Server

API Endpoints (Backend)

Authentication (AuthController)

POST /api/auth/login - User login

POST /api/auth/register - User registration

Event Management (EventController)

GET /api/events - Get all events

POST /api/events - Create a new event (Admin only)

DELETE /api/events/{id} - Delete an event (Admin only)

RSVP Management (RSVPController)

POST /api/rsvp - RSVP to an event

GET /api/rsvp/{eventId} - Get attendees of an event (Admin only)

Technologies Used

Frontend: React.js

Backend: .NET 8.0, ASP.NET Core

Database: SQL Server

Authentication: JWT

State Management: Context API

Contributing

Feel free to fork the repository and submit pull requests with improvements!

License

This project is licensed under the MIT License.

Contact

For any issues or questions, reach out to Mukesh via GitHub or email.
