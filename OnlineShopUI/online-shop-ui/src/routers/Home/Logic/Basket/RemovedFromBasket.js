export default function removeFromBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket'));

  if (!basketModel) {
    localStorage.setItem('basket', JSON.stringify([]));
  }

  basketModel = basketModel.filter((productId) => productId !== id);

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
