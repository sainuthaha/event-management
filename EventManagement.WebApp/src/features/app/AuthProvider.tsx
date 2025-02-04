import React, { useEffect, useState } from 'react';
import { PublicClientApplication } from '@azure/msal-browser';
import { MsalProvider } from '@azure/msal-react';
import { msalConfig } from '../../config/authConfig';
import { useNavigate } from 'react-router-dom';
import { Url } from '../../config/urls';

export const msalInstance = new PublicClientApplication(msalConfig);

const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [isInitialized, setIsInitialized] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const initializeMsal = async () => {
            try {
                console.log("Initializing MSAL...");
                await msalInstance.initialize(); // ✅ Ensure MSAL is ready
                
                console.log("MSAL Initialized. Handling redirect promise...");
                const response = await msalInstance.handleRedirectPromise();

                console.log("Redirect response:", response);
                
                if (response && response.account) {
                    msalInstance.setActiveAccount(response.account);
                    sessionStorage.setItem('msalToken', response.accessToken); // Store token in session storage
                    sessionStorage.setItem('userEmail', response.account.username); // Store user's email in session storage
                    if ( window.location.pathname === '/login') {
                        navigate(Url.EventManagement);}
                } else {
                    const currentAccounts = msalInstance.getAllAccounts();
                    if (currentAccounts.length > 0) {
                        msalInstance.setActiveAccount(currentAccounts[0]);
                    }
                }

                setIsInitialized(true);
            } catch (error) {
                console.error("Error initializing MSAL:", error);
            }
        };

        initializeMsal();
    }, [navigate]);

    // 🛠 Debugging: Check if MSAL is correctly initialized
    useEffect(() => {
        console.log("MSAL Instance:", msalInstance);
        console.log("Current accounts:", msalInstance.getAllAccounts());
    }, [isInitialized]);

    if (!isInitialized) {
        return <div>Loading authentication...</div>; // Prevent usage before MSAL is ready
    }

    return <MsalProvider instance={msalInstance}>{children}</MsalProvider>;
};

export default AuthProvider;
