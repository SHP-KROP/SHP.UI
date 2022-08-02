import React, { useState } from 'react';
import useLikes from './hooks/useLikes';
import './LikesPage.scss';
import Feedback from '../../components/FeedbackATop/Feedback';
import Basket from '../../components/Basket/Basket';
import useBasketFilling from '../Home/Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from '../Home/Logic/Basket/hooks/useBasketHandlers';
import HeadBlock from '../../components/HeadBlock/HeadBlock';
import ProductCard from '../../components/Card/ProductCard';

export default function LikesPage() {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [isBasketOpen, setBasketOpen] = useState(() => false);
  const [basket, setBasket] = useBasketFilling();
  const [
    handleClickAddInBasket,
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ] = useBasketHandlers({ basket, setBasket });

  return (
    <div className="wrapper">
      <Feedback />
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
      <div className="likespage">
        {isLoading ? (
          <h1>Loading</h1>
        ) : (
          // likedProducts && likedProducts.map((product) => <p>{JSON.stringify(product)}</p>)
          <>
            {likedProducts &&
              likedProducts.map((product) => (
                <div className="likeproduct">
                  <ProductCard
                    name={product.name}
                    description={product.description}
                    price={product.price}
                    handleClick={handleClickAddInBasket}
                    basket={basket}
                  />
                </div>
              ))}
            {likedProducts &&
              likedProducts.map((product) => (
                <div className="likeproduct">
                  <ProductCard
                    name={product.name}
                    description={product.description}
                    price={product.price}
                    handleClick={handleClickAddInBasket}
                    basket={basket}
                  />
                </div>
              ))}
          </>
        )}
      </div>
    </div>
  );
}
