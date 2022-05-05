import { useEffect, useState } from "react";
import CoreAPI from "../../../API/CoreAPI";

const useProductCardFilling = () => {
  const [products, setProducts] = useState(() => []);

  useEffect(() => {
    getProducts();
  }, []);

  function getProducts() {
    CoreAPI
    .get("/product")
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
