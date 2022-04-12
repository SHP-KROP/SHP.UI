import React from 'react';
import './SideMenuList.scss';

const SideMenuList = (props) => {
  return (
    <div className="categories__menu">
      <p>
        <strong>{props.nameCategory}</strong>
      </p>
      <ul>
        <li>
          <a href="#">{props.item}</a>
        </li>
        <li>
          <a href="#">{props.item1}</a>
        </li>
        <li>
          <a href="#">{props.item2}</a>
        </li>
        <li>
          <a href="#">{props.item3}</a>
        </li>
        <li>
          <a href="#">{props.item4}</a>
        </li>
      </ul>
      <button>
        <strong>More categories</strong> &#11166;
      </button>
    </div>
  );
};
export default SideMenuList;
