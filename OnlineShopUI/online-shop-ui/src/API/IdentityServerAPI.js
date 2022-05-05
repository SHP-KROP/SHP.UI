import axios from "axios";
import config from "../config.json";

let identityServerBaseUrl = '';

switch (config.api.state) {
    case "docker":
        identityServerBaseUrl = config.api.docker.identityServer.url;
        break;
    case "debug":
        identityServerBaseUrl = config.api.debug.identityServer.url;
        break;
    default:
        identityServerBaseUrl = config.api.debug.identityServer.url;
}

const axiosConfig = {
    baseURL: identityServerBaseUrl,
    timeout: 30 * 1000
};

const IdentityAPI = axios.create(axiosConfig);

export default IdentityAPI;