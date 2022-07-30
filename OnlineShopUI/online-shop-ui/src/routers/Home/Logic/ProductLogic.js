import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';

const useProductCardFilling = (setBasket) => {
  const [products, setProducts] = useState(() => []);

  useEffect(() => {
    getProducts();
  }, []);

  // function getMockedProducts() {
  //   let products = cardInfo.map((product) => {
  //     storeInBasketById(product.id);
  //     return { ...product, isInBasket: true };
  //   });
  //   setProducts(products);
  // }

  function getBasket() {
    let basket = JSON.parse(localStorage.getItem('basket')) ?? [];
    console.log(basket);
    return basket;
  }

  function proceedProductsForBasket(products) {
    const basket = getBasket();

    let data = products.map((product) => {
      let productInBasket = basket.find((x) => x.id === product.id);

      if (productInBasket) {
        return {
          ...product,
          countInBasket: productInBasket.countInBasket,
        };
      }

      return {
        ...product,
        countInBasket: 0,
      };
    });

    setBasket(data.filter((x) => x.countInBasket !== 0));

    return data;
  }

  function getProducts() {
    CoreAPI.get('/product')
      .then((response) => {
        // console.log(response.data);
        let data = proceedProductsForBasket(response.data);
        setProducts(data);
      })
      .catch((error) => {
        console.warn(error);
      });
  }

  return products;
};

export default useProductCardFilling;
