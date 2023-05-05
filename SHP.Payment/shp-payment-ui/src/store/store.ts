import { createStore, applyMiddleware } from 'redux';
import thunkMiddleware from 'redux-thunk';
import { composeWithDevTools } from 'redux-devtools-extension';
import { checkoutReducer } from './reducers';

const middlewares = [thunkMiddleware];

const store = createStore(
  checkoutReducer,
  composeWithDevTools(applyMiddleware(...middlewares))
);

export default store;
