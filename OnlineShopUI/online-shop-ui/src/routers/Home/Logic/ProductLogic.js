import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import useAuth from '../../../hooks/useAuth';
import useAuthHeaders from '../../../hooks/useAuthHeaders';

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);

  const { user } = useAuth();
  const authHeaders = useAuthHeaders();

  useEffect(() => {
    user ? getProductsWithLikes() : getProducts();
  }, []);

  useEffect(() => {
    user ? getProductsWithLikes() : getProducts();
  }, [JSON.stringify(user)]);

  function getProductsWithLikes() {
    CoreAPI.get('/like/product', authHeaders)
      .then((response) => {
        setProducts(
          response?.data?.map((product) => {
            return { ...product, countInBasket: 0 };
          })
        );
      })
      .catch((error) => {
        console.warn(error);
      });
  }

  function getProducts() {
    CoreAPI.get('/product')
      .then((response) => {
        setProducts(
          response?.data?.map((product) => {
            return { ...product, countInBasket: 0 };
          })
        );
      })
      .catch((error) => {
        console.warn(error);
      });
  }

  return products;
};

export default useProductCardFilling;
