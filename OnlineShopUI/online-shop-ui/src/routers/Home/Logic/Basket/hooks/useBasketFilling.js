import { useEffect, useState } from "react";
import CoreAPI from "../../../../../API/CoreAPI";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

toast.configure();

export default function useBasketFilling() {
  const [basket, setBasket] = useState([]);

  useEffect(() => {
    fillBasket();
  }, []);

  function fillBasket() {
    let basketModel = JSON.parse(localStorage.getItem("basket")) ?? [];
    
    const idRangeModel = { ids: basketModel.map((x) => x.id) };

    CoreAPI.post("/product/range", idRangeModel)
      .then((response) => {
        let productsInBasket = response.data.map((product) => {
          return {
            ...product,
            countInBasket: basketModel.find((x) => x.id === product.id)
              .countInBasket,
          };
        });

        setBasket(productsInBasket);
      })
      .catch((error) => {
        if (error?.response?.status >= 400) {
          toast.error("Something went wrong with loading the basket", {
            position: toast.POSITION.BOTTOM_RIGHT,
          });
        }
      });
  }

  return [basket, setBasket];
}
