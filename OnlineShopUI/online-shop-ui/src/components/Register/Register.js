import { Link, LinearProgress } from "@mui/material";
import { React, useState, useEffect, useRef } from "react";
import { toast } from "react-toastify";
import { Navigate } from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import axios from "axios";
import "./Register.scss";
import RegisterBG from "../../img/banner-bg.png";

const BASE_URL = "https://localhost:44330/api/user/register/";

toast.configure();

const Register = () => {
  let [name, setName] = useState(() => "");
  let [pass, setPass] = useState(() => "");
  let [flag, setFlag] = useState(() => false);
  let [isLoading, setIsLoading] = useState(() => false);
  let [isRedirect, setIsRedirect] = useState(() => false);
  let initialRender = useRef(true);

  useEffect(() => {
    if (initialRender.current) {
      initialRender.current = false;
      return;
    }

    if (!(name && pass)) {
      toast.warn("Name and password cannot be empty", {
        position: toast.POSITION.BOTTOM_RIGHT,
      });

      return;
    }

    setIsLoading(true);

    axios
      .post(BASE_URL, {
        userName: name,
        password: pass,
      })
      .then((response) => {
        proceedResponse(response.data);
      })
      .catch((error) => {
        console.warn(error.response);
        if (typeof error.response.data !== "object") {
          toast.error(`${error.response.data}`, {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
          return;
        }
        toast.error("Internal server error", {
          position: toast.POSITION.BOTTOM_RIGHT,
        });
      })
      .finally(() => {
        setIsLoading(false);
      });
  }, [flag]);

  const proceedResponse = (response) => {
    try {
      toast.success(`Welcome ${response.userName}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
      localStorage.setItem("token", response.token);
      setIsRedirect(true);
    } catch (ex) {
      console.warn(ex);
      toast.error(`${ex.error}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
    }
  };

  return (
    <>
      {isRedirect ? <Navigate push to="/main" /> : null}
      <div className="registration">
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
          <div className="registration__img">
            <img src={RegisterBG} alt="Bg" />
          </div>
        </div>
      </div>
    </>
  );
};

export default Register;
