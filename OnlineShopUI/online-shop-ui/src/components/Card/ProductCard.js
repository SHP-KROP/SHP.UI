import React, { useState } from 'react';
import './ProductCard.scss';
import ProductBg from '../../img/product-img.png';
import LocalMallIcon from '@mui/icons-material/LocalMall';
import useLikes from '../../routers/LikesPage/hooks/useLikes';
import FavoriteIcon from '@mui/icons-material/Favorite';

const ProductCard = ({ handleClick, card, basket }) => {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [isLiked, setIsLiked] = useState(card.isLiked);
  const isInBasket = () => {
    if (!basket) {
      return false;
    }
    return !!basket.find((x) => x.id === card.id);
  };

  const onLikeClicked = () => {
    isLiked ? unlikeProductById(card.id) : likeProductById(card.id);
    setIsLiked((prev) => !prev);
  };

  // const setStateLike = () => {
  //   if (isLiked) {
  //     setIsLiked(!isLiked);
  //     unlikeProductById(1);
  //     return;
  //   }
  //   likeProductById(1);
  //   setIsLiked(isLiked);
  // };

  return (
    <div className="product-card__body" id="product-card__body">
      <div className="product-card__img">
        <img src={ProductBg} alt="img" />
        <div className="product-card__img-buttons">
          <button onClick={onLikeClicked}>
            <FavoriteIcon sx={{ fill: isLiked ? 'red' : 'blue' }} />
          </button>
        </div>
      </div>
      <div className="product-card__info">
        <p>{card.name}</p>
        <span>{card.description}</span>
      </div>
      <div className="product-card__price">
        <p>
          <strong>{card.price} USD</strong>
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
