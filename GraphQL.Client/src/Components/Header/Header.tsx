// import './Header.scss'
// interface IHeaderProps{

// }

// export const Header = (props:IHeaderProps) =>{
//     return(
//         <>
//             <div className="header-container d-flex col-12">
//                 <div className="app-logo col-6 d-flex justify-content-start align-items-center text-white"></div>
//                 <div className="app-search col-6 d-flex justify-content-end text-white align-items-center"></div>
//             </div>
//         </>
//     )
// }

import './Header.scss'

interface IHeaderProps {}

export const Header = (props: IHeaderProps) => {
    return (
        <div className="header-container">

            <div className="logo-section">
                <span className="logo-text">CloudMonitor</span>
            </div>

            <div className="search-section">
                <input
                    type="text"
                    placeholder="Search applications..."
                    className="search-input"
                />

                <div className="user-avatar">
                    NV
                </div>
            </div>

        </div>
    )
}