import React, { useState } from 'react';
import Feedback from '../FeedbackATop/Feedback';
import Basket from '../Basket/Basket';
import HeadBlock from '../HeadBlock/HeadBlock';
import useBasketFilling from '../../routers/Home/Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from '../../routers/Home/Logic/Basket/hooks/useBasketHandlers';

export default function Header() {
  const [isBasketOpen, setBasketOpen] = useState(() => false);
  const [basket, setBasket] = useBasketFilling();
  const [
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ] = useBasketHandlers({ basket, setBasket });
  return (
    <>
      <Basket
        onClose={() => setBasketOpen(false)}
        opened={isBasketOpen}
        basket={basket}
        handleClickIncreaseBasketCount={handleClickIncreaseBasketCount}
        handleClickDecreaseBasketCount={handleClickDecreaseBasketCount}
        handleClickRemoveFromBasket={handleClickRemoveFromBasket}
      />
      <HeadBlock
        productsInBasketCount={basket.length}
        basketOpen={isBasketOpen}
        onClickCart={() => setBasketOpen(true)}
      />
    </>
  );
}
