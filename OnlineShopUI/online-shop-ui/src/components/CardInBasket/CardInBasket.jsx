import React, { useState } from 'react';
import RemoveIcon from '@mui/icons-material/Remove';
import AddIcon from '@mui/icons-material/Add';
import CancelIcon from '@mui/icons-material/Cancel';

export default function CardInBasket({
  card,
  handleClickIncreaseBasketCount,
  handleClickDecreaseBasketCount,
  handleRemoveFromBasket,
  setTotalChanged,
}) {
  const [cardCount, setCardCount] = useState(card.countInBasket);

  const onIncreased = () => {
    if (cardCount < card.amount) {
      handleClickIncreaseBasketCount(card);
      setCardCount(cardCount + 1);
      setTotalChanged((prev) => !prev);
    }
  };

  const onDecreased = () => {
    if (cardCount > 1) {
      handleClickDecreaseBasketCount(card);
      setCardCount(cardCount - 1);
      setTotalChanged((prev) => !prev);
    } else {
      handleRemoveFromBasket(card);
      setTotalChanged((prev) => !prev);
    }
  };

  return (
    <div className="basket__cartItem">
      <div className="basket__cartItemPhoto">
        <img src={card.photoUrl} alt="logo" />
      </div>
      <div className="basket__itemInfo">
        <p>{card.name}</p>
        <b>
          {card.price}$ {cardCount > 1 && `(${cardCount * card.price}$)`}{' '}
        </b>
      </div>
      <div>
        <div
          style={{ display: 'flex', alignItems: 'center', marginRight: '50%' }}
          className="basket__itemCounter"
        >
          <RemoveIcon onClick={onDecreased} />
          <div style={{ margin: '10%', fontWeight: 'bold' }}>{cardCount}</div>
          <AddIcon onClick={onIncreased} />
          <CancelIcon
            sx={{ fontSize: '35px' }}
            onClick={() => handleRemoveFromBasket(card)}
          />
        </div>
      </div>
    </div>
  );
}
