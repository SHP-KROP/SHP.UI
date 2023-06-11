import { GoogleLogout } from "react-google-login";
import IdentityAPI from '../../API/IdentityServerAPI';
import useAuth from '../../hooks/useAuth';
import useAuthHeaders from '../../hooks/useAuthHeaders';
import { toast } from 'react-toastify';

const clientId =
  "930917656503-gqbc3li3obv7munodub7le5gon26s1r9.apps.googleusercontent.com";

export default function LogoutGoogle() {
  const { setUser } = useAuth();
  const authHeaders = useAuthHeaders();

  const onSuccess = () => {
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

    console.log("Logout success!");
  };

  return (
    <div>
      <GoogleLogout
        clientId={clientId}
        buttonText="Logout"
        onSuccess={onSuccess}
      />
    </div>
  );
}
