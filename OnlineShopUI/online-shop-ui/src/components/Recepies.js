import React from 'react';
import '../components/Recepies.scss';
import SideMenuList from './SideMenuList';

const Recepies = () => {
  return (
    <div className="categories">
      <SideMenuList
        nameCategory="Category menu"
        item="Bakery"
        item1="Fruit and vegetables"
        item2="Meat and fish"
        item3="Drinks"
        item4="Kitchen"
      />
      <div className="categories__banner">
        <div className="categories__banner-info">
          <span>Banner subfocus</span>
          <p>
            <strong>Space for heading</strong>
          </p>
        </div>
        <button className="categories__read">
          <p className="categories__button-text">
            <strong>Read recepies</strong>
            <p>&#11166;</p>
          </p>
        </button>
      </div>
      <div className="categories__banner">
        <div className="categories__banner-info">
          <span>Banner subfocus</span>
          <p>
            <strong>Space for heading</strong>
          </p>
        </div>
        <button className="categories__read">
          <p className="categories__button-text">
            <strong>Read recepies</strong>
            <p>&#11166;</p>
          </p>
        </button>
      </div>
    </div>
  );
};

export default Recepies;
