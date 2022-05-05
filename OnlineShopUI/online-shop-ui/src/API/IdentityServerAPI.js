import axios from "axios";

const DEBUG_IDENTITY_SERVER_URL = 'https://localhost:44330/api';
const DOCKER_IDENTITY_SERVER_URL = 'http://localhost:8080/api';

const axiosConfig = {
    baseURL: DOCKER_IDENTITY_SERVER_URL,
    timeout: 30 * 1000
};

const IdentityAPI = axios.create(axiosConfig);

export default IdentityAPI;