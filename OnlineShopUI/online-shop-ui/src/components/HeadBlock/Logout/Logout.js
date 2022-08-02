import LogoutIcon from "@mui/icons-material/Logout";
import logOutUser from "./Logic/logOutUser";
import useAuthMock from "../../mock/useAuthMock";

export default function Logout() {
  function onLogOut() {
    logOutUser();
  }

  // TODO: Provide useAuth hook here
  const user = useAuthMock();

  return (
    <>
      {user && <button
        style={{ border: "none", backgroundColor: "white" }}
        onClick={() => onLogOut()}
      >
        <LogoutIcon />
      </button>}
    </>
  );
}
