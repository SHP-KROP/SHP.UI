import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';
import { UserContext } from './Contexts/UserContext';
import useAuthProvider from './hooks/useAuthProvider';
import CrudSeller from './routers/Seller/CRUD/CrudSeller';
import ProductInfo from './routers/Product/ProductInfo';
import { useState } from 'react';
import Payments from './routers/Payments/Payments';
function App() {
  const userProvider = useAuthProvider();
  const [isBasketOpen, setBasketOpen] = useState(() => false);

  return (
    <>
      <UserContext.Provider value={userProvider}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/home" element={<Home />}></Route>
          <Route path="/register" element={<Register />}></Route>
          <Route path="/likes" element={<LikesPage />}></Route>
          <Route path="/seller" element={<CrudSeller />}></Route>
          <Route
            path="/product/:productName"
            element={
              <ProductInfo
                basketOpen={isBasketOpen}
                onClickCart={() => setBasketOpen(true)}
              />
            }
          ></Route>
          <Route path="/payments" element={<Payments />}></Route>
        </Routes>
      </UserContext.Provider>
    </>
  );
}

export default App;
