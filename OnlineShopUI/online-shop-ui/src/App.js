import './App.scss';
import Header from './components/Header';
import Recepies from './components/Recepies';
import Search from './components/Search';
// import MenuBoard from './components/Menu';
function App() {
  return (
    <div className="App">
      <Header />
      <Search />
      {/* <MenuBoard /> */}
      <Recepies />
    </div>
  );
}

export default App;
