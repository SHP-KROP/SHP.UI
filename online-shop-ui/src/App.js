import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';
import { UserContext } from './Contexts/UserContext';
import useAuthProvider from './hooks/useAuthProvider';

function App() {
  const userProvider = useAuthProvider();

  return (
    <>
      <UserContext.Provider value={userProvider}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Home />}></Route>
          <Route path="/register" element={<Register />}></Route>
          <Route path="/likes" element={<LikesPage />}></Route>
        </Routes>
      </UserContext.Provider>
    </>
  );
}

export default App;
