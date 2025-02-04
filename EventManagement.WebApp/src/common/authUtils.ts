import { PublicClientApplication, AccountInfo } from '@azure/msal-browser';
import { msalConfig, loginRequest } from '../config/authConfig';

const msalInstance = new PublicClientApplication(msalConfig);

export const acquireToken = async (account: AccountInfo) => {
    try {
        const response = await msalInstance.acquireTokenSilent({
            ...loginRequest,
            account,
        });
        return response.accessToken;
    } catch (error) {
        console.error('Error acquiring token silently:', error);
        throw error;
    }
};