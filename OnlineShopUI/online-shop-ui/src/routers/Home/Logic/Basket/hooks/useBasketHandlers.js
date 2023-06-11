import storeInBasketById from '../functions/StoreInBasketById';
import removeFromBasketById from '../functions/RemoveFromBasketById';
import increaseCountInBasketById from '../functions/IncreaseCountInBasketById';
import decreaseCountInBasketById from '../functions/DecreaseCountInBasketById';

export default function useBasketHandlers({ basket, setBasket }) {
  const handleClickAddInBasket = (card) => {
    if (!basket.some((item) => item.id === card.id)) {
      card.countInBasket = 1;
      storeInBasketById(card.id);
      setBasket([...basket, card]);
    }
  };

  const handleClickRemoveFromBasket = (card) => {
    removeFromBasketById(card.id);
    const updatedBasket = basket.filter((item) => item.id !== card.id);
    setBasket(updatedBasket);
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
