import './App.scss';
import Header from './components/Header';
import Search from './components/Search';
// import MenuBoard from './components/Menu';
import ProductCard from './components/ProductCard';
import SideMenuList from './components/SideMenuList';
import Banner from './components/Banner';

const arr = [
  { name: 'NameTitle', price: '12999' },
  { name: 'AnotherNameTitle', price: '12109' },
  { name: 'AnotherNameTitle', price: '12109' },
  { name: 'AnotherNameTitle', price: '12109' },
  { name: 'AnotherNameTitle', price: '12109' },
  { name: 'AnotherNameTitle', price: '12109' },
  { name: 'AnotherNameTitle', price: '12109' },
  { name: 'AnotherNameTitle', price: '12109' },
];
const menu = [
  {
    nameCategory: 'asd',
    item: 'asdasd',
    item1: 'agfhg',
    item2: 'opietu',
    item3: 'wijvn',
    item4: 'fsdgm',
  },
  {
    nameCategory: 'asd',
    item: 'asdasd',
    item1: 'agfhg',
    item2: 'opietu',
    item3: 'wijvn',
    item4: 'fsdgm',
  },
  {
    nameCategory: 'asd',
    item: 'asdasd',
    item1: 'agfhg',
    item2: 'opietu',
    item3: 'wijvn',
    item4: 'fsdgm',
  },
];

function App() {
  return (
    <div className="App">
      <Header />
      <Search />
      {/* <MenuBoard /> */}
      <div className="blocks">
        <div className="blockSideMenus">
          {menu.map((obj) => (
            <SideMenuList
              nameCategory={obj.nameCategory}
              item={obj.item}
              item1={obj.item1}
              item2={obj.item2}
              item3={obj.item3}
              item4={obj.item4}
            />
          ))}
        </div>
        <div className="blockBanners">
          <Banner />
          <Banner />
        </div>
        <div className="blockCards">
          {arr.map((obj) => (
            <ProductCard name={obj.name} price={obj.price} />
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;
