import React from 'react';
import './BlockProduct.scss';
import ProductCard from './ProductCard';
import SideMenuList from './SideMenuList';

const BlockProduct = () => {
  return (
    <div className="product-card">
      <SideMenuList
        nameCategory="Best selling products"
        item="Kitchen"
        item1="Meat and fish"
        item2="Special nutrition"
        item3="Pharmacy"
        item4="Kitchen"
      />
      <ProductCard />
      <ProductCard />
      <ProductCard />
    </div>
  );
};

export default BlockProduct;
