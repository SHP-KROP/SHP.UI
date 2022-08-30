import { useState } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import useAuthHeaders from '../../../hooks/useAuthHeaders';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useEffect } from 'react';
toast.configure();

export default function useSellerProduct() {
  const [sellerProducts, setSellerProducts] = useState([]);
  const authHeaders = useAuthHeaders();

  useEffect(() => {
    fillSellerProducts();
  }, []);

  function fillSellerProducts() {
    CoreAPI.get('/product/my-products', authHeaders)
      .then((response) => {
        if (response.status === 204) return;

        setSellerProducts(response.data);
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
          toast.error('Something went wrong with loading the products', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      });
  }

  return [sellerProducts, setSellerProducts];
}
