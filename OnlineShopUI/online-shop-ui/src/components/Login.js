import React, { useState, useEffect, useRef } from "react";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import CloseIcon from "@mui/icons-material/Close";
import "./Login.scss";
import FormGroup from "@mui/material/FormGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import User from "../img/icon-user.png";
import { Link } from "react-router-dom";
import axios from "axios";
import { toast } from "react-toastify";

const BASE_URL = "https://localhost:44330/api/user/login/";

toast.configure();

export default function Login() {
  const [isOpen, setOpen] = useState(() => false);
  const handleLogInModalOpen = () => setOpen(true);
  const handleLogInModalClose = () => setOpen(false);

  const [name, setUsername] = useState(() => "");
  const [pass, setPassword] = useState(() => "");
  const [flag, setFlag] = useState(() => true);
  let initialRender = useRef(true);

  useEffect(() => {
    if (initialRender.current) {
      initialRender.current = false;
      return;
    }

    if (localStorage.getItem("token")) {
      toast.warn("You are already logged in!", {
        position: toast.POSITION.BOTTOM_RIGHT,
      });

      return;
    }

    if (!(name && pass)) {
      toast.warn("Name and password cannot be empty", {
        position: toast.POSITION.BOTTOM_RIGHT,
      });

      return;
    }

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
      });
  }, [flag]);

  const proceedResponse = (response) => {
    try {
      toast.success(`Welcome ${response.userName}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
      localStorage.setItem("token", response.token);
      handleLogInModalClose();
    } catch (ex) {
      console.warn(ex);
      toast.error(`${ex.error}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
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
