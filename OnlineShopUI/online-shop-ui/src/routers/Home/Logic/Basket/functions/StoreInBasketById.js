export default function storeInBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket'));
  console.log(`BASKET MODEL: `, basketModel);

  if (!basketModel) {
    basketModel = [{ id: id, countInBasket: 1 }];
    localStorage.setItem('basket', JSON.stringify(basketModel));
    return;
  }

  if (basketModel.find((basket) => basket.id === id)) {
    let basketItem = basketModel.find((basketItem) => basketItem.id === id);
    basketItem.countInBasket = 1;
  } else {
    basketModel.push({ id: id, countInBasket: 1 });
  }

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
