import React from 'react';
import './ProductCard.scss';
import ProductBg from '../img/product-img.png';

const ProductCard = () => {
  return (
    <div className="product-card__body">
      <div className="product-card__img">
        <img src={ProductBg} alt="img" />
      </div>
      <div className="product-card__info">
        <p>Product title</p>
        <span>Space for a small product description</span>
      </div>
      <div className="product-card__price">
        <p>
          <strong>1.48 USD</strong>
        </p>
        <button>Buy now</button>
      </div>
    </div>
  );
};

export default ProductCard;
