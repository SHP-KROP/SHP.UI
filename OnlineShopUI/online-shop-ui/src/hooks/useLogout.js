import useAuth from './useAuth';
import IdentityAPI from '../API/IdentityServerAPI';
import useAuthHeaders from './useAuthHeaders';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

toast.configure();

export default function useLogout() {
  const { setUser } = useAuth();
  const authHeaders = useAuthHeaders();

  function logOut() {
    setUser(null);
    localStorage.removeItem('user');
    localStorage.removeItem('jwt');
    localStorage.removeItem('refreshToken');

    IdentityAPI.post('/user/revoke-token', {}, authHeaders)
      .then()
      .catch((error) => {
        try {
          if (typeof error.response.data !== 'object') {
            toast.error(`${error.response.data}`, {
              position: toast.POSITION.BOTTOM_RIGHT,
            });
            return;
          }
        } catch {
          toast.error('Something went wrong during unlogging process', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      });
  }
  return logOut;
}
