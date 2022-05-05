import axios from "axios";

const DEBUG_CORE_API_URL = 'https://localhost:44318/api';
const DOCKER_CORE_API_URL = 'http://localhost:8081/api';

const axiosConfig = {
    baseURL: DOCKER_CORE_API_URL,
    timeout: 30 * 1000
};

const CoreAPI = axios.create(axiosConfig);

export default CoreAPI;