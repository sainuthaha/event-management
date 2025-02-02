import { Route, Routes } from 'react-router-dom';
import './App.css';
import { HomePage } from './pages/HomePage';
import { Url } from './config/urls';
import { EventManagement } from './pages/EventManagement';
import { ParticipantsPage } from './pages/ParticipantPage';

export default function App() {
    return (
        <>
            <Routes>
                <Route path={Url.Home} element={<HomePage />} />
                <Route path={Url.EventManagement} element={<EventManagement />} />
                <Route path="/events/:eventId/registrations" element={<ParticipantsPage event={null} />} />
            </Routes>
        </>
    );
}