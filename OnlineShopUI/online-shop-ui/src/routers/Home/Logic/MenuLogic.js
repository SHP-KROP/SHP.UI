import Sneakers from '../../.././img/sneakers.png';
import Trousers from '../../.././img/trousers.png';
import Jackets from '../../.././img/jackets.png';
const useMenuFilling = () => {
  const menu = [
    {
      nameCategory: 'Jackets',
      iconCategory: Jackets,
      subCategories: [
        {
          name: 'asdasd',
          link: '/asdasd',
        },
      ],
    },
    {
      nameCategory: 'Trousers',
      iconCategory: Trousers,
      subCategories: [
        {
          name: 'asdasd',
          link: '/asdasd',
        },
      ],
    },
    {
      nameCategory: 'Sneakers',
      iconCategory: Sneakers,
      subCategories: [
        {
          name: 'pampers',
          link: '/papmers',
        },
        {
          name: 'poops',
          link: '/poops',
        },
      ],
    },
  ];

  return menu;
};

export default useMenuFilling;
