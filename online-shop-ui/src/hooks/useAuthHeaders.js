import useAuth from './useAuth';
import IdentityAPI from '../API/IdentityServerAPI'

const tryRefreshingToken = () => {
  if (localStorage.jwt && localStorage.refreshToken) {
    const token = localStorage.jwt;
    const refreshToken = localStorage.refreshToken;
    const credentials = { token, refreshToken };

    IdentityAPI.post('/user/refresh-token', credentials)
      .then((response) => {
        if (response.data.success) {
          localStorage.setItem('jwt', response.data.token);
          localStorage.setItem('refreshToken', response.data.refreshToken);

          const user = JSON.parse(localStorage.user);
          localStorage.setItem('user', JSON.stringify({...user, token: localStorage.jwt, refreshToken: localStorage.refreshToken}));

          console.log('TOKEN REFRESHED SUCCESSFULLY');
        }
      })
      .catch((error) => {
        console.log(error.response.data['0']);
      });
  }
}

export default function useAuthHeaders() {
  const { user } = useAuth();
  tryRefreshingToken();

  return { headers: { Authorization: user ? `Bearer ${user.token}` : null } };
}
