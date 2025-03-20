import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { User, Mail, Lock, Shield } from 'lucide-react';
import '../App.css';
import { useAuth } from '../context/AuthContext';


const Register = () => {
  const { register } = useAuth();
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isAdmin, setIsAdmin] = useState(false);
  const [error, setError] = useState('');
  const navigate = useNavigate();


  const handleSubmit = async (e) => {
    console.log('registering isAdmin:', isAdmin);
    e.preventDefault();
    setError(''); // Clear any previous errors
    try {
      const response = await register(name, email, password, isAdmin);

      if (response.ok) {
        alert(isAdmin ? 'Admin registration successful!' : 'Registration successful!');
        navigate('/login'); // Redirect to login page after successful registration
      } else {
        const { message } = await response.json();
        setError(message); // Show error message if registration fails
      }
    } catch (err) {
      setError('Failed to register. Please try again.');
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h1 className="auth-title">Create Account</h1>
        {error && <div className="error-message">{error}</div>}
        <form onSubmit={handleSubmit} className="auth-form">
          <div className="form-group">
            <label htmlFor="name">Full Name</label>
            <div className="relative">
              <User className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" size={18} />
              <input
                id="name"
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Enter your full name"
                className="pl-10"
                required
              />
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="email">Email Address</label>
            <div className="relative">
              <Mail className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" size={18} />
              <input
                id="email"
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Enter your email"
                className="pl-10"
                required
              />
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="password">Password</label>
            <div className="relative">
              <Lock className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" size={18} />
              <input
                id="password"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Create a password"
                className="pl-10"
                required
              />
            </div>
          </div>
          {/* <div className="form-group admin-toggle">
            <input
              type="checkbox"
              id="adminCheckbox"
              checked={isAdmin}
              onChange={(e) => setIsAdmin(e.target.checked)}
              className="form-checkbox"
            />
            <label htmlFor="adminCheckbox">
              <Shield size={18} className="text-gray-400" />
              Register as Admin
            </label>
          </div> */}

          <button type="submit" className="auth-button">
            {isAdmin ? 'Create Admin Account' : 'Create Account'}
          </button>
        </form>
        <div className="auth-links">
          Already have an account? <Link to="/login">Sign in</Link>
        </div>
      </div>
    </div>
  );
};

export default Register;