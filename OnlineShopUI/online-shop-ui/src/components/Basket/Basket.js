import React from 'react';
import './Basket.scss';
import CloseIcon from '@mui/icons-material/Close';
import CardInBasket from '../CardInBasket/CardInBasket';

const Basket = ({ onClose, opened, cardInfo, basket }) => {
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
                <CardInBasket
                  cardName={card.name}
                  cardPrice={card.price}
                  cardId={card.id}
                />
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
