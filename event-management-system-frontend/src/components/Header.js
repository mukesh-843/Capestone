import React, { useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import '../App.css';
import { Info, Menu, X } from 'lucide-react';

const Header = () => {
  const { user, logout } = useAuth();
  const location = useLocation();
  const isLoginPage = location.pathname === '/login';
  const isRegisterPage = location.pathname === '/register';
  
  const [showInfo, setShowInfo] = useState(false);
  const [menuOpen, setMenuOpen] = useState(false);

  return (
    <header>
      <nav>
        <div className="nav-left">
          <Link className="button-link" to="/">Events</Link>
          <button className="info-icon" onClick={() => setShowInfo(true)}>
            <Info size={24} />
          </button>
          {user?.role && <Link className="button-link" to="/create-event">Create Event</Link>}
        </div>

        <div className="nav-right">
          {user ? (
            <div className="user-menu">
              <span className="welcome-text">Welcome, {user.name}</span>
              <button onClick={logout} className="logout-button">Logout</button>
            </div>
          ) : (
            <div className="auth-nav">
              {isLoginPage ? (
                <Link className="button-link" to="/register">Register</Link>
              ) : isRegisterPage ? (
                <Link className="button-link" to="/login">Login</Link>
              ) : (
                <>
                  <Link className="button-link" to="/login">Login</Link>
                  <Link className="button-link" to="/register">Register</Link>
                </>
              )}
            </div>
          )}
        </div>

        {/* Hamburger Menu for Mobile */}
        <div className="hamburger-menu">
          <button onClick={() => setMenuOpen(!menuOpen)} className="hamburger-button">
            {menuOpen ? <X size={28} /> : <Menu size={28} />}
          </button>
          
          {menuOpen && (
            <div className="hamburger-dropdown">
              {user && <span className="welcome-text">Welcome, {user.name}</span>}
              {user?.role === 'admin' && <div className="admin-tag">Admin</div>}
              <Link className="button-link" to="/" onClick={() => setMenuOpen(false)}>Events</Link>
              {user && <Link className="button-link" to="/create-event" onClick={() => setMenuOpen(false)}>Create Event</Link>}
              <button onClick={() => setShowInfo(true)} className="button-link">Info</button>
              {user && (
                <button onClick={() => {
                  logout();
                  setMenuOpen(false);
                }} className="logout-button">
                  Logout
                </button>
              )}
              {!user && (
                <>
                  {isLoginPage ? (
                    <Link className="button-link" to="/register" onClick={() => setMenuOpen(false)}>Register</Link>
                  ) : isRegisterPage ? (
                    <Link className="button-link" to="/login" onClick={() => setMenuOpen(false)}>Login</Link>
                  ) : (
                    <>
                      <Link className="button-link" to="/login" onClick={() => setMenuOpen(false)}>Login</Link>
                      <Link className="button-link" to="/register" onClick={() => setMenuOpen(false)}>Register</Link>
                    </>
                  )}
                </>
              )}
            </div>
          )}
        </div>

        {/* Info Modal */}
        {showInfo && (
          <div className="info-modal-backdrop" onClick={() => setShowInfo(false)}>
            <div className="info-modal" onClick={e => e.stopPropagation()}>
              <h2>Welcome</h2>
              <p>This is an Event Management Application made by Mukesh Gautam{' '}
                <a href="https://www.linkedin.com/in/mukeshgtm/" target="_blank" rel="noopener noreferrer">
                  ➡️Click Me⬅️
                </a>
              </p>
              

              <button className="close-modal" onClick={() => setShowInfo(false)}>Close</button>
            </div>
          </div>
        )}
      </nav>
    </header>
  );
};

export default Header;
