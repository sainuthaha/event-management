import React from 'react';
import { Event } from '../../config/models';
import './EventCard.css'; // Import the CSS file for styling

interface EventCardProps {
    event: Event;
    buttonText: string;
    onButtonClick: (event: Event) => void;
}

export const EventCard: React.FC<EventCardProps> = ({ event, buttonText, onButtonClick }) => {
    const isSoldOut = event.availableTickets === 0;

    return (
        <div className="event-card">
            <div className="event-card-header">
                <h2>{event.name}</h2>
            </div>
            <hr className="event-card-divider" />
            <div className="event-card-content">
                <p className="event-card-description">{event.description}</p>
                <p className="event-card-location">📍 {event.location}</p>
                <p className="event-card-date">📅 {new Date(event.startTime).toLocaleDateString()}</p>
                <button
                    className="register-button"
                    onClick={() => onButtonClick(event)}
                    disabled={buttonText === 'Register' && isSoldOut}
                >
                    {buttonText === 'Register' && isSoldOut ? 'No tickets available' : buttonText}
                </button>
            </div>
        </div>
    );
};