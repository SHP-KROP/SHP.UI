import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);

  useEffect(() => {
    getProducts();
  }, []);

  function getProducts() {
    CoreAPI.get('/product')
      .then((response) => {
        setProducts(response.data.map(product => {
          return {...product, countInBasket: 0};
        }));
      })
      .catch((error) => {
        console.warn(error);
      });
  }

  return products;
};

export default useProductCardFilling;
