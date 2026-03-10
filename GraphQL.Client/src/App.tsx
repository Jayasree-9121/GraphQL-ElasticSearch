import './App.css'
import { Dashboard } from './Components/Dashboard/Dashboard'
import { Header } from './Components/Header/Header'
import { Sidebar } from './Components/Sidebar/Sidebar'
import { Transaction } from './Components/Transaction/Transaction'
import UserList from './UserList'
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';

function App() {

  return (
    <>
      {/* <UserList /> */}
      <Header />
      <div className='d-flex dashboard-body'>
        <Sidebar />
        {/* <Dashboard /> */}

        <BrowserRouter>

          {/* Routes */}
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/transaction" element={<Transaction />} />
          </Routes>
        </BrowserRouter>
      </div>
    </>
  )
}

export default App
