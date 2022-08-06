import { useState, useEffect, useRef } from 'react';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import IdentityServerAPI from '../../../API/IdentityServerAPI';
import useAuth from '../../../Helper/hook/useAuth';

toast.configure();

const useRegister = () => {
  let [name, setName] = useState(() => '');
  let [pass, setPass] = useState(() => '');
  let [flag, setFlag] = useState(() => false);
  let [isLoading, setIsLoading] = useState(() => false);
  let [isRedirect, setIsRedirect] = useState(() => false);

  const { setUser } = useAuth();

  let initialRender = useRef(true);

  useEffect(() => {
    if (initialRender.current) {
      initialRender.current = false;
      return;
    }

    if (!(name && pass)) {
      toast.warn('Name and password cannot be empty', {
        position: toast.POSITION.BOTTOM_RIGHT,
      });

      return;
    }

    setIsLoading(true);

    IdentityServerAPI.post('/user/register', {
      userName: name,
      password: pass,
    })
      .then((response) => {
        proceedResponse(response.data);
      })
      .catch((error) => {
        console.warn(error?.response);
        try {
          if (typeof error.response.data !== 'object') {
            toast.error(`${error.response.data}`, {
              position: toast.POSITION.BOTTOM_RIGHT,
            });
            return;
          }
        } catch {
          toast.error('Something went wrong', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
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
      setUser(response);
      localStorage.setItem('user', JSON.stringify(response));
      setIsRedirect(true);
    } catch (ex) {
      console.warn(ex);
      toast.error(`${ex.error}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
    }
  };

  return [isRedirect, isLoading, setFlag, setName, setPass, flag];
};

export default useRegister;
