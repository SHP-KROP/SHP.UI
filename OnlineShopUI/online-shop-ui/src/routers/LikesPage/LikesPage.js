import React from 'react';
import useLikes from './hooks/useLikes';
import './LikesPage.scss';
import useBasketFilling from '../Home/Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from '../Home/Logic/Basket/hooks/useBasketHandlers';
import ProductCard from '../../components/Card/ProductCard';

export default function LikesPage() {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [basket, setBasket] = useBasketFilling();
  const [handleClickAddInBasket] = useBasketHandlers({ basket, setBasket });

  return (
    <div className="wrapper">
      <div className="likespage">
        {isLoading ? (
          <h1>Loading</h1>
        ) : (
          <>
            {likedProducts &&
              likedProducts.map((product) => (
                <div className="likeproduct">
                  <ProductCard
                    card={product}
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
