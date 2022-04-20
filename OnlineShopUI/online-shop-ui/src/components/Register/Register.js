import { Link } from "@mui/material";
import { React, useState, useEffect } from "react";
import { AuthService } from "../../_services/AuthService";
import axios from "axios";

const BASE_URL = "https://localhost:44330/api/user/register/";

const Register = () => {
  const [name, setUsername] = useState(() => "");
  const [pass, setPassword] = useState(() => "");

  function register() {
    axios
      .post(BASE_URL, {
        userName: name,
        password: pass,
      })
      .then((response) => response.data)
      .then((data) => localStorage.setItem("RESPONSE", data));
  }

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
            <button className="btn" type="submit" onClick={register}>
              Submit
            </button>
          </form>
        </div>
      </div>
    </>
  );
};

export default Register;
