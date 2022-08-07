import { GoogleLogout } from "react-google-login";

const clientId =
  "930917656503-gqbc3li3obv7munodub7le5gon26s1r9.apps.googleusercontent.com";

export default function LogoutGoogle() {
  const onSuccess = () => {
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
