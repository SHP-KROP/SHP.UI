import React, { useState } from 'react';
import removeFromBasketById from '../../routers/Home/Logic/Basket/functions/RemoveFromBasketById';
import AddIcon from '@mui/icons-material/Add';
import RemoveIcon from '@mui/icons-material/Remove';
import CancelIcon from '@mui/icons-material/Cancel';

export default function CardInBasket({
  card,
  handleClickIncreaseBasketCount,
  handleClickDecreaseBasketCount,
  handleClickRemoveFromBasket,
  setTotalChanged,
}) {
  const [cardCount, setCardCount] = useState(card.countInBasket);

  const onIncreased = () => {
    handleClickIncreaseBasketCount(card);
    setCardCount((cardCount) => cardCount + 1);
    setTotalChanged((prev) => !prev);
  };

  const onDecreased = () => {
    handleClickDecreaseBasketCount(card);
    if (cardCount === 1) {
      removeFromBasketById(card.id);
      setTotalChanged((prev) => !prev);

      return;
    }
    setCardCount(cardCount - 1);
    setTotalChanged((prev) => !prev);
  };

  return (
    <div className="basket__cartItem">
      <div className="basket__cartItemPhoto"></div>
      <div className="basket__itemInfo">
        <p>{card.name}</p>
        <b>{card.price}$ {(cardCount > 1) && `(${cardCount * card.price}$)`} </b>
      </div>
      <div>
        <div
          style={{ display: 'flex', alignItems: 'center', marginRight: '50%' }}
          className="basket__itemCounter"
        >
          
          <RemoveIcon onClick={() => onDecreased()} />
          <div style={{ margin: '10%', fontWeight: 'bold' }}>
            {card.countInBasket}
          </div>
          <AddIcon onClick={() => onIncreased()} />
          <CancelIcon
            sx={{ fontSize: '35px' }}
            onClick={() => handleClickRemoveFromBasket(card)}
          />
        </div>
      </div>
    </div>
  );
}
