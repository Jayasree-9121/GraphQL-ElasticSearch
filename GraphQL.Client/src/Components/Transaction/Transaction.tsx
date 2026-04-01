import { useState } from 'react';
import './Transaction.scss'
import type { TransactionLogs } from '../../Interface/Transaction';

interface ITransactionProps {
    applicationName?: string;
}

export const Transaction = (props: ITransactionProps) => {
    const [errorLogs, setErrorLogs] = useState<TransactionLogs[]>([]);
    const [loader, setLoader] = useState(false);
    const [errors, setErrors] = useState<string | null>(null);
    const [viewMode, setViewMode] = useState<'json' | 'table'>('json');

    const [errorType, setErrorType] = useState('');
    const [timeRange, setTimeRange] = useState('Last 24 Hours');
    const [operationName, setOperationName] = useState('');
    const [minDuration, setMinDuration] = useState<number>(0);

    const fetchUserData = async (text: string) => {
        try {
            setLoader(true);
            const response = await fetch('https://localhost:7020/graphql', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    query: `
                    query ($text: String!) {
                        searchElasticData(text: $text) {
                                planning {
                                    errorCategory
                                    technologies
                                }
                                analysis {
                                  errorType
                                  errorSummary
                                  businessImpact
                                  severity
                                  fixSuggestions
                                  recommendedFix
                                }
                        }
                    }`,
                    variables: { text }
                }),
            });

const result = await response.json();

if (result.errors) {
    console.warn("GraphQL Errors:", result.errors);
    setErrors(result.errors[0]?.message || 'GraphQL errors occurred');
    setLoader(false);
    return;
}

const rawData = result.data?.searchElasticData || [];

const mappedData: TransactionLogs[] = [];
const planningArr = rawData.planning || [];
const analysisArr = rawData.analysis || [];
const maxLen = Math.max(planningArr.length, analysisArr.length);

for(let i = 0; i < maxLen; i++) {
    const planning = planningArr[i] || {};
    const analysis = analysisArr[i] || {};
    mappedData.push({
        ErrorCategory:  planning.errorCategory,
        Technologies: planning.technologies ,
        InvestigationSteps: analysis.investigationSteps || '',
        SearchQueries: analysis.searchQueries || '',
        ErrorType: analysis.errorType || '',
        ErrorSummary: analysis.errorSummary || '',
        PossibleCauses: analysis.possibleCauses || [],
        OccurrenceConditions: analysis.occurrenceConditions || [],
        BusinessImpact: analysis.businessImpact || '',
        Severity: analysis.severity || '',
        FixSuggestions: analysis.fixSuggestions || [],
        RecommendedFix: analysis.recommendedFix || '',
    })
}

setErrorLogs(mappedData);
            setLoader(false);

        } catch (error) {
            console.error("Fetch error:", error);
            setLoader(false);
        }
    };

    const handleQuery = () => {
        const searchText = errorType || operationName || "uncaught";
        fetchUserData(searchText);
    };

    const renderJsonView = () => {
        if (errorLogs.length === 0) {
            return (
                <div className="no-data">
                    <p>No error logs found.</p>
                </div>
            );
        }

        return (
            <pre className="json-content">
                {JSON.stringify(errorLogs, null, 2)}
            </pre>
        );
    };

    const renderTableView = () => {
        if (errorLogs.length === 0) {
            return (
                <div className="no-data">
                    <p>No error logs found.</p>
                </div>
            );
        }
        
        return (
            <div className="table-container">
                <table className="error-logs-table">
<thead>
                        <tr>
                            <th>Error Category</th>
                            <th>Technologies</th>
                            <th>Investigation Steps</th>
                            <th>Search Queries</th>
                            <th>Error Type</th>
                            <th>Error Summary</th>
                            <th>Business Impact</th>
                            <th>Severity</th>
                            <th>Fix Suggestions</th>
                            <th>Recommended Fix</th>
                        </tr>
                    </thead>
                    <tbody>
                        {errorLogs.map((log, index) => (
                            <tr>
                                <td>{log.ErrorCategory}</td>
                                <td>{log.Technologies}</td>
                                <td>{log.InvestigationSteps}</td>
                                <td>{log.SearchQueries}</td>
                                <td>{log.ErrorType}</td>
                                <td>{log.ErrorSummary}</td>
                                <td>{log.BusinessImpact}</td>
                                <td>{log.Severity}</td>
                                <td>{log.FixSuggestions.join(', ')}</td>
                                <td>{log.RecommendedFix}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    };

    const copyToClipboard = () => {
        navigator.clipboard.writeText(JSON.stringify(errorLogs, null, 2));
    };

    return (
        <div className='transaction-page container-fluid d-flex flex-column gap-5'>

            <div className='transaction-header d-flex justify-content-between align-items-center mx-2'>
                <div>
                    <h2>{props.applicationName ? `${props.applicationName} - Transaction Search` : 'Transaction Search'}</h2>
                    <p>Query and analyze application telemetry and trace data.</p>
                </div>

                <div className='d-flex'>
                    <button className='btn btn-primary' onClick={handleQuery}>Query</button>
                    <button className='btn btn-secondary mx-2'>Save</button>
                </div>
            </div>

            <div className='transaction-filter-container row mx-2'>

                <div className='col-md-3 filter-item'>
                    <label>Error Type</label>
                    <input
                        type="text"
                        placeholder="e.g. NullReferenceException"
                        className='form-control'
                        value={errorType}
                        onChange={(e) => setErrorType(e.target.value)}
                    />
                </div>

                <div className='col-md-3 filter-item'>
                    <label>Time Range</label>
                    <select 
                        className='form-control'
                        value={timeRange}
                        onChange={(e) => setTimeRange(e.target.value)}
                    >
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
                        value={operationName}
                        onChange={(e) => setOperationName(e.target.value)}
                    />
                </div>

                <div className='col-md-3 filter-item'>
                    <label>Minimum Duration (ms)</label>
                    <input
                        type="number"
                        placeholder="0"
                        className='form-control'
                        value={minDuration}
                        onChange={(e) => setMinDuration(Number(e.target.value))}
                    />
                </div>

            </div>

            <div className='result-section'>

                <div className='result-header d-flex justify-content-between align-items-center'>
                    <h5>Result Detail</h5>

                    <div className='d-flex align-items-center gap-3'>
                        <div className='view-toggle'>
                            <button
                                className={`toggle-btn ${viewMode === 'json' ? 'active' : ''}`}
                                onClick={() => setViewMode('json')}
                            >
                                JSON
                            </button>
                            <button
                                className={`toggle-btn ${viewMode === 'table' ? 'active' : ''}`}
                                onClick={() => setViewMode('table')}
                            >
                                Table
                            </button>
                        </div>

                        <button className='btn btn-sm btn-outline-light' onClick={copyToClipboard}>
                            Copy JSON
                        </button>
                        <button className='btn btn-sm btn-outline-light ms-2'>Download</button>
                    </div>
                </div>

                <div className='result-body'>
                    {loader ? (
                        <div className="loader-container">
                            <div className="spinner-border text-primary" role="status">
                                <span className="visually-hidden">Loading...</span>
                            </div>
                        </div>
                    ) : errors ? (
                        <div className="error-message">
                            <p>Error: {errors}</p>
                        </div>
                    ) : viewMode === 'json' ? (
                        renderJsonView()
                    ) : (
                        renderTableView()
                    )}
                </div>

            </div>

        </div>
    )
}

