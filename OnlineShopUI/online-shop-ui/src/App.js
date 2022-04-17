import './App.scss';
import Header from './components/Header';
import HeadBlock from './components/HeadBlock';
// import MenuBoard from './components/Menu';
import ProductCard from './components/ProductCard';
import SideMenuList from './components/SideMenuList';
import Banner from './components/Banner';

const arr = [
  { name: 'NameTitle', price: '12999' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
  { name: 'AnotherNameTitle', description: 'decsr', price: '12109' },
];
const menu = [
  {
    nameCategory: 'asd',
    subCategories: [
      {
        name: 'asdasd',
        link: '/asdasd',
      },
    ],
  },
  {
    nameCategory: 'asd',
    subCategories: [
      {
        name: 'asdasd',
        link: '/asdasd',
      },
    ],
  },
  {
    nameCategory: 'ass',
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

function App() {
  return (
    <div className="App">
      <Header />
      <HeadBlock />
      {/* <MenuBoard /> */}
      <div className="blocks">
        <div className="blockSideMenus">
          {menu.map((obj) => (
            <SideMenuList
              nameCategory={obj.nameCategory}
              subCategories={obj.subCategories}
            />
          ))}
        </div>
        <div className="blockBanners">
          <Banner />
          <Banner />
        </div>
        <div className="blockCards">
          {arr.map((obj) => (
            <ProductCard
              name={obj.name}
              description={obj.description}
              price={obj.price}
            />
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;
