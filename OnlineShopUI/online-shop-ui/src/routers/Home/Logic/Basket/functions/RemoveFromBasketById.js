export default function removeFromBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket'));

  if (!basketModel) {
    return;
  }

  basketModel = basketModel.filter((basketProduct) => basketProduct.id !== id);

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
