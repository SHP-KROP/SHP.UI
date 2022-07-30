import removeFromBasketById from './RemoveFromBasketById';

export default function decreaseCountInBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket'));

  let basketItem = basketModel.find((basketItem) => basketItem.id === id);
  if (basketItem.countInBasket === 1) {
    removeFromBasketById(id);
  } else {
    basketItem.countInBasket--;
  }

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
