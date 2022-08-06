import { toast } from 'react-toastify';
import { useState, useEffect, useRef } from 'react';
import IdentityAPI from '../../../API/IdentityServerAPI.js';
import useAuth from '../../../Helper/hook/useAuth';

toast.configure();

const useLogin = () => {
  const [name, setUsername] = useState(() => '');
  const [pass, setPassword] = useState(() => '');
  const [flag, setFlag] = useState(() => true);

  const { setUser } = useAuth();

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

    IdentityAPI.post('/user/login', {
      userName: name,
      password: pass,
    })
      .then((response) => {
        if (response) {
          proceedResponse(response?.data);

          return;
        }

        toast.error('Internal server error', {
          position: toast.POSITION.BOTTOM_RIGHT,
        });
      })
      .catch((error) => {
        try {
          console.warn(error?.response);
          if (typeof error.response.data !== 'object') {
            toast.error(`${error.response.data}`, {
              position: toast.POSITION.BOTTOM_RIGHT,
            });
            return;
          }
        } catch {
          toast.error('Internal server error', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      });
  }, [flag]);

  const proceedResponse = (response) => {
    try {
      toast.success(`Welcome ${response.userName}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
      setUser(response);
      console.success('Success, window need to be closed!');
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
