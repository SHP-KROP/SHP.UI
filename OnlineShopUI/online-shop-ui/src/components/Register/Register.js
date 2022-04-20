import { Link } from "@mui/material";
import { React, useState } from "react";
import { AuthService } from "../../_services/AuthService";

const Register = () => {
  const [userName, setUsername] = useState(() => "");
  const [password, setPassword] = useState(() => "");

  const authService = new AuthService();

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
            <button
              className="btn"
              type="submit"
              onClick={authService.register({
                userName: userName,
                password: password,
              })}
            >
              Submit
            </button>
          </form>
        </div>
      </div>
    </>
  );
};

export default Register;
