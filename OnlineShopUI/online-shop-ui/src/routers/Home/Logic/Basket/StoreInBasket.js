export default function storeInBasketById(id) {
  let basketModel = JSON.parse(localStorage.getItem('basket'));
  console.log(`Baskete model ${basketModel}`);

  if (!basketModel) {
    basketModel = [];
    basketModel.push(id);
    localStorage.setItem('basket', JSON.stringify(basketModel));
    return;
  }

  if (!basketModel.includes(id)) {
    basketModel.push(id);
  }

  localStorage.setItem('basket', JSON.stringify(basketModel));
}
