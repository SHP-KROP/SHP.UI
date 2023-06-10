import {
  CHECKOUT_REQUEST,
  CHECKOUT_SUCCESS,
  CHECKOUT_FAILURE,
  CheckoutActionTypes,
  CheckoutState,
} from './types';

const initialState: CheckoutState = {
  creditCard: {
    number: '',
    expirationMonth: '',
    expirationYear: '',
    cvc: '',
  },
  productsInBasket: [],
};

export const checkoutReducer = (
  state = initialState,
  action: CheckoutActionTypes
): CheckoutState => {
  switch (action.type) {
    case CHECKOUT_REQUEST:
      return state;
    case CHECKOUT_SUCCESS:
      return initialState;
    case CHECKOUT_FAILURE:
      return state;
    default:
      return state;
  }
};
