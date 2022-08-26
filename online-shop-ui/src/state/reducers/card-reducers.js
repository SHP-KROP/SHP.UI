export const cards = (state = [], action) => {
  switch (action.type) {
    case 'ADD_CARD': {
      return [
        ...state,
        {
          addInBasket: false,
        },
      ];
    }
    default: {
      return state;
    }
  }
};
