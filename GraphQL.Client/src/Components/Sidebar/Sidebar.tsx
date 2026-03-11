import './Sidebar.scss'
import { MdDashboard, MdSearch } from "react-icons/md"
import { Link, useLocation } from "react-router-dom";

interface ISidebarProps {
    onDashboardClick: () => void;
}

export const Sidebar = ({ onDashboardClick }: ISidebarProps) => {
    const location = useLocation();
    const isDashboard = location.pathname === '/';

    return (
        <div className="sidebar-container">

            <div 
                className={`sidebar-item ${isDashboard ? 'active' : ''}`}
                onClick={onDashboardClick}
                style={{ cursor: 'pointer' }}
            >
                <MdDashboard size={22} />
                <span>Dashboard</span>
            </div>

            <div className="sidebar-item">
                <MdSearch size={22} />
                <span>Transactions</span>
            </div>

        </div>
    )
}
