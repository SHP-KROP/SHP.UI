import React from 'react';
import './Basket.scss';
import Remove from '../../img/btn-remove.svg';
import CloseCart from '../../img/btn-close.svg';

const Basket = ({ onClose, opened }) => {
  return (
    <div className={opened ? 'overlay' : 'overlayUnVisible'}>
      <div className="basket">
        <div className="basket__title">
          <h2>
            Shopping cart
            <button className="basket__cartClose" onClick={onClose}>
              <img src={CloseCart} alt="CloseCart" />
            </button>
          </h2>
        </div>
        <div className="basket__items">
          <div className="basket__cartItem">
            <div className="basket__cartItemPhoto"></div>
            <div className="basket__itemInfo">
              <p>Lorem ipsum dolor, sit amet consectetur</p>
              <b>12 999 usd</b>
            </div>
            <img src={Remove} alt="Remove" />
          </div>
        </div>
        <div className="basket__price">
          <span>Subtotal</span>
          <p className="basket__price">73.98 USD</p>
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
