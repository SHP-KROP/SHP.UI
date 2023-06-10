import React, { useState } from 'react';
import './ProductCard.scss';
import LocalMallIcon from '@mui/icons-material/LocalMall';
import useLikes from '../../routers/LikesPage/hooks/useLikes';
import FavoriteIcon from '@mui/icons-material/Favorite';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import useAuth from '../../hooks/useAuth';
import { Link } from 'react-router-dom';

const ProductCard = ({ card, addToBasket, basket }) => {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [isLiked, setIsLiked] = useState(card.isLiked);
  const { user } = useAuth();

  const isInBasket = () => {
    return basket.some((item) => item.id === card.id);
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

  const handleAddToBasket = () => {
    addToBasket(card);
  };

  return (
    <div
      style={{
        background: user && isLiked ? 'rgb(192, 192, 192)' : 'white',
        transition: '0.15s ease-in-out',
      }}
      onClick={handleDoubleClick}
      className="product-card__body"
    >
      <div className="product-card__img">
        <img src={card.photoUrl} alt="img" />
        <div className="product-card__img-buttons">
          <button onClick={onLikeClicked}>
            <div>
              {isLiked ? (
                <FavoriteIcon
                  sx={{ visibility: user ? 'visible' : 'hidden', fill: 'red' }}
                />
              ) : (
                <FavoriteBorderIcon
                  sx={{ visibility: user ? 'visible' : 'hidden' }}
                />
              )}
            </div>
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
        <Link to={`/product/${card.name}`}>Details</Link>
        <div className="product-card__addtobasket">
          <button
            style={{
              backgroundColor: 'inherit',
              border: 'none',
            }}
            onClick={handleAddToBasket}
            disabled={isInBasket()}
          >
            <LocalMallIcon
              style={{ fill: isInBasket() ? 'gray' : 'inherit' }} // Set the fill color to gray when the button is disabled
            />
          </button>
        </div>
      </div>
    </div>
  );
};

export default ProductCard;
