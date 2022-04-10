import React from 'react';
import '../components/Recepies.scss';

const Recepies = () => {
  return (
    <div className="categories">
      <div className="categories__menu">
        <p>
          <strong>Category menu</strong>
        </p>
        <ul>
          <li>
            <a href="#">Bakery</a>
          </li>
          <li>
            <a href="#">Fruit and vegetables</a>
          </li>
          <li>
            <a href="#">Meat and fish</a>
          </li>
          <li>
            <a href="#">Drinks</a>
          </li>
          <li>
            <a href="#">Kitchen</a>
          </li>
        </ul>
        <button>
          <strong>More categories</strong> &#11166;
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
