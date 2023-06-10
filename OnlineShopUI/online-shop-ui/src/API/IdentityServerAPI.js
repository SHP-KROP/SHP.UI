import axios from 'axios';

const axiosConfig = {
  baseURL: process.env.REACT_APP_IDENTITY_SERVER_URL,
  timeout: 30 * 1000,
};

const IdentityAPI = axios.create(axiosConfig);
export default IdentityAPI;
