import React, { useState } from 'react';
import './ProductCard.scss';
import ProductBg from '../../img/product-img.png';
import LocalMallIcon from '@mui/icons-material/LocalMall';
import useLikes from '../../routers/LikesPage/hooks/useLikes';
import FavoriteIcon from '@mui/icons-material/Favorite';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import useAuth from '../../hooks/useAuth';

const ProductCard = ({ handleClick, card, basket }) => {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [isLiked, setIsLiked] = useState(card.isLiked);
  const { user } = useAuth();

  const isInBasket = () => {
    if (!basket) {
      return false;
    }
    return !!basket.find((x) => x.id === card.id);
  };

  const onLikeClicked = () => {
    if (!user) return;
    isLiked ? unlikeProductById(card.id) : likeProductById(card.id);
    setIsLiked((prev) => !prev);
  };

  const handleDoubleClick = (event) => {
    if (event.detail === 2) {
      onLikeClicked();
    }
  };

  return (
    <div
      style={{
        background: isLiked ? ' rgb(241,252,164)' : 'white',
        transition: '0.15s ease-in-out',
      }}
      onClick={handleDoubleClick}
      className="product-card__body"
    >
      <div className="product-card__img">
        <img src={ProductBg} alt="img" />
        <div className="product-card__img-buttons">
          {user && (
            <button onClick={onLikeClicked}>
              {isLiked ? (
                <FavoriteIcon sx={{ fill: 'red' }} />
              ) : (
                <FavoriteBorderIcon />
              )}
            </button>
          )}
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
