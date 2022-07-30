import removeFromBasketById from './RemovedFromBasket';

export default function decreaseCountInBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket')); //{id: 1, countInBasket: 0} or null

  let basketItem = basketModel.find((basketItem) => basketItem.id === id);
  if (basketItem.countInBasket === 1) {
    removeFromBasketById(id);
  } else {
    basketItem.countInBasket--;
  }

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
