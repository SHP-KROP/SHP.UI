import React from 'react';
import './Search.scss';

import IconSearch from '../img/icon-search.png';

import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import Fade from '@mui/material/Fade';
import User from '../img/icon-user.png';
import Card from '../img/icon-card.png';

const Search = () => {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const open = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
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
          aria-controls={open ? 'fade-menu' : undefined}
          aria-haspopup="true"
          aria-expanded={open ? 'true' : undefined}
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
          anchorEl={anchorEl}
          open={open}
          onClose={handleClose}
          TransitionComponent={Fade}
        >
          <MenuItem onClick={handleClose}>Product</MenuItem>
          <MenuItem onClick={handleClose}>Product</MenuItem>
          <MenuItem onClick={handleClose}>Product</MenuItem>
        </Menu>
        <input type="text" placeholder="Search Products, categories ..." />
        <img src={IconSearch} alt="search" />
      </div>
      <div className="profile">
        <a href="#">
          <img src={User} alt="user" />
        </a>
        <a href="#">
          <img src={Card} alt="card" />
        </a>
      </div>
    </div>
  );
};

export default Search;
