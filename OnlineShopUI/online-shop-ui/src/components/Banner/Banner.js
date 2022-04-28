import React from 'react';
import './Banner.scss';

const Banner = () => {
  return (
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
          <p>&#11166;</p> {/* html code arrow right */}
        </p>
      </button>
    </div>
  );
};

export default Banner;
