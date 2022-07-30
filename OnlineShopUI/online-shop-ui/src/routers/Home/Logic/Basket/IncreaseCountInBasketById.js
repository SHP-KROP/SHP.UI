export default function increaseCountInBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket')); //{id: 1, countInBasket: 0} or null

  let basketItem = basketModel.find((basketItem) => basketItem.id === id);
  basketItem.countInBasket++;

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
