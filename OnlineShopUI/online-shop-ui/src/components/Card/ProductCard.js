import React, { useState } from 'react';
import './ProductCard.scss';
import ProductBg from '../../img/product-img.png';
import LocalMallIcon from '@mui/icons-material/LocalMall';
import useLikes from '../../routers/LikesPage/hooks/useLikes';
import FavoriteIcon from '@mui/icons-material/Favorite';
const ProductCard = ({
  name,
  description,
  price,
  handleClick,
  card,
  basket,
}) => {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [isLiked, setIsLiked] = useState(true);
  const isInBasket = () => {
    if (!basket) {
      return false;
    }
    return !!basket.find((x) => x.id === card.id);
  };
  const setStateLike = (isLiked) => {
    if (isLiked) {
      setIsLiked(!isLiked);
      unlikeProductById(1);
    }
    likeProductById(1);
    setIsLiked(isLiked);
  };

  return (
    <div className="product-card__body" id="product-card__body">
      <div className="product-card__img">
        <img src={ProductBg} alt="img" />
        <div className="product-card__img-buttons">
          <button onClick={() => setStateLike(isLiked)}>
            <FavoriteIcon sx={{ fill: 'red' }} />
          </button>
        </div>
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
        <div className="product-card__addtobasket">
          <button
            style={{ backgroundColor: 'inherit', border: 'none' }}
            disabled={isInBasket()}
            onClick={() => handleClick(card)}
          >
            <LocalMallIcon />
          </button>
        </div>
      </div>
    </div>
  );
};

export default ProductCard;
