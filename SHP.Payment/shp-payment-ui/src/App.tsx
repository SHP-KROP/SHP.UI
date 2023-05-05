import React from 'react';
import PaymentsForm from './components/PaymentsForm';
import { CheckoutState } from './store/types';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Payment Form</h1>
      </header>
      <main>
        <PaymentsForm
          onSubmit={function (values: CheckoutState): void {
            throw new Error('Function not implemented.');
          }}
        />
      </main>
    </div>
  );
}

export default App;
