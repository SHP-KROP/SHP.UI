import { Link, LinearProgress } from "@mui/material";
import { Navigate } from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import "./Register.scss";
import Feedback from "../../components/FeedbackATop/Feedback";
import useRegister from "./Logic/RegisterLogic";

const Register = () => {
  const [isRedirect, isLoading, setFlag, setName, setPass, flag] =
    useRegister();

  return (
    <>
      {isRedirect ? <Navigate push to="/main" /> : null}
      <div className="registration">
        <Feedback />

        <div className="registration__body">
          <div className="registration__info">
            <div className="registration__header">
              <div className="registration__greeting">
                <p>
                  <strong>Welcome</strong>
                </p>
                <span>Register new account</span>
              </div>
              <div className="registration__backToSign">
                <Link>Sign in</Link>
              </div>
            </div>
            {/* Calling to the methods */}
            <div className="messages"></div>

            <form className="registration__form">
              <div className="registration__inputName input">
                <label className="label">Username</label>
                <input
                  type="text"
                  onChange={(e) => {
                    setName(e.target.value);
                  }}
                />
              </div>

              <div className="registration__inputPassword input">
                <label className="label">Password</label>
                <input
                  type="password"
                  onChange={(e) => {
                    setPass(e.target.value);
                  }}
                />
              </div>
            </form>
            {isLoading ? (
              <LinearProgress />
            ) : (
              <button
                className="registration__btn"
                type="submit"
                onClick={() => {
                  setFlag(!flag);
                }}
              >
                Get Started
              </button>
            )}

            <div className="registration__exit">
              <Link href="/main">Back to main page</Link>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Register;
