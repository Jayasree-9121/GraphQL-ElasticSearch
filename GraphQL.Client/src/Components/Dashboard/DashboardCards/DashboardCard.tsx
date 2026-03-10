import Card from "react-bootstrap/Card";
import './DashboardCard.scss'

export const DasboardCard = () => {
    return (
        <div className="card-container">
            <Card className="shadow-sm dashboard-card m-0">
                <Card.Body>
                    <Card.Title>Total Users</Card.Title>
                    <Card.Subtitle className="mb-2">
                        Active Accounts
                    </Card.Subtitle>
                </Card.Body>
            </Card>
        </div>
    );
};
//