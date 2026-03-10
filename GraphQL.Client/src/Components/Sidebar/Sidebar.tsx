// import './Sidebar.scss'
// export const Sidebar = () =>{
//     return(
//         <>
//             <div className="sidebar-container d-flex justify-content-center align-items-center text-white flex-column">
//                 <div></div>
//             </div>
//         </>
//     )
// }


import './Sidebar.scss'
import { MdDashboard, MdSearch } from "react-icons/md"

export const Sidebar = () => {
    return (
        <div className="sidebar-container">

            <div className="sidebar-item active">
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