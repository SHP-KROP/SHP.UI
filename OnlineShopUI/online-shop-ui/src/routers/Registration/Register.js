import { Link, LinearProgress } from '@mui/material';
import { Navigate } from 'react-router-dom';
import 'react-toastify/dist/ReactToastify.css';
import './Register.scss';
import Feedback from '../../components/FeedbackATop/Feedback';
import useRegister from './Logic/RegisterLogic';
import Error from '../../img/icon-error.png';
import UseValidation from '../../Validation/Validation';

const Register = () => {
  const [isRedirect, isLoading, setFlag, setName, setPass, flag] =
    useRegister();

  const [register, handleSubmit, reset, errors, isValid] = UseValidation();

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
        <Feedback />
        <div className="registration__body">
          <div className="registration__info">
            <div className="registration__header">
              <div className="registration__greeting">
                <p>
                  <strong>Welcome</strong>
                </p>
                <span>Register new account</span>
              </div>
              <div className="registration__backToSign">
                <Link>Sign in</Link>
              </div>
            </div>
            <div className="messages"></div>
            <form
              className="registration__form"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="registration__inputName input">
                <label className="label">Username</label>
                <input
                  type="text"
                  {...register('name', {
                    required: <p>Field Username required for registration</p>,
                    minLength: {
                      value: 3,
                      message: <p>Username must be more than 3 characters</p>,
                    },
                  })}
                />
              </div>
              <div className="registration__error">
                {errors?.name && (
                  <p>
                    <img src={Error} alt="Erorr" />
                    {errors?.name.message || 'Error!'}
                  </p>
                )}
              </div>

              <div className="registration__inputPassword input">
                <label className="label">Password</label>
                <input
                  type="password"
                  {...register('pass', {
                    required: 'Field Password required for registration',
                    pattern: {
                      value:
                        /(?=.*?[#?!@$%^&*-]).{5,30}/,
                    },
                  })}
                />
              </div>

              <div className="registration__error">
                {errors?.pass && (
                  <p>
                    <img src={Error} alt="Erorr" />
                    Password must be longer than 5 characters and contain at
                    least one special character
                  </p>
                )}
              </div>
              {isLoading ? (
                <LinearProgress />
              ) : (
                <input
                  type="submit"
                  className="registration__btn"
                  value="Get Started"
                  disabled={!isValid}
                />
              )}
            </form>

            <div className="registration__exit">
              <Link href="/home">Back to main page</Link>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Register;
