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
import Handlers from '../../Helper/Handlers';

export default function Login() {
  const [setUsername, setPassword, flag, setFlag] = useLogin();

  const [handleLogInModalOpen, handleLogInModalClose, isOpen] = Handlers();

  return (
    <div>
      <a href="#">
        <img
          src={User}
          alt="user"
          className="log-btn"
          onClick={handleLogInModalOpen}
        />
      </a>
      <Modal
        className="log-modal"
        open={isOpen}
        onClose={handleLogInModalClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box>
          <div className="login">
            <div className="login__type">
              <p>
                <strong>Вход</strong>
              </p>
              <CloseIcon
                className="login__btnclose"
                onClick={handleLogInModalClose}
              ></CloseIcon>
            </div>
            <div className="login__date">
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
                <label>Пароль</label>
                <input
                  type="password"
                  onChange={(e) => {
                    setPassword(e.target.value);
                  }}
                />
              </div>
              <a href="#">Забыли пароль?</a>
              <FormGroup>
                <FormControlLabel
                  className="login__check"
                  control={<Checkbox defaultChecked />}
                  label="Запомнить меня"
                />
              </FormGroup>
            </div>
            <div className="login__actions">
              <div className="login__sign">
                <button
                  className="login__sign-btn"
                  onClick={() => setFlag(!flag)}
                >
                  Войти
                </button>
              </div>
              <div className="login__register">
                <Link to="/register" onClick={handleLogInModalClose}>
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
