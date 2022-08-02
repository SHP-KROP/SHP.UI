import LogoutIcon from "@mui/icons-material/Logout";
import logOutUser from "./Logic/logOutUser"

export default function Logout({ logOut=logOutUser }) {
  return (
    <button style={{ border: "none", backgroundColor: "white" }} onClick={() => logOut()}>
      <LogoutIcon />
    </button>
  );
}
