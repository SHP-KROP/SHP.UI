import { useEffect, useState } from 'react';
import CoreAPI from '../../../../../API/CoreAPI';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

toast.configure();

export default function useBasketFilling() {
  const [basket, setBasket] = useState([]);

  useEffect(() => {
    fillBasket();
  }, []);
  useEffect(() => {
    fillBasket();
  }, [JSON.stringify(basket)]);
  function fillBasket() {
    let basketModel = JSON.parse(localStorage.getItem('basket')) ?? [];

    const idRangeModel = { ids: basketModel.map((x) => x.id) };

    CoreAPI.post('/product/range', idRangeModel)
      .then((response) => {
        if (response.data.length === 0) return;

        let productsInBasket = response.data.map((product) => {
          const existingProduct = basketModel.find((x) => x.id === product.id);
          const countInBasket = existingProduct
            ? existingProduct.countInBasket
            : 0;

          return {
            ...product,
            countInBasket,
          };
        });

        setBasket(productsInBasket);
      })
      .catch((error) => {
        console.log(error);
        if (!error.response) {
          toast.error('Internal server error - server is not available', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
          return;
        }
        if (error?.response?.status >= 400) {
          toast.error('Something went wrong with loading the basket', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      });
  }

  return [basket, setBasket];
}
