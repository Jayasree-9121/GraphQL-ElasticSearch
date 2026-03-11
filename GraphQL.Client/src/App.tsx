import { useState } from "react";
import './App.css'
import { Dashboard } from './Components/Dashboard/Dashboard'
import { Header } from './Components/Header/Header'
import { Sidebar } from './Components/Sidebar/Sidebar'
import { Transaction } from './Components/Transaction/Transaction'
import UserList from './UserList'
import { Routes, Route, useNavigate } from 'react-router-dom';
import type { Application } from "./Interface/Application";

function App() {
    const [selectedApplication, setSelectedApplication] = useState<Application | null>(null);
    const navigate = useNavigate();

    const handleSelectApplication = (app: Application | null) => {
        setSelectedApplication(app);
        if (app) {
            navigate('/transaction');
        }
    };

    const handleDashboardClick = () => {
        setSelectedApplication(null);
        navigate('/');
    };

    return (
        <>
            {/* <UserList /> */}
            <Header applicationName={selectedApplication?.name} />
            <div className='d-flex dashboard-body'>
                <Sidebar onDashboardClick={handleDashboardClick} />

                {/* Routes */}
                <Routes>
                    <Route path="/" element={<Dashboard onSelectApplication={handleSelectApplication} />} />
                    <Route path="/transaction" element={<Transaction applicationName={selectedApplication?.name} />} />
                </Routes>
            </div>
        </>
    )
}

export default App
