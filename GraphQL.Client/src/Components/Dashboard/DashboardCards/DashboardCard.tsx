import Card from "react-bootstrap/Card";
import './DashboardCard.scss'
import type { Application } from "../../../Interface/Application";


interface IDashboardCardProps {
    application: Application;
    onClick: () => void;
}

export const DasboardCard = ({ application, onClick }: IDashboardCardProps) => {
    return (
        <div className="card-container" onClick={onClick} style={{ cursor: 'pointer' }}>
            <Card className="shadow-sm dashboard-card m-0">
                <Card.Body>
                    <Card.Title>{application.name}</Card.Title>
                    <Card.Subtitle className="mb-2">
                        {application.description}
                    </Card.Subtitle>
                </Card.Body>
            </Card>
        </div>
    );
};
