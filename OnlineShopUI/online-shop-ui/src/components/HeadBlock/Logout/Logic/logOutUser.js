import IdentityAPI from "../../../../API/IdentityServerAPI";

export default function logOutUser() {
  let token = localStorage.getItem("token");
  if (!token) {
    return;
  }

  localStorage.removeItem("token");

  IdentityAPI.post('/revoke', {}, { headers: { "Authorization": token}})
  return 
}
