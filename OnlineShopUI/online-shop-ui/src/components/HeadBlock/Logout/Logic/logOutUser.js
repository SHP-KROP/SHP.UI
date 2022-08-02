import IdentityAPI from "../../../../API/IdentityServerAPI";

export default function logOutUser() {
  const token = localStorage.getItem("token");
  if (!token) {
    return;
  }

  localStorage.removeItem("token");

  // TODO: Check UI logic for refresh tokens
  /*
  const refresh = localStorage.getItem("refresh");

  if (!refresh) {
    return;
  }
  */

  // TODO: Finish with revoking tokens
  IdentityAPI.post('/revoke', {}, { headers: { "Authorization": token}})
  return 
}
