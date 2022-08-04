import React, { useState } from 'react';
import '../HeadBlock/HeadBlock.scss';
import Login from '../ModalLogin/Login';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import Fade from '@mui/material/Fade';
import Badge from '@mui/material/Badge';
import Card from '../../img/icon-card.png';
import { Link } from 'react-router-dom';
import BookmarkBorderIcon from '@mui/icons-material/BookmarkBorder';

const HeadBlock = ({ onClickCart, basketOpen, productsInBasketCount }) => {
  const [anchorElement, setAnchorElement] = useState(null);
  const isOpen = !!anchorElement;

  const handleClick = (event) => {
    setAnchorElement(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorElement(null);
  };
  return (
    <div className="search">
      <div className="name-shop">
        <h1>NameShop</h1>
      </div>
      <div className="search__input">
        <button
          className="search__buttonList"
          id="fade-button"
          aria-controls={isOpen ? 'fade-menu' : undefined}
          aria-haspopup="true"
          aria-expanded={isOpen ? 'true' : undefined}
          onClick={handleClick}
        >
          <p className="list">
            All categories <p className="arrow">&#11167;</p>
          </p>
        </button>
        <Menu
          id="fade-menu"
          MenuListProps={{
            'aria-labelledby': 'fade-button',
          }}
          anchorEl={anchorElement}
          open={isOpen}
          onClose={handleClose}
          TransitionComponent={Fade}
        >
          <MenuItem onClick={handleClose}>Product</MenuItem>
          <MenuItem onClick={handleClose}>Product</MenuItem>
          <MenuItem onClick={handleClose}>Product</MenuItem>
        </Menu>
        <input type="text" placeholder="Search Products, categories ..." />
      </div>
      <div className="profile">
        <Login />
        {!basketOpen && (
          <Badge
            badgeContent={productsInBasketCount}
            color="secondary"
            max={99}
          >
            <button className="openCartButton" onClick={onClickCart}>
              <img src={Card} alt="card" />
            </button>
          </Badge>
        )}
        <Link to="/likes">
          <BookmarkBorderIcon sx={{ fill: 'black' }} />
        </Link>
      </div>
    </div>
  );
};

export default HeadBlock;
