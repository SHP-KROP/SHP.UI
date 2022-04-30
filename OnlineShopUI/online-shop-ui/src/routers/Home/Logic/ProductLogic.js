import { useEffect, useState } from "react";
import axios from 'axios';

const BASE_URL = "https://localhost:44318/api/Product";

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);

  useEffect(() => {
    getProducts();
  }, []);

  function getProducts() {
    axios
    .get(BASE_URL)
    .then((response) => {
      console.log(response.data)
      setProducts(response.data);
    })
    .catch((error) => {
      console.warn(error);
    })
  }

  return products;
};

export default useProductCardFilling;
