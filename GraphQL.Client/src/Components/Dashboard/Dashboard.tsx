import { useState } from "react";
import { Row, Col, Container } from "react-bootstrap";
import { DasboardCard } from "./DashboardCards/DashboardCard";
import './Dashboard.scss'

export const Dashboard = () => {
    const [board] = useState([1, 2, 3, 4, 5, 6, 7, 8]);

    return (
        <Container fluid className="dashboard-container px-4">
            <Row className="g-4">
                {board.map((item) => (
                    <Col key={item} xs={12} sm={6} lg={3} className="mb-4">
                        <DasboardCard />
                    </Col>
                ))}
            </Row>
        </Container>
    );
};
