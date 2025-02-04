 
import React, { useEffect, useState } from 'react';
import { useLocation, useParams} from 'react-router-dom';
import { get } from '../common/httpClient';
import { Registration } from '../config/models';
import { Event } from '../config/models';
import { Banner } from '../features/app/Banner'; // Import the Banner component
import './ParticipantsPage.css'; // Import the CSS file
import { Url } from '../config/urls';

interface ParticipantsPageProps {
    event: Event;
}

export const ParticipantsPage: React.FC<ParticipantsPageProps> = () => {
    const { eventId } = useParams<{ eventId: string }>();
    const location = useLocation();
    
    const event = location.state?.event as Event;
    const [participants, setParticipants] = useState<Registration[]>([]);

    useEffect(() => {
        const fetchParticipants = async () => {
            try {
                const data = await get(`/events/${eventId}/registrations`);
                setParticipants(data);
            } catch (error) {
                console.error('Error fetching participants:', error);
            }
        };

        fetchParticipants();
    }, [eventId]);

    return (
        <div className="participants-page">
            <Banner title={`Participants for ${event?.name}`} buttonText="Admin Page" buttonLink={Url.EventManagement} />
            <div className="participants-cards">
                {participants.map(participant => (
                    <div className="participant-card">
                        <div className="participant-info">
                            <h3>{participant.name}</h3>
                            <p>Email: {participant.emailAddress}</p>
                            <p>Phone: {participant.phoneNumber}</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};