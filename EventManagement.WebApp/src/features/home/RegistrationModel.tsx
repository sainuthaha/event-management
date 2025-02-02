import React from 'react';
import './RegistrationModel.css'; // Import the CSS file for styling
import { Event } from '../../config/models';
import { Registration } from '../../config/models';

interface RegistrationModalProps {
    isOpen: boolean;
    event: Event | null;
    registration: Registration;
    onClose: () => void;
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
    onSubmit: (e: React.FormEvent) => void; // Add onSubmit prop
}

export const RegistrationModal: React.FC<RegistrationModalProps> = ({
    isOpen,
    event,
    registration,
    onClose,
    onChange,
    onSubmit,
}) => {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal">
                <h2>Register for {event?.name}</h2>
                <form onSubmit={onSubmit}>
                    <label>
                        Name:
                        <input type="text" name="name" value={registration.name} onChange={onChange} required />
                    </label>
                    <label>
                        Email:
                        <input type="email" name="emailAddress" value={registration.emailAddress} onChange={onChange} required />
                    </label>
                    <label>
                        Phone:
                        <input type="tel" name="phoneNumber" value={registration.phoneNumber} onChange={onChange} required />
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