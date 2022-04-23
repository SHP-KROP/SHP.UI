import { Link } from '@mui/material';
import { React, useState, useEffect } from 'react';
import { AuthService } from '../../_services/AuthService';
import axios from 'axios';
import './Register.scss';
import RegisterBG from '../../img/banner-bg.png';

const BASE_URL = 'https://localhost:44330/api/user/register/';

const Register = () => {
  const [name, setUsername] = useState(() => '');
  const [pass, setPassword] = useState(() => '');
  const [flag, setFlag] = useState(() => true);

  const [response, setResponse] = useState(() => {});

  useEffect(() => {
    if (!(name && pass)) {
      return;
    }
    console.log('Calling api started');
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
          alert('You cannot register such a user');
        } else if (error.request) {
          console.log(error.request);
        } else if (error.message) {
          console.log(error.message);
        }
      });
  });

  const proceedResponse = (response) => {
    try {
      alert(`Welcome ${response.userName}`);
      localStorage.setItem('token', response.token);
    } catch (ex) {
      console.log(ex);
      alert('Impossible to register such a user');
    }
  };

  return (
    <>
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
                <label className="label">Full Name</label>
                <input
                  type="text"
                  onChange={(e) => {
                    setUsername(e.target.value);
                  }}
                />
              </div>
              <div className="registration__inputPassword input">
                <label className="label">Password</label>
                <input
                  type="password"
                  onChange={(e) => {
                    setPassword(e.target.value);
                  }}
                />
              </div>

              <button
                className="registration__btn"
                type="submit"
                onClick={() => setFlag(!flag)}
              >
                Get Started
              </button>
            </form>

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
