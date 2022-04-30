import { useForm } from 'react-hook-form';

const UseValidation = () => {
  const {
    register,
    formState: { errors, isValid },
    handleSubmit,
    reset,
  } = useForm({
    mode: 'onChange',
  });

  return [register, handleSubmit, reset, errors, isValid];
};

export default UseValidation;
