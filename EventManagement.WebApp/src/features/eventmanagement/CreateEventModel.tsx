import React from 'react';
import './CreateEventModel.css'; // Import the CSS file for styling
import { Event } from '../../config/models';

interface CreateEventModalProps {
    isOpen: boolean;
    onClose: () => void;
    onSubmit: (e: React.FormEvent) => void;
    handleChange: (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void;
    event: Event;
}

export const CreateEventModal: React.FC<CreateEventModalProps> = ({ isOpen, onClose, onSubmit, handleChange, event }) => {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal">
                <h2>Create Event</h2>
                <form onSubmit={onSubmit}>
                    <label>
                        Name:
                        <input type="text" name="name" value={event.name} onChange={handleChange} required />
                    </label>
                    <label>
                        Description:
                        <textarea name="description" value={event.description} onChange={handleChange} required />
                    </label>
                    <label>
                        Location:
                        <input type="text" name="location" value={event.location} onChange={handleChange} required />
                    </label>
                    <label>
                        Start Time:
                        <input type="datetime-local" name="startTime" value={event.startTime.toISOString().slice(0, 16)} onChange={handleChange} required min={new Date().toISOString().slice(0, 16)} />
                    </label>
                    <label>
                        Available Tickets:
                        <input type="number" name="availableTickets" value={event.availableTickets} onChange={handleChange} required />
                    </label>
                    <div className="modal-buttons">
                        <button type="submit" className="submit-button">Submit</button>
                        <button type="button" className="cancel-button" onClick={onClose}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
};