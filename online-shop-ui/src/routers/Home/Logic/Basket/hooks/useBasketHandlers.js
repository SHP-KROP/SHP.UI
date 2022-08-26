import storeInBasketById from "../functions/StoreInBasketById";
import removeFromBasketById from "../functions/RemoveFromBasketById";
import increaseCountInBasketById from "../functions/IncreaseCountInBasketById";
import decreaseCountInBasketById from "../functions/DecreaseCountInBasketById";

export default function useBasketHandlers({ basket, setBasket }) {
  const handleClickAddInBasket = (card) => {
    if (!card.countInBasket) {
      card = { ...card, countInBasket: 1 };
      console.log(card);
      setBasket([...basket, card]);
    }
    storeInBasketById(card.id);
  };

  const handleClickRemoveFromBasket = (card) => {
    card.countInBasket = 0;
    removeFromBasketById(card.id);
    let newBasket = basket.filter((x) => x.id !== card.id);
    setBasket(newBasket);
  };

  const handleClickIncreaseBasketCount = (card) => {
    increaseCountInBasketById(card.id);
    card.countInBasket++;
  };

  const handleClickDecreaseBasketCount = (card) => {
    decreaseCountInBasketById(card.id);
    card.countInBasket--;
    if (card.countInBasket === 0) {
      setBasket(basket.filter((basketItem) => basketItem.id !== card.id));
    }
  };

  return [
    handleClickAddInBasket,
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ];
}
