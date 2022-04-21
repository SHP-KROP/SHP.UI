import { Link } from "@mui/material";
import { React, useState, useEffect } from "react";
import { AuthService } from "../../_services/AuthService";
import axios from "axios";

const BASE_URL = "https://localhost:44330/api/user/register/";

const Register = () => {
  const [name, setUsername] = useState(() => "");
  const [pass, setPassword] = useState(() => "");
  const [flag, setFlag] = useState(() => true);

  const [response, setResponse] = useState(() => {});

  useEffect(() => {
    if (!(name && pass)) {
      return;
    }
    console.log("Calling api started");
    axios
      .post(BASE_URL, {
        userName: name,
        password: pass,
      })
      .then((response) => {
        setResponse(response.data);
        proceedResponse(response.data);
      })
      .catch((error) => {
        if (error.response.status === 400) {
          alert("You cannot register such a user");
        } else if (error.request) {
          console.log(error.request);
        } else if (error.message) {
          console.log(error.message);
        }
      });
  }, [flag]);

  const proceedResponse = (response) => {
    try {
      alert(`Welcome ${response.userName}`);
      localStorage.setItem("token", response.token);
    } catch (ex) {
      console.log(ex);
      alert("Impossible to register such a user");
    }
  };

  return (
    <>
      <div>
        <div>
          <Link href="/main">Back to main page</Link>
        </div>
        <div className="form">
          <div>
            <h1>User Registration</h1>
          </div>

          {/* Calling to the methods */}
          <div className="messages"></div>

          <form>
            <label className="label">Name</label>
            <input
              type="text"
              onChange={(e) => {
                setUsername(e.target.value);
              }}
            />
            <label className="label">Password</label>
            <input
              type="password"
              onChange={(e) => {
                setPassword(e.target.value);
              }}
            />
          </form>
          <button className="btn" type="submit" onClick={() => setFlag(!flag)}>
            Submit
          </button>
        </div>
      </div>
    </>
  );
};

export default Register;
