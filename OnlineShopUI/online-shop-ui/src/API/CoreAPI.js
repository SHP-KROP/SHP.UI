import axios from 'axios';

const axiosConfig = {
  baseURL: process.env.CORE_API_URL,
  timeout: 30 * 1000,
};

const CoreAPI = axios.create(axiosConfig);

export default CoreAPI;
