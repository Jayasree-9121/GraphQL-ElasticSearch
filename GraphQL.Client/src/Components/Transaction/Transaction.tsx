import { useEffect, useState } from 'react';
import './Transaction.scss'
import type { TransactionLogs } from '../../Interface/Transaction';

interface ITransactionProps {
    applicationName?: string;
}

export const Transaction = (props: ITransactionProps) => {
    const [errorLogs, setErrorLogs] = useState<TransactionLogs[]>([]);
    const [loader, setLoader] = useState(false);
    const [errors, setErrors] = useState();

const fetchUserData = async (text: string) => {
    try {
        const response = await fetch('https://localhost:7020/graphql', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                query: `
                query ($text: String!) {
                    searchElasticData(text: $text) {
                        session_Id
                        user_Id
                        type
                    }
                }
                `,
                variables: { text }
            }),
        });

        const result = await response.json();

        if (result.errors) {
            setErrors(result.errors);
            return;
        }

        setErrorLogs(result.data.user);
        setLoader(false);
        console.log(result.data.user);

    } catch (error) {
        console.error("Fetch error:", error);
    }
};

    useEffect(()=>{
        fetchUserData("uncaught");
    },[errorLogs])


    return (
        <div className='transaction-page container-fluid d-flex flex-column gap-5'>

            {/* Header */}
            <div className='transaction-header d-flex justify-content-between align-items-center mx-2'>
                <div>
                    <h2>{props.applicationName ? `${props.applicationName} - Transaction Search` : 'Transaction Search'}</h2>
                    <p>Query and analyze application telemetry and trace data.</p>
                </div>

                <div className='d-flex'>
                    <button className='btn btn-primary'>Query</button>
                    <button className='btn btn-secondary mx-2'>Save</button>
                </div>
            </div>

            {/* Filters */}
            <div className='transaction-filter-container row mx-2'>

                <div className='col-md-3 filter-item'>
                    <label>Error Type</label>
                    <input
                        type="text"
                        placeholder="e.g. NullReferenceException"
                        className='form-control'
                    />
                </div>

                <div className='col-md-3 filter-item'>
                    <label>Time Range</label>
                    <select className='form-control'>
                        <option>Last 24 Hours</option>
                        <option>Last 7 Days</option>
                        <option>Last 30 Days</option>
                    </select>
                </div>

                <div className='col-md-3 filter-item'>
                    <label>Operation Name</label>
                    <input
                        type="text"
                        placeholder="GET /api/v1/orders"
                        className='form-control'
                    />
                </div>

                <div className='col-md-3 filter-item'>
                    <label>Minimum Duration (ms)</label>
                    <input
                        type="number"
                        placeholder="0"
                        className='form-control'
                    />
                </div>

            </div>

            {/* Result Section */}
            <div className='result-section'>

                <div className='result-header d-flex justify-content-between'>
                    <h5>Result Detail</h5>

                    <div>
                        <button className='btn btn-sm btn-outline-light'>Copy JSON</button>
                        <button className='btn btn-sm btn-outline-light ms-2'>Download</button>
                    </div>
                </div>

                <div className='result-body'>
                    <pre>
                        {`{
  "timestamp": "2023-11-20T14:22:31.004Z",
  "id": "9d3b-487a-85d1",
  "severityLevel": "Error",
  "message": "Critical failure in payment processing pipeline",
  "operation": {
    "name": "POST /v1/checkout",
    "id": "f4e2c901",
    "parentId": "a8823f21"
  },
  "exception": {
    "type": "System.NullReferenceException"
  }
}`}
                    </pre>
                </div>

            </div>

        </div>
    )
}