import React, { useState, useEffect } from "react";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import CloseIcon from "@mui/icons-material/Close";
import "./Login.scss";
import FormGroup from "@mui/material/FormGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import User from "../img/icon-user.png";
import Register from "./Register/Register";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";
import axios from "axios";

const BASE_URL = "https://localhost:44330/api/user/login/";

export default function Login() {
  const [isOpen, setOpen] = useState(() => false);
  const handleLogInModalOpen = () => setOpen(true);
  const handleLogInModalClose = () => setOpen(false);

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
        if (error.response.status === 401) {
          alert("Wrong user credentials");
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
    <div>
      <a href="#">
        <img
          src={User}
          alt="user"
          className="log-btn"
          onClick={handleLogInModalOpen}
        />
      </a>
      <Modal
        className="log-modal"
        open={isOpen}
        onClose={handleLogInModalClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box>
          <div className="login">
            <div className="login__type">
              <p>
                <strong>Вход</strong>
              </p>
              <CloseIcon
                className="login__btnclose"
                onClick={handleLogInModalClose}
              ></CloseIcon>
            </div>
            <div className="login__date">
              <div className="login__email">
                <label>Username</label>
                <input
                  type="text"
                  onChange={(e) => {
                    setUsername(e.target.value);
                  }}
                />
              </div>
              <div className="login__pass">
                <label>Пароль</label>
                <input
                  type="password"
                  onChange={(e) => {
                    setPassword(e.target.value);
                  }}
                />
              </div>
              <a href="#">Забыли пароль?</a>
              <FormGroup>
                <FormControlLabel
                  className="login__check"
                  control={<Checkbox defaultChecked />}
                  label="Запомнить меня"
                />
              </FormGroup>
            </div>
            <div className="login__actions">
              <div className="login__sign">
                <button
                  className="login__sign-btn"
                  onClick={() => setFlag(!flag)}
                >
                  Войти
                </button>
              </div>
              <div className="login__register">
                <Link to="/register" onClick={handleLogInModalClose}>
                  Register
                </Link>
              </div>
            </div>
          </div>
        </Box>
      </Modal>
    </div>
  );
}
