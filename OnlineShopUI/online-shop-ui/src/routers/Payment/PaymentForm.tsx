import { FC } from 'react';
import { Formik, Form, Field, FieldInputProps } from 'formik';
import * as Yup from 'yup';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import {
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Button,
} from '@material-ui/core';
import React from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    formControl: {
      marginTop: theme.spacing(2),
      minWidth: 120,
    },
    submitButton: {
      marginTop: theme.spacing(2),
    },
  })
);
const user = JSON.parse(localStorage.getItem('user') || '{}');
const { token } = user;
const config = {
  headers: {
    Authorization: `Bearer ${token}`,
  },
};
const basket = localStorage.getItem('basket');
const PaymentsForm: FC = () => {
  const classes = useStyles();

  return (
    <>
      <h1 style={{ textAlign: 'center', margin: '1%' }}>
        Payment Details Form
      </h1>
      <Formik
        initialValues={{
          creditCard: {
            number: '',
            expirationMonth: '',
            expirationYear: '',
            cvc: '',
          },
          productsInBasket: basket
            ? JSON.parse(basket).map((product: any) => ({
                productId: product.id,
                amount: product.countInBasket,
              }))
            : [],
        }}
        validationSchema={Yup.object().shape({
          creditCard: Yup.object().shape({
            number: Yup.string()
              .required('Credit card number is required')
              .matches(/^\d{16}$/, 'Invalid credit card number'),
            expirationMonth: Yup.string().required(
              'Expiration month is required'
            ),
            expirationYear: Yup.string().required(
              'Expiration year is required'
            ),
            cvc: Yup.string()
              .required('CVC is required')
              .matches(/^\d{3}$/, 'Invalid CVC'),
          }),
        })}
        onSubmit={async (values, { setSubmitting }) => {
          try {
            setSubmitting(true);
            const res = await axios.post(
              'https://localhost:44318/api/Checkout',
              values,
              config
            );
            if (res.status === 200) {
              toast.success('Payment successful');
              localStorage.removeItem('basket');
            } else if (res.status === 400) {
              toast.error('Something went wrong');
            }
          } catch (error) {
            toast.error('Something went wrong');
          } finally {
            setSubmitting(false);
          }
        }}
      >
        {({ errors, touched, isSubmitting }) => (
          <Form style={{ width: '50%', margin: '0 auto' }}>
            <Field name="creditCard.number">
              {({ field }: { field: FieldInputProps<string> }) => (
                <TextField
                  {...field}
                  label="Credit card number"
                  variant="outlined"
                  fullWidth
                  error={
                    touched.creditCard?.number &&
                    Boolean(errors.creditCard?.number)
                  }
                  helperText={
                    touched.creditCard?.number && errors.creditCard?.number
                  }
                />
              )}
            </Field>
            <div
              className="expiration"
              style={{
                display: 'flex',
                justifyContent: 'space-evenly',
                gap: '5%',
              }}
            >
              <Field name="creditCard.expirationMonth">
                {({ field }: { field: FieldInputProps<string> }) => (
                  <FormControl
                    variant="outlined"
                    className={classes.formControl}
                    style={{ width: '50%' }}
                  >
                    <InputLabel>Expiration month</InputLabel>
                    <Select
                      {...field}
                      label="Expiration month"
                      error={
                        touched.creditCard?.expirationMonth &&
                        Boolean(errors.creditCard?.expirationMonth)
                      }
                    >
                      <MenuItem value="01">January</MenuItem>
                      <MenuItem value="02">February</MenuItem>
                      <MenuItem value="03">March</MenuItem>
                      <MenuItem value="04">April</MenuItem>
                      <MenuItem value="05">May</MenuItem>
                      <MenuItem value="06">June</MenuItem>
                      <MenuItem value="07">July</MenuItem>
                      <MenuItem value="08">August</MenuItem>
                      <MenuItem value="09">September</MenuItem>
                      <MenuItem value="10">October</MenuItem>
                      <MenuItem value="11">November</MenuItem>
                      <MenuItem value="12">December</MenuItem>
                    </Select>
                  </FormControl>
                )}
              </Field>
              <Field name="creditCard.expirationYear">
                {({ field }: { field: FieldInputProps<string> }) => (
                  <FormControl
                    variant="outlined"
                    className={classes.formControl}
                    style={{ width: '50%' }}
                  >
                    <InputLabel>Expiration year</InputLabel>
                    <Select
                      {...field}
                      label="Expiration year"
                      error={
                        touched.creditCard?.expirationYear &&
                        Boolean(errors.creditCard?.expirationYear)
                      }
                    >
                      <MenuItem value="2022">2022</MenuItem>
                      <MenuItem value="2023">2023</MenuItem>
                      <MenuItem value="2024">2024</MenuItem>
                      <MenuItem value="2025">2025</MenuItem>
                      <MenuItem value="2026">2026</MenuItem>
                      <MenuItem value="2027">2027</MenuItem>
                      <MenuItem value="2028">2028</MenuItem>
                      <MenuItem value="2029">2029</MenuItem>
                      <MenuItem value="2030">2030</MenuItem>
                    </Select>
                  </FormControl>
                )}
              </Field>
            </div>
            <Field name="creditCard.cvc">
              {({ field }: { field: FieldInputProps<string> }) => (
                <TextField
                  {...field}
                  label="CVC"
                  variant="outlined"
                  fullWidth
                  error={
                    touched.creditCard?.cvc && Boolean(errors.creditCard?.cvc)
                  }
                  helperText={touched.creditCard?.cvc && errors.creditCard?.cvc}
                  style={{ marginTop: '5%' }}
                />
              )}
            </Field>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              className={classes.submitButton}
              disabled={isSubmitting}
              fullWidth
            >
              Submit
            </Button>
            <Button
              type="submit"
              variant="contained"
              color="primary"
              className={classes.submitButton}
            >
              <Link to="/" style={{ color: 'white' }}>
                Back to home
              </Link>
            </Button>
          </Form>
        )}
      </Formik>
    </>
  );
};

export default PaymentsForm;
