import axios from 'axios';

declare const coreApiBaseUrl: string;

const axiosConfig = {
  baseURL: coreApiBaseUrl,
  timeout: 30 * 1000,
};

const CoreAPI = axios.create(axiosConfig);

export default CoreAPI;
