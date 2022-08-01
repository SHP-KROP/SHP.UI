import { useState, useEffect } from 'react';
import CoreAPI from '../../../API/CoreAPI';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import useAuthHeaders from './useAuthHeaders';

toast.configure();

export default function useLikes() {
  const [isLoading, setLoading] = useState(false);
  const [likedProducts, setLikedProducts] = useState([]);
  const authHeaders = useAuthHeaders();

  function unlikeProductById(id) {
    CoreAPI.delete(`/like/${id}`, authHeaders)
      .then((response) => {
        setLikedProducts(likedProducts.filter(x => x.id !== response.data.id));
      })
      .catch((error) => {
        if (!error.response) {
          toast.error('Internal server error - server is not available', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
          return;
        }
        if (error?.response?.status === 400) {
          toast.error('Unable to unlike the product', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      })
      .finally(() => {
        setLoading(false);
      });
  }

  function likeProductById(id) {
    CoreAPI.post(`/like/${id}`, {}, authHeaders)
      .then((response) => {
        setLikedProducts([...likedProducts, response.data]);
      })
      .catch((error) => {
        if (!error.response) {
          toast.error('Internal server error - server is not available', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
          return;
        }
        if (error?.response?.status === 400) {
          toast.error('Unable to like the product', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      })
      .finally(() => {
        setLoading(false);
      });
  }

  useEffect(() => {
    getLikedProducts();
  }, []);

  function getLikedProducts() {
    setLoading(true);

    CoreAPI.get('/like', authHeaders)
      .then((response) => {
        setLikedProducts(response.data);
      })
      .catch((error) => {
        if (!error.response) {
          toast.error('Internal server error - server is not available', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
          return;
        }
        if (error?.response?.status === 400) {
          toast.error('Something went wrong with loading liked', {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      })
      .finally(() => {
        setLoading(false);
      });
  }

  return [isLoading, likedProducts, likeProductById, unlikeProductById];
}
