import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';
import { UserContext } from './Contexts/UserContext';
import useAuthProvider from './hooks/useAuthProvider';
import CrudSeller from './routers/Seller/CRUD/CrudSeller';
import ProductInfo from './routers/Product/ProductInfo';
import PaymentForm from './routers/Payment/PaymentForm.tsx';
import HeadBlock from './components/HeadBlock/HeadBlock';
import Basket from './components/Basket/Basket';
import useBasketFilling from './routers/Home/Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from './routers/Home/Logic/Basket/hooks/useBasketHandlers';
import './App.scss';

function App() {
  const userProvider = useAuthProvider();
  const [isBasketOpen, setBasketOpen] = useState(false);

  const [basket, setBasket] = useBasketFilling();
  const [
    handleClickAddInBasket,
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ] = useBasketHandlers({ basket, setBasket });
  console.log('APP:', basket);
  const productsInBasketCount = basket.reduce(
    (count, card) => count + card.countInBasket,
    0
  );

  const handleRemoveFromBasket = (cardId) => {
    handleClickRemoveFromBasket(cardId);
  };

  return (
    <>
      <UserContext.Provider value={userProvider}>
        <Basket
          onClose={() => setBasketOpen(false)}
          opened={isBasketOpen}
          basket={basket}
          handleRemoveFromBasket={handleRemoveFromBasket}
          handleClickIncreaseBasketCount={handleClickIncreaseBasketCount}
          handleClickDecreaseBasketCount={handleClickDecreaseBasketCount}
        />

        <HeadBlock
          productsInBasketCount={productsInBasketCount}
          basketOpen={isBasketOpen}
          onClickCart={() => setBasketOpen(true)}
          basket={basket}
        />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/register" element={<Register />} />
          <Route path="/likes" element={<LikesPage />} />
          <Route path="/seller" element={<CrudSeller />} />
          <Route path="/product/:productName" element={<ProductInfo />} />
          <Route path="/payment" element={<PaymentForm />} />
        </Routes>
      </UserContext.Provider>
    </>
  );
}

export default App;
