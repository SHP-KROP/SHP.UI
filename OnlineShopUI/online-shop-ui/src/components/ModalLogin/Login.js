import { useContext } from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import CloseIcon from '@mui/icons-material/Close';
import './Login.scss';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import User from '../../img/icon-user.png';
import { Link } from 'react-router-dom';
import useLogin from './Logic/LoginLogic';
import UseHandlers from '../../Helper/Handlers';
import useAuth from '../../Helper/hook/useAuth';
export default function Login() {
  const [setUsername, setPassword, flag, setFlag, pass, name] = useLogin();

  const [handleModalClose, handleModalOpen, isOpen] = UseHandlers();
  const { user, setUser } = useContext(useAuth);
  return (
    <div>
      <a href="#">
        <img
          src={User}
          alt="user"
          className="log-btn"
          onClick={handleModalOpen}
        />
      </a>
      <Modal
        className="log-modal"
        open={isOpen}
        onClose={handleModalClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box>
          <div className="login">
            <div className="login__type">
              <p>
                <strong>Log in</strong>
              </p>
              <CloseIcon
                className="login__btnclose"
                onClick={handleModalClose}
              ></CloseIcon>
            </div>

            <form className="login__date">
              <div className="login__email">
                <label>Username</label>
                <input
                  type="text"
                  onChange={(e) => {
                    setUsername(e.target.value);
                  }}
                />
              </div>

              <div className="login__pass">
                <label>Password</label>
                <input
                  type="password"
                  onChange={(e) => {
                    setPassword(e.target.value);
                  }}
                />
              </div>
              <a href="#">Forgot password?</a>
              <FormGroup>
                <FormControlLabel
                  className="login__check"
                  control={<Checkbox defaultChecked />}
                  label="Remember me"
                />
              </FormGroup>
            </form>
            <div className="login__actions">
              <div className="login__sign">
                <input
                  type="submit"
                  className="login__sign-btn"
                  onClick={() => setFlag(!flag)}
                  value="Log in"
                />
              </div>
              <div className="login__register">
                <Link to="/register" onClick={handleModalClose}>
                  Register
                </Link>
              </div>
            </div>
          </div>
        </Box>
      </Modal>
    </div>
  );
}
