import './App.scss';
import Header from './components/Header';
import HeadBlock from './components/HeadBlock';
// import MenuBoard from './components/Menu';
import ProductCard from './components/ProductCard';
import SideMenuList from './components/SideMenuList';
import Banner from './components/Banner';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Register from './components/Register/Register';
import MainMage from './components/MainPage';

const arr = [
  { name: 'NameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
  { name: 'AnotherNameTitle', description: 'decsr', price: 1000 },
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
    <>
      <Routes>
        <Route path="/main" element={<MainMage />}></Route>
        <Route path="/register" element={<Register />}></Route>
      </Routes>
    </>
  );
}

export default App;
