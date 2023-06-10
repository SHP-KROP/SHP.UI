import axios from 'axios';

const axiosConfig = {
  baseURL: process.env.IDENTITY_SERVER_URL,
  timeout: 30 * 1000,
};

const IdentityAPI = axios.create(axiosConfig);
export default IdentityAPI;
