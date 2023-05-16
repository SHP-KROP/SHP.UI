import { Dispatch } from 'redux';
import {
  CHECKOUT_REQUEST,
  CHECKOUT_SUCCESS,
  CHECKOUT_FAILURE,
  CheckoutActionTypes,
  CheckoutState,
} from './types';
import CoreAPI from '../../../API/CoreAPI';
import axios from 'axios';
export const checkoutRequest = (): CheckoutActionTypes => ({
  type: CHECKOUT_REQUEST,
});

export const checkoutSuccess = (data: any): CheckoutActionTypes => ({
  type: CHECKOUT_SUCCESS,
  payload: data,
});

export const checkoutFailure = (error: string): CheckoutActionTypes => ({
  type: CHECKOUT_FAILURE,
  error,
});

export const checkout = (checkoutData: CheckoutState) => {
  return async (dispatch: Dispatch<CheckoutActionTypes>) => {
    dispatch(checkoutRequest());

    try {
      const user = JSON.parse(localStorage.getItem('user') || '{}');
      const { token } = user;
      const config = {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      };
      const response = await axios.post(
        'https://localhost:44318/api/Checkout',
        checkoutData,
        config
      );
      console.log(response.data);
      dispatch(checkoutSuccess(response.data));
    } catch (error) {
      const errorMessage = (error as Error).message;
      dispatch(checkoutFailure(errorMessage));
    }
  };
};
