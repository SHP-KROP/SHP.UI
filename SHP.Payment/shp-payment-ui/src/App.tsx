import React from 'react';
import PaymentsForm from './components/PaymentsForm';
import { useDispatch } from 'react-redux';
import { checkout } from './store/actions';
import { CheckoutState } from './store/types';
function App() {
  const dispatch = useDispatch();
  const handleCheckout = (values: CheckoutState) => {
    dispatch(checkout(values));
  };
  return (
    <div className="App">
      <header className="App-header">
        <h1>Payment Form</h1>
      </header>
      <main>
        <PaymentsForm onSubmit={handleCheckout} />
      </main>
    </div>
  );
}

export default App;
