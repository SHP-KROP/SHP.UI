import Axios from "axios";

class AuthService {
  constructor() {
    this.baseUrl = "https://localhost:44341/api/";
  }

  register(userDto) {
    const username = userDto.userName;
    const password = userDto.password;

    Axios.post(this.baseUrl + "register", {
      userName: username,
      password: password,
    }).then((response) => console.log(response));
  }
}

export { AuthService };
