import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';
import { useState } from 'react';
import { useMemo } from 'react';
import { UserContext } from './Helper/hook/UserContext';
function App() {
  const [user, setUser] = useState(null);
  const prodiverUser = useMemo(() => ({ user, setUser }), [user, setUser]);

  return (
    <>
      <UserContext.Provider value={prodiverUser}>
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
