import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Url } from './config/urls';
import { HomePage } from './pages/HomePage';
import { EventManagement } from './pages/EventManagement';
import { ParticipantsPage } from './pages/ParticipantPage';
import { LoginPage } from './pages/LoginPage';
import AuthProvider from './features/app/AuthProvider';
import PrivateRoute from './features/app/PrivateRoute';

const App: React.FC = () => {
    return (
       
            <Router>
                 <AuthProvider>
                <Routes>
                    <Route path={Url.Home} element={<HomePage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/events/:eventId/registrations" element={<PrivateRoute element={ParticipantsPage} />} /> {/* Add the ParticipantPage route */}
                    <Route path={Url.EventManagement} element={<PrivateRoute element={EventManagement} />} />
                </Routes>
                </AuthProvider>
            </Router>
     
    );
};

export default App;