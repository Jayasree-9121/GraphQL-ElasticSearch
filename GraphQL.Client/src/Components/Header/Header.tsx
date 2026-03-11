import './Header.scss'

interface IHeaderProps {
    applicationName?: string;
}

export const Header = ({ applicationName }: IHeaderProps) => {
    return (
        <div className="header-container">

            <div className="logo-section">
                <span className="logo-text">Cloud Monitor</span>
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
