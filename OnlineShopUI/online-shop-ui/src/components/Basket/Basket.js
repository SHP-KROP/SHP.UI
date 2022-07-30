import React, { useEffect, useState } from 'react';
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
  const [totalPrice, setTotalPrice] = useState(0);

  function calculateTotal() {
    const sum = basket.map(x => x.price * x.countInBasket).reduce((x, y) => x + y);
    setTotalPrice(sum);
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
              calculateTotal={calculateTotal}
            />
          ))}
        </div>
        <div className="basket__price">
          <span>Subtotal</span>
          <p className="basket__price">{totalPrice} USD</p>
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
