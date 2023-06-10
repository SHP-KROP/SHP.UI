import { useState } from 'react';
import {
  Link,
  LinearProgress,
  TextField,
  Button,
  RadioGroup,
  FormControlLabel,
  Radio,
} from '@mui/material';
import { Navigate } from 'react-router-dom';
import 'react-toastify/dist/ReactToastify.css';
import useRegister from './Logic/RegisterLogic';
import Error from '../../img/icon-error.png';
import UseValidation from '../../Validation/Validation';
import './Register.scss';

const Register = () => {
  const [isRedirect, isLoading, setFlag, setName, setPass, flag] =
    useRegister();

  const [register, handleSubmit, reset, errors, isValid] = UseValidation();
  const [role, setRole] = useState('Buyer'); // Default role is "Buyer"

  const onSubmit = (data) => {
    setName(data?.name);
    setPass(data?.pass);
    setFlag(!flag);
    reset();
  };

  return (
    <>
      {isRedirect ? <Navigate push to="/home" /> : null}
      <div className="registration">
        <div className="registration__body">
          <div className="registration__info">
            <div className="registration__header"></div>
            <div className="messages"></div>
            <form
              className="registration__form"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="registration__greeting">
                <p>
                  <strong>Welcome</strong>
                </p>
                <span>Register new account</span>
              </div>
              <div className="registration__inputName input">
                <TextField
                  label="Username"
                  type="text"
                  {...register('name', {
                    required: 'Field Username required for registration',
                    minLength: {
                      value: 3,
                      message: 'Username must be more than 3 characters',
                    },
                  })}
                  error={!!errors?.name}
                  helperText={errors?.name?.message || ''}
                  fullWidth
                />
              </div>
              <div className="registration__error">
                {errors?.name && (
                  <p>
                    <img src={Error} alt="Error" />
                    {errors?.name.message || 'Error!'}
                  </p>
                )}
              </div>

              <div className="registration__inputPassword input">
                <TextField
                  label="Password"
                  type="password"
                  {...register('pass', {
                    required: 'Field Password required for registration',
                    pattern: {
                      value:
                        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&].{6,30}$/,
                      message:
                        'Password must be between 5 and 30 characters and contain at least one special character, digit, uppercase and lowercase letter',
                    },
                  })}
                  error={!!errors?.pass}
                  helperText={errors?.pass?.message || ''}
                  fullWidth
                />
              </div>

              <div className="registration__error">
                {errors?.pass && (
                  <p>
                    <img src={Error} alt="Error" />
                    Password must be between 5 and 30 characters and contain at
                    least one special character, digit, uppercase and lowercase
                    letter
                  </p>
                )}
              </div>

              <div className="registration__role">
                <RadioGroup
                  row
                  aria-label="role"
                  name="role"
                  value={role}
                  onChange={(e) => setRole(e.target.value)}
                >
                  <FormControlLabel
                    value="Buyer"
                    control={<Radio />}
                    label="Buyer"
                  />
                  <FormControlLabel
                    value="Seller"
                    control={<Radio />}
                    label="Seller"
                  />
                </RadioGroup>
              </div>

              {isLoading ? (
                <LinearProgress />
              ) : (
                <Button
                  variant="contained"
                  type="submit"
                  className="registration__btn"
                  disabled={!isValid}
                >
                  Get Started
                </Button>
              )}
            </form>

            <div className="registration__exit">
              <Button variant="contained">
                <Link
                  href="/"
                  style={{
                    color: 'white',
                    textDecoration: 'none',
                  }}
                >
                  Back to main page
                </Link>
              </Button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Register;
