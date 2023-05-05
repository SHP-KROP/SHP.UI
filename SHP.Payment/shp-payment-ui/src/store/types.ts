export interface Product {
  productId: number;
  amount: number;
}

export interface CreditCard {
  number: string;
  expirationMonth: string;
  expirationYear: string;
  cvc: string;
}

export interface CheckoutState {
  creditCard: CreditCard;
  productsInBasket: Product[];
}

export const CHECKOUT_REQUEST = 'CHECKOUT_REQUEST';
export const CHECKOUT_SUCCESS = 'CHECKOUT_SUCCESS';
export const CHECKOUT_FAILURE = 'CHECKOUT_FAILURE';

interface CheckoutRequestAction {
  type: typeof CHECKOUT_REQUEST;
}

interface CheckoutSuccessAction {
  type: typeof CHECKOUT_SUCCESS;
}

interface CheckoutFailureAction {
  type: typeof CHECKOUT_FAILURE;
  error: string;
}

export type CheckoutActionTypes =
  | CheckoutRequestAction
  | CheckoutSuccessAction
  | CheckoutFailureAction;
