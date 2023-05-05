import axios from 'axios';
import { Dispatch } from 'redux';
import {
  CHECKOUT_REQUEST,
  CHECKOUT_SUCCESS,
  CHECKOUT_FAILURE,
  CheckoutActionTypes,
  CheckoutState,
} from './types';
import { Action } from 'redux';

export const checkoutRequest = (): CheckoutActionTypes => ({
  type: CHECKOUT_REQUEST,
});

export const checkoutSuccess = (): CheckoutActionTypes => ({
  type: CHECKOUT_SUCCESS,
});

export const checkoutFailure = (error: string): CheckoutActionTypes => ({
  type: CHECKOUT_FAILURE,
  error,
});

export const checkout =
  (checkoutData: CheckoutState) => async (dispatch: Dispatch<Action>) => {
    dispatch({ type: CHECKOUT_REQUEST });

    try {
      const response = await axios.post('/Checkout', checkoutData);
      dispatch({ type: CHECKOUT_SUCCESS, payload: response.data });
    } catch (error) {
      const errorMessage = (error as Error).message;
      dispatch({ type: CHECKOUT_FAILURE, payload: errorMessage });
    }
  };
