export default function increaseCountInBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket'));

  let basketItem = basketModel.find((basketItem) => basketItem.id === id);
  basketItem.countInBasket++;

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
