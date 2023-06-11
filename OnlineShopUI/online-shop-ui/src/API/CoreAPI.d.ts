import axios from 'axios';

const axiosConfig = {
  baseURL: process.env.REACT_APP_CORE_API_URL,
  timeout: 30 * 1000,
};

const CoreAPI = axios.create(axiosConfig);

export default CoreAPI;
