import React from 'react';
import Remove from '../../img/btn-remove.svg';
import removeFromBasketById from '../../routers/Home/Logic/Basket/RemovedFromBasket';

export default function CardInBasket({ cardName, cardPrice, cardId }) {
  return (
    <div className="basket__cartItem">
      <div className="basket__cartItemPhoto"></div>
      <div className="basket__itemInfo">
        <p>{cardName}</p>
        <b>{cardPrice} usd</b>
      </div>
      <div className="basket__cartRemove">
        <button onClick={() => removeFromBasketById(cardId)}>
          <img src={Remove} alt="Remove" />
        </button>
      </div>
    </div>
  );
}
