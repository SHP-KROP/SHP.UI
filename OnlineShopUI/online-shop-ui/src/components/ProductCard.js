import React from 'react';
import './ProductCard.scss';
import ProductBg from '../img/product-img.png';

const ProductCard = (props) => {
  return (
    <div className="product-card__body">
      <div className="product-card__img">
        <img src={ProductBg} alt="img" />
      </div>
      <div className="product-card__info">
        <p>{props.name}</p>
        <span>{props.description}</span>
      </div>
      <div className="product-card__price">
        <p>
          <strong>{props.price} USD</strong>
        </p>
        <button>Buy now</button>
      </div>
    </div>
  );
};

export default ProductCard;
