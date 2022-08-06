import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';
import useAuth from './Helper/hook/useAuth';
import { useState } from 'react';
import { useMemo } from 'react';
function App() {
  const [user, setUser] = useState(null);
  const prodiverUser = useMemo(() => ({ user, setUser }), [user, setUser]);
  return (
    <>
      <useAuth.Provider value={prodiverUser}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Home />}></Route>
          <Route path="/register" element={<Register />}></Route>
          <Route path="/likes" element={<LikesPage />}></Route>
        </Routes>
      </useAuth.Provider>
    </>
  );
}

export default App;
