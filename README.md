Local Web Service for Dynamic Event Booking: A Practical, Application-Oriented Solution
This project aims to provide a local web service that facilitates dynamic, calendar-based event booking with limited capacity. The system is designed to address the need for organized registration for events where seat availability is limited, especially for organizations that lack automated reservation systems.
Background
Organizations such as Rosatom often face challenges in creating and managing reservation forms for events. Currently, these organizations must manually create a new form for each event, which can be time-consuming and inefficient. This service aims to offer a robust and reusable solution that can benefit a wide range of organizations.
Proposed Solution
The proposed system allows users to:
Create unique reservation forms for events with customizable dates, start times, and capacity via an administrative dashboard.
Automatic booking restrictions when capacity is reached.
Avoid overbooking with safety measures for simultaneous bookings in the same time slot.
Generate a user-friendly booking form for each event where participants can sign up by filling out custom fields defined by the organizer.
Allow administrators to view a list of registered attendees for each event and edit event details.
Key features:
- Administrative form creation: Define event name, description, upload related files, select multiple dates and start times, set available slots for each combination, add custom fields (e.g. participant name, email, phone).
- User booking process: Provide users with a unique link to the booking form, complete the booking by entering required information and selecting a slot.
Bookings are added to a registration list accessible to the event organizer.
Concurrency control:
- Built-in checks prevent overbooking by multiple users trying to reserve the same time slot.
