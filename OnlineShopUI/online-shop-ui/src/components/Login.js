import React from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import CloseIcon from '@mui/icons-material/Close';
import './Login.scss';
import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import User from '../img/icon-user.png';

export default function Login() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <div>
      <a href="#">
        <img src={User} alt="user" className="log-btn" onClick={handleOpen} />
      </a>
      <Modal
        className="log-modal"
        open={open}
        onClose={handleClose}
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
                onClick={handleClose}
              ></CloseIcon>
            </div>
            <div className="login__date">
              <div className="login__email">
                <label>Эл. почта или телефон</label>
                <input type="text" placeholder="example@gmail.com" />
              </div>
              <div className="login__pass">
                <label>Пароль</label>
                {/* <i>
                <LockIcon className="lock"></LockIcon>
              </i> */}
                <input type="text" placeholder="1d$5D03" />
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
                <button className="login__sign-btn">Войти</button>
              </div>
              <div className="login__registr">
                <a href="#">Зарегистрироваться</a>
              </div>
            </div>
          </div>
        </Box>
      </Modal>
    </div>
  );
}
