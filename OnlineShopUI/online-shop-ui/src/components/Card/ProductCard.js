import React from 'react';
import { useState } from 'react';
import './ProductCard.scss';
import ProductBg from '../../img/product-img.png';

const ProductCard = ({ name, description, price, handleClick, card }) => {
  return (
    <div className="product-card__body">
      <div className="product-card__img">
        <img src={ProductBg} alt="img" />
      </div>
      <div className="product-card__info">
        <p>{name}</p>
        <span>{description}</span>
      </div>
      <div className="product-card__price">
        <p>
          <strong>{price} USD</strong>
        </p>
        <button>Buy now</button>
      </div>
      <div>
        <button onClick={() => handleClick(card)}>In cart</button>
      </div>
    </div>
  );
};

export default ProductCard;
