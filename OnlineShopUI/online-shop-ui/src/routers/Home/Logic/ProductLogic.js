import { useEffect, useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import useLikes from '../../LikesPage/hooks/useLikes';

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();

  useEffect(() => {
    getProducts();

    console.log(likedProducts);
    if (likedProducts) {
      setProducts(
        products.map((x) => {
          return { ...x, isLiked: true };
        })
      );
    }
  }, []);

  function getProducts() {
    CoreAPI.get('/product')
      .then((response) => {
        setProducts(
          response?.data?.map((product) => {
            return { ...product, countInBasket: 0, isLiked: false };
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
