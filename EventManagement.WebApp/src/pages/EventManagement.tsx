 
import React, { useState, useEffect,  } from 'react';
import { Url } from '../config/urls';
import { Banner } from '../features/app/Banner';
import { Event } from '../config/models';
import { get, post } from '../common/httpClient';
import { useNavigate } from 'react-router-dom';
import './EventManagement.css'; // Import the CSS file for styling
import { CreateEventModal } from '../features/eventmanagement/CreateEventModel';
import { EventCard } from '../features/home/Eventcard';

export const EventManagement: React.FC = () => {
    const navigate = useNavigate();
    const [, setSelectedEvent] = useState<Event | null>(null);
    const [events, setEvents] = useState<Event[]>([]);
    const [isCreateEventModalOpen, setIsCreateEventModalOpen] = useState(false);
    const [newEvent, setNewEvent] = useState<Event>(
        new Event({
            name: '',
            description: '',
            location: '',
            availableTickets: 0,
            startTime: new Date(),
            createdBy: sessionStorage.getItem('userEmail') || ''
        })
    );

    const fetchEvents = async () => {
        try {
            const email = sessionStorage.getItem('userEmail');
            const data = await get(`/events/created/${email}`); 
            const eventObjects = data.map((eventData: Event) => Event.fromJS(eventData));
            setEvents(eventObjects);
        } catch (error) {
            console.error('Error fetching events:', error);
        }
    };

    useEffect(() => {
        fetchEvents();
    }, []);

    
    const handleViewParticipants = (event: Event) => {
        const token = sessionStorage.getItem('msalToken');
        if (!token) {
            console.log('Token expired or not found, redirecting to login...');
            navigate('/login');
            return;
        }
        
        setSelectedEvent(event);
        console.log('View participants for event:', event);
        navigate(`/events/${event.id}/registrations`, { state: { event } });
    };


    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setNewEvent((prev) => {
            const updatedEvent = new Event();
            Object.assign(updatedEvent, prev, { [name]: name === 'startTime' ? new Date(value) : name === 'availableTickets' ? parseInt(value) : value });
            return updatedEvent;
        });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!newEvent.name) {
            alert('Please fill in the event name');
            return;
        }
        if (!newEvent.description) {
            alert('Please fill in the event description');
            return;
        }
        if (!newEvent.location) {
            alert('Please fill in the event location');
            return;
        }
        if (!newEvent.startTime) {
            alert('Please fill in the event start time');
            return;
        }
        if (!newEvent.availableTickets) {
            alert('Please fill in the number of available tickets');
            return;
        }
        try {
            const token = sessionStorage.getItem('msalToken');

            const response = await post('/events', { arg: newEvent }, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            console.log('Event creation successful:', response.data);
            setEvents((prevEvents) => [...prevEvents]);
            await fetchEvents();
            setIsCreateEventModalOpen(false);
        } catch (error) {
            console.error('Error creating event:', error);
        }
    };

    return (
        <div>
            <Banner title="Event Management Page" buttonText="Homepage" buttonLink={Url.Home} />
            <div className="event-management-cards">
                <div className="card create-event-card" onClick={() => setIsCreateEventModalOpen(true)}>
                    <h2>+ Create Event</h2>
                </div>
                {events.map(event => (
                    <EventCard
                        key={event?.id}
                        event={event}
                        buttonText="View Participants"
                        onButtonClick={handleViewParticipants}
                    />
                ))}
            </div>
            <CreateEventModal
                isOpen={isCreateEventModalOpen}
                onClose={() => setIsCreateEventModalOpen(false)}
                onSubmit={handleSubmit}
                handleChange={handleInputChange}
                event={newEvent}
            />
        </div>
    );
};