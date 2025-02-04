 
import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useMsal } from '@azure/msal-react';

interface PrivateRouteProps {
    element: React.ComponentType<any>;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element: Component }) => {
    const { accounts } = useMsal();
    const isAuthenticated = accounts.length > 0;
    const location = useLocation();
    return isAuthenticated ? <Component /> : <Navigate to="/login" state={{ from: location }} />;
};

export default PrivateRoute;