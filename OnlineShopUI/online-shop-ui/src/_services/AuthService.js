import Axios from "axios";

class AuthService {
  constructor() {
    this.baseUrl = "https://localhost:44341/";
  }

  register(userDto) {
    const username = userDto.userName;
    const password = userDto.password;

    Axios.post(this.baseUrl + "register", userDto).then((response) => {
      console.log(response);
    });
  }
}

export { AuthService };
