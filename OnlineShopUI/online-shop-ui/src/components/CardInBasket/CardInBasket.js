import React, { useState } from 'react';
import Remove from '../../img/btn-remove.svg';
import removeFromBasketById from '../../routers/Home/Logic/Basket/functions/RemoveFromBasketById';

export default function CardInBasket({
  card,
  handleClickIncreaseBasketCount,
  handleClickDecreaseBasketCount,
  handleClickRemoveFromBasket,
}) {
  const [cardCount, setCardCount] = useState(card.countInBasket);

  const onIncreased = () => {
    handleClickIncreaseBasketCount(card);
    setCardCount(cardCount + 1);
  };

  const onDecreased = () => {
    handleClickDecreaseBasketCount(card);
    if (cardCount === 1) {
      removeFromBasketById(card.id);
      return;
    }
    setCardCount(cardCount - 1);
  };

  return (
    <div className="basket__cartItem">
      <div className="basket__cartItemPhoto"></div>
      <div className="basket__itemInfo">
        <p>{card.name}</p>
        <b>{card.price} usd</b>
      </div>
      <div className="basket__itemCounter">
        <label>
          {cardCount}
          <button style={{ width: '30px' }} onClick={() => onIncreased()}>
            +
          </button>
          <button style={{ width: '30px' }} onClick={() => onDecreased()}>
            -
          </button>
        </label>
      </div>
      <div className="basket__cartRemove">
        <button onClick={() => handleClickRemoveFromBasket(card)}>
          <img src={Remove} alt="Remove" />
        </button>
      </div>
    </div>
  );
}
