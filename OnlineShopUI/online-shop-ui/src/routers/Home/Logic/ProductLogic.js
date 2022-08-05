import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import useAuthHeaders from '../../LikesPage/hooks/useAuthHeaders';
import useLikes from '../../LikesPage/hooks/useLikes';

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const authHeaders = useAuthHeaders();

  useEffect(() => {
    authHeaders?.headers?.Authorization
      ? getProductsWithLikes()
      : getProducts();
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
