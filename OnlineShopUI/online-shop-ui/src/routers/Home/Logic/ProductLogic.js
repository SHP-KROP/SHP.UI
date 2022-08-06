import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import useAuth from '../../../Helper/hook/useAuth';
import useAuthHeaders from '../../LikesPage/hooks/useAuthHeaders';

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);

  const { user } = useAuth();
  const authHeaders = useAuthHeaders();

  useEffect(() => {
    user ? getProductsWithLikes() : getProducts();
  }, []);

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
