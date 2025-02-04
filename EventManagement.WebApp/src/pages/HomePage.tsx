import React, { useEffect, useState } from 'react';
import { Event, Registration } from '../config/models'; // Adjust the path as necessary
import { getEvents, post } from '../common/httpClient';
import './HomePage.css'; // Import the CSS file for styling
import { RegistrationModal } from '../features/home/RegistrationModel';
import { EventCard } from '../features/home/Eventcard';
import { Banner } from '../features/app/Banner';
import { Url } from '../config/urls';

export const HomePage: React.FC = () => {
    const [events, setEvents] = useState<Event[]>([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
    const [registration, setRegistration] = useState<Registration>(new Registration());

    useEffect(() => {
        const fetchEvents = async () => {
            try {
                const data = await getEvents();
                const eventObjects = data.map((eventData: Event) => Event.fromJS(eventData));
                setEvents(eventObjects);
            } catch (error) {
                console.error('Error fetching events:', error);
            }
        };

        fetchEvents();
    }, []);

    const handleRegisterClick = (event: Event) => {
        setSelectedEvent(event);
        setRegistration((prev) => {
            const updatedRegistration = new Registration();
            Object.assign(updatedRegistration, prev, { eventId: event.id });
            return updatedRegistration;
        }); // Assign event ID to registration object
        setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
        setSelectedEvent(null);
        setRegistration(new Registration());
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setRegistration((prev) => {
            const updatedRegistration = new Registration();
            Object.assign(updatedRegistration, prev, { [name]: value });
            return updatedRegistration;
        });
    };
    
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!registration.name || !registration.emailAddress || !registration.phoneNumber) {
            alert('Please fill in all fields');
            return;
        }
        try {
            const response = await post('/registrations', { arg: registration });
            console.log('Registration successful:', response.data);
            handleCloseModal();
        } catch (error) {
            console.error('Error submitting registration:', error);
        }
    };
    return (
        <>
        <Banner title="Event Hub" buttonText="Admin Login" buttonLink={Url.EventManagement} />
        <div className="home-page">
            <div className="event-cards">
                {events.map((event: Event) => (
                    <EventCard key={event.id} event={event} buttonText='Register' onButtonClick={handleRegisterClick} />
                ))}
            </div>

            <RegistrationModal
                isOpen={isModalOpen}
                event={selectedEvent}
                registration={registration}
                onClose={handleCloseModal}
                onChange={handleInputChange}
                onSubmit={handleSubmit} 
            />
        </div>
        </>
    );
};