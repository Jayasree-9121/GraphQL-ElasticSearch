import { Row, Col, Container } from "react-bootstrap";
import { DasboardCard } from "./DashboardCards/DashboardCard";
import './Dashboard.scss'
import type { Application } from "../../Interface/Application";



const applications: Application[] = [
    { id: "1", name: "MID", description: "Market Intelligence Data" },
    { id: "2", name: "BOM", description: "Bill of Materials Tracker" },
    { id: "3", name: "NOP", description: "Number Order Processing" },
    { id: "4", name: "PO TRACKER", description: "Purchase Order Tracking System" },
    { id: "5", name: "Inventory", description: "Inventory Management" },
    { id: "6", name: "Orders", description: "Order Processing System" },
    { id: "7", name: "Reports", description: "Analytics and Reporting" },
    { id: "8", name: "Settings", description: "Application Settings" },
];

interface IDashboardProps {
    onSelectApplication: (app: Application | null) => void;
}

export const Dashboard = ({ onSelectApplication }: IDashboardProps) => {

    const handleCardClick = (app: Application) => {
        onSelectApplication(app);
    };

    return (
        <Container fluid className="dashboard-container px-4">
            <Row className="g-4">
                {applications.map((app) => (
                    <Col key={app.id} xs={12} sm={6} lg={3} className="mb-4">
                        <DasboardCard 
                            application={app} 
                            onClick={() => handleCardClick(app)} 
                        />
                    </Col>
                ))}
            </Row>
        </Container>
    );
};
