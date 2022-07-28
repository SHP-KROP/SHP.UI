import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import { cardInfo } from '../../../components/mock/data';
import storeInBasketById from './Basket/StoreInBasket';

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);

  useEffect(() => {
    getMockedProducts();
  }, []);

  function getMockedProducts() {
    let products = cardInfo.map((product) => {
      storeInBasketById(product.id);
      return { ...product, isInBasket: true };
    });
    setProducts(products);
  }

  function getProducts() {
    CoreAPI.get('/product')
      .then((response) => {
        console.log(response.data);
        setProducts(response.data);
      })
      .catch((error) => {
        console.warn(error);
      });
  }

  return products;
};

export default useProductCardFilling;
