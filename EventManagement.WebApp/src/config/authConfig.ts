import { Configuration, LogLevel } from '@azure/msal-browser';
import { ENVIRONMENT } from './env';

export const msalConfig: Configuration = {
    auth: {
        clientId: ENVIRONMENT.CLIENT_ID!,
        authority: `https://login.microsoftonline.com/${ENVIRONMENT.TENANT_ID}`,
        redirectUri: 'http://localhost:5173/event-management', // Ensure this matches the redirect URI configured in Azure AD
    },
    cache: {
        cacheLocation: 'localStorage',
        storeAuthStateInCookie: false,
    },
    system: {
        loggerOptions: {
            loggerCallback: (level, message, containsPii) => {
                if (containsPii) {
                    return;
                }
                switch (level) {
                    case LogLevel.Error:
                        console.error(message);
                        break;
                    case LogLevel.Info:
                        console.info(message);
                        break;
                    case LogLevel.Verbose:
                        console.debug(message);
                        break;
                    case LogLevel.Warning:
                        console.warn(message);
                        break;
                }
            },
        },
    },
};

export const loginRequest = {
    scopes: ["api://26df75cb-7649-4d76-84d5-cda71f6fa93e/access_as_user"], // Correct scope
  };
