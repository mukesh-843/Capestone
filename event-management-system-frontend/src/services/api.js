import axios from 'axios';

const API_URL = 'https://localhost:7037';

export const getEvents = async () => {
  const response = await axios.get(`${API_URL}/api/Events`);
  return response.data;
};

export const getEventById = async (id) => {
  const response = await axios.get(`${API_URL}/api/Events/${id}`);
  return response.data;
};

export const rsvpEvent = async (eventId) => {
  const token = localStorage.getItem('token');
  await axios.post(`${API_URL}/api/Events/${eventId}/rsvp`, {}, {
    headers: { Authorization: `Bearer ${token}` },
  });
};

export const loginUser = async (email, password) => {
  const response = await axios.post(`${API_URL}/api/Users/login`, { email, password });
  return response.data;
};

export const registerUser = async (name, email, password, isAdmin) => {
  console.log('api isAdmin:', isAdmin);

  const endpoint = isAdmin ? '/api/admin/register' : '/api/Users/register';
  try {
    const response = await axios.post(`${API_URL}${endpoint}`, { name, email, password });
    return response.data;
  } catch (error) {
    console.error('Error registering user:', error);
    throw error;
  }
};

export const createEvent = async (eventData) => {
  const token = localStorage.getItem('token');

  if(!token){
    throw new Error("Unauthorised:No token found");
  }

  try {
    const response = await axios.post(`${API_URL}/api/Events`, eventData, {
      headers: {
        Authorization: `Bearer ${token}`
      },
    });
    return response.data;
  } catch (error) {
    console.error("Error creating event:", error.response ? error.response.data : error.message);
    throw error; 
  }
};


export const deleteEvent = async (eventId) => {
  const token = localStorage.getItem('token');
  await axios.delete(`${API_URL}/api/Events/${eventId}`, {
    headers: { Authorization: `Bearer ${token}` },
  });
};

export const getEventAttendees = async (eventId) => {
  const token = localStorage.getItem('token');
  const response = await axios.get(`${API_URL}/api/Events/${eventId}/attendees`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return response.data;
};

export const sendEventReminder = async (eventId) => {
  const token = localStorage.getItem('token'); 

  const response = await axios.post(`${API_URL}/api/Events/${eventId}/remind`, {}, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  return response.data;
};
