import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';

function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/home" element={<Home />}></Route>
        <Route path="/register" element={<Register />}></Route>
        <Route path="/likes" element={<LikesPage />}></Route>
      </Routes>
    </>
  );
}

export default App;
