import React, { useEffect, useState } from 'react';
import '../HeadBlock/HeadBlock.scss';
import Login from '../ModalLogin/Login';
import Badge from '@mui/material/Badge';
import Card from '../../img/icon-card.png';
import { Link } from 'react-router-dom';
import BookmarkBorderIcon from '@mui/icons-material/BookmarkBorder';
import LogoutIcon from '@mui/icons-material/Logout';
import useLogout from '../../hooks/useLogout';
import IdentityAPI from '../../API/IdentityServerAPI';
import GoogleIcon from '@mui/icons-material/Google';
import useAuth from '../../hooks/useAuth';

const HeadBlock = ({ onClickCart, basketOpen, basket }) => {
  const [authUrl, setAuthUrl] = useState('');
  const { user } = useAuth();
  const [productsInBasketCount, setProductsInBasketCount] = useState(0);

  useEffect(() => {
    IdentityAPI.post('/user/redirect-to-auth', {
      redirectUrl: window.location.href,
    })
      .then((response) => {
        setAuthUrl(response.data);
        console.log(response.data);
      })
      .catch((error) => {
        console.warn(error);
      });
  }, []);
  useEffect(() => {
    const count = basket.reduce(
      (count, product) => count + product.countInBasket,
      0
    );
    setProductsInBasketCount(count);
  }, [basket]);

  const logOut = useLogout();

  return (
    <div className="search">
      <div className="name-shop">
        <h1>FashionShop</h1>
      </div>

      <div className="profile">
        <Login />
        {!basketOpen && (
          <Badge badgeContent={basket.length} color="secondary" max={99}>
            <button className="openCartButton" onClick={onClickCart}>
              <img src={Card} alt="card" />
            </button>
          </Badge>
        )}
        <Link to="/likes">
          <BookmarkBorderIcon sx={{ fill: 'black' }} />
        </Link>
        <div className="logout">
          <button onClick={logOut}>
            <LogoutIcon />
          </button>
        </div>
        <div>
          <button
            onClick={() => {
              window.location.href = authUrl;
            }}
            style={{ backgroundColor: 'inherit', border: 'none' }}
          >
            <GoogleIcon />
          </button>
        </div>
      </div>
    </div>
  );
};

export default HeadBlock;
