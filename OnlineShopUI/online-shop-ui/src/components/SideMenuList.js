import React from 'react';
import './SideMenuList.scss';

const SideMenuList = (props) => {
  return (
    <div className="categories__menu">
      <p>
        <strong>{props.nameCategory}</strong>
      </p>
      <ul>
        {props.subCategories.map((linkName) => {
          return (
            <li>
              <a href={linkName.link}>{linkName.name}</a>
            </li>
          );
        })}
      </ul>
      <button>
        <strong>More categories</strong> &#11166;
        {/*&#11166 - html code sybmol: arrow right*/}
      </button>
    </div>
  );
};
export default SideMenuList;
