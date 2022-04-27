import axios from 'axios';
import { toast } from 'react-toastify';
import { useState, useEffect, useRef } from 'react';
import Handlers from '../../../Helper/Handlers';
const BASE_URL = 'https://localhost:44330/api/user/login/';

toast.configure();

const useLogin = () => {
  const [handleLogInModalClose] = Handlers();

  const [name, setUsername] = useState(() => '');
  const [pass, setPassword] = useState(() => '');
  const [flag, setFlag] = useState(() => true);
  let initialRender = useRef(true);

  useEffect(() => {
    if (initialRender.current) {
      initialRender.current = false;
      return;
    }

    if (localStorage.getItem('token')) {
      toast.warn('You are already logged in!', {
        position: toast.POSITION.BOTTOM_RIGHT,
      });

      return;
    }

    if (!(name && pass)) {
      toast.warn('Name and password cannot be empty', {
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
        if (typeof error.response.data !== 'object') {
          toast.error(`${error.response.data}`, {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
          return;
        }
        toast.error('Internal server error', {
          position: toast.POSITION.BOTTOM_RIGHT,
        });
      });
  }, [flag]);

  const proceedResponse = (response) => {
    try {
      toast.success(`Welcome ${response.userName}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
      localStorage.setItem('token', response.token);
      handleLogInModalClose();
    } catch (ex) {
      console.warn(ex);
      toast.error(`${ex.error}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
    }
  };

  return [setUsername, setPassword, flag, setFlag];
};

export default useLogin;
