import React from 'react';
import { useMsal } from '@azure/msal-react';
import { loginRequest } from '../config/authConfig';
import { InteractionStatus } from '@azure/msal-browser';
import './LoginPage.css';

export const LoginPage: React.FC = () => {
    const { instance, inProgress } = useMsal();

    const handleLogin = () => {
        if (inProgress === InteractionStatus.None) {
            console.log('Login Request:', loginRequest);
            console.log('Instance:', instance.getAllAccounts());
            instance.loginRedirect(loginRequest);
        }
    };

    return (
        <div className="page-container">
            <div className="card">
                <h2 className="page-title">Login</h2>
                <button className="primary-button" onClick={handleLogin}>Log In</button>
            </div>
        </div>
    );
};