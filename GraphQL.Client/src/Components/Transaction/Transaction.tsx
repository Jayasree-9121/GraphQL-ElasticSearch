// import './Transaction.scss'
// export const Transaction = () => {
//     return (
//         <>
//             <div className='' style={{width:'90vw'}}>
//                 <div className='p-3'>
//                     <div className='d-flex justify-content-between'>
//                         <div className='col-6 d-flex flex-column justify-content-start'>
//                             <h3 className='text-white'>Transaction Search</h3>
//                             <p className='text-white'>Query and analyze application tetemetry and trace data</p>
//                         </div>
//                         <div className='justify-content-end col-6'>
//                             <div className='d-flex justify-content-end'>
//                                 <button className='btn btn-primary'>Query</button>
//                                 <button className='btn btn-secondary mx-2'>Save</button>
//                             </div>
//                         </div>
//                     </div>

//                     <div className='transaction-filter-container d-flex col-12'>
//                         <div className='text-white col-3'>error type

//                         </div>
//                         <div className='text-white col-3'>error type</div>
//                         <div className='text-white col-3'>error type</div>
//                         <div className='text-white col-3'>error type</div>
//                     </div>
//                 </div>
//             </div>
//         </>
//     )
// }








import './Transaction.scss'

export const Transaction = () => {
    return (
        <div className='transaction-page container-fluid d-flex flex-column gap-5'>

            {/* Header */}
            <div className='transaction-header d-flex justify-content-between align-items-center mx-2'>
                <div>
                    <h2>Transaction Search</h2>
                    <p>Query and analyze application telemetry and trace data.</p>
                </div>

                <div className='d-flex'>
                    {/* <button className='btn run-btn'>▶ Run Query</button>
                    <button className='btn save-btn ms-2'>💾 Save</button> */}

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