import React, { useState } from 'react';
import './Basket.scss';
import CloseIcon from '@mui/icons-material/Close';
import CardInBasket from '../CardInBasket/CardInBasket';

const Basket = ({
  onClose,
  opened,
  basket,
  handleClickIncreaseBasketCount,
  handleClickDecreaseBasketCount,
  handleClickRemoveFromBasket,
}) => {
  const [totalChanged, setTotalChanged] = useState(false);

  function getTotal() {
    return basket.map(card => card.countInBasket * card.price).reduce((x, y) => x + y, 0)
  }

  return (
    <div className={opened ? 'overlay' : 'overlayUnVisible'}>
      <div className="basket">
        <div className="basket__title">
          <h2>
            Shopping basket
            <button className="basket__cartClose" onClick={onClose}>
              <CloseIcon style={{ color: 'red' }}></CloseIcon>
            </button>
          </h2>
        </div>
        <div className="basket__items">
          {basket.map((card) => (
            <CardInBasket
              card={card}
              handleClickIncreaseBasketCount={handleClickIncreaseBasketCount}
              handleClickDecreaseBasketCount={handleClickDecreaseBasketCount}
              handleClickRemoveFromBasket={handleClickRemoveFromBasket}
              setTotalChanged={setTotalChanged}
            />
          ))}
        </div>
        <div className="basket__price">
          <span>Subtotal</span>
          <p className="basket__price">{getTotal()} USD</p>
        </div>
        <hr />

        <div className="basket__btn">
          <span>Continue shopping</span>
          <button>Go to Checkout</button>
        </div>
      </div>
    </div>
  );
};

export default Basket;
