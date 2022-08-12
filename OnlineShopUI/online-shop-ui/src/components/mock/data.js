export const cardInfo = [
  {
    id: 1,
    name: 'Product1',
    description: 'Cool product1',
    price: 1000,
    amount: 0,
    isLiked: true,
  },
  {
    id: 2,
    name: 'Product2',
    description: 'Cool product2',
    price: 1002,
    amount: 1,
    isLiked: false,
  },
  {
    id: 3,
    name: 'Product3',
    description: 'Cool product3',
    price: 1003,
    amount: 2,
    isLiked: true,
  },
];

export const columnsTable = [
  { id: 'name', label: 'Name', minWidth: 170 },
  { id: 'description', label: 'Description', minWidth: 100 },
  {
    id: 'price',
    label: 'Price',
    minWidth: 170,
    align: 'right',
    format: (value) => value.toLocaleString('en-US'),
  },
  {
    id: 'amount',
    label: 'Amount',
    minWidth: 170,
    align: 'right',
    format: (value) => value.toLocaleString('en-US'),
  },
];
