import { GoogleLogin } from "react-google-login";
import IdentityAPI from "../../API/IdentityServerAPI";
import useAuth from "../../hooks/useAuth";
import { toast } from 'react-toastify';

const clientId =
  "930917656503-gqbc3li3obv7munodub7le5gon26s1r9.apps.googleusercontent.com";
toast.configure();

export default function LoginGoogle() {
  const { user, setUser } = useAuth();
  const onSuccess = (googleResponse) => {
    const token = googleResponse.tokenId;
    console.log(googleResponse)
    IdentityAPI.post(`/user/google-auth?tokenId=${token}`).then((response) => {
      toast.success(`Welcome ${googleResponse.profileObj.name}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
      setUser(response.data);
      localStorage.setItem('user', JSON.stringify(response.data));
      localStorage.setItem('jwt', response.data.token);
      localStorage.setItem('refreshToken', response.data.refreshToken);
    }).catch(error => {
      toast.error('Internal server error during Google authorization process', {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
    });
  };

  const onFailure = (res) => {
    console.log("Login failed", res);
  };
  return (
    <div>
      <GoogleLogin
        clientId={clientId}
        buttonText="Login"
        onSuccess={onSuccess}
        onFailure={onFailure}
        cookiePolicy={"single_host_origin"}
        isSignedIn={true}
      />
    </div>
  );
}
