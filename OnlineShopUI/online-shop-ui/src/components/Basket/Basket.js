import React from 'react';
import './Basket.scss';
import Remove from '../../img/btn-remove.svg';
import CloseIcon from '@mui/icons-material/Close';
import removeFromBasketById from '../../routers/Home/Logic/Basket/RemovedFromBasket';

const Basket = ({ onClose, opened, cardInfo }) => {
  return (
    <div className={opened ? 'overlay' : 'overlayUnVisible'}>
      <div className="basket">
        <div className="basket__title">
          <h2>
            Shopping cart
            <button className="basket__cartClose" onClick={onClose}>
              <CloseIcon style={{ color: 'red' }}></CloseIcon>
            </button>
          </h2>
        </div>
        <div className="basket__items">
          {cardInfo.map((card) => {
            if (card.isInBasket) {
              return (
                <div className="basket__cartItem">
                  <div className="basket__cartItemPhoto"></div>
                  <div className="basket__itemInfo">
                    <p>{card.name}</p>
                    <b>{card.price} usd</b>
                  </div>
                  <button onClick={() => removeFromBasketById(card.id)}>
                    <img src={Remove} alt="Remove" />
                  </button>
                </div>
              );
            }
          })}
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
