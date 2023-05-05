import axios from 'axios';
import config from '../config.json';

let coreApiBaseUrl = '';

switch (config.api.state) {
  case 'docker':
    coreApiBaseUrl = config.api.docker.core.url;
    break;
  case 'debug':
    coreApiBaseUrl = config.api.debug.core.url;
    break;
  default:
    coreApiBaseUrl = config.api.debug.core.url;
}

const axiosConfig = {
  baseURL: coreApiBaseUrl,
  timeout: 30 * 1000,
};

const CoreAPI = axios.create(axiosConfig);

export default CoreAPI;
