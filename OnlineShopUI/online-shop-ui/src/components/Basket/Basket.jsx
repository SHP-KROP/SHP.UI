import React from 'react';
import './Basket.scss';
import CloseIcon from '@mui/icons-material/Close';
import CardInBasket from '../CardInBasket/CardInBasket';
import { Link } from 'react-router-dom';

const Basket = ({
  onClose,
  opened,
  basket,
  handleClickAddInBasket,
  handleClickIncreaseBasketCount,
  handleClickDecreaseBasketCount,
  handleRemoveFromBasket,
}) => {
  function getTotal() {
    return basket
      .map((card) => card.countInBasket * card.price)
      .reduce((x, y) => x + y, 0);
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
              key={card.id}
              card={card}
              handleClickIncreaseBasketCount={handleClickIncreaseBasketCount}
              handleClickDecreaseBasketCount={handleClickDecreaseBasketCount}
              handleRemoveFromBasket={handleRemoveFromBasket}
            />
          ))}
        </div>
        <div className="basket__price">
          <span>Total price:</span>
          <p className="basket__price">{getTotal()}$</p>
        </div>
        <hr />

        <div className="basket__btn">
          <span>Continue shopping</span>
          <Link to={'/payment'}>Go to Checkout</Link>
        </div>
      </div>
    </div>
  );
};

export default Basket;
