import { Route, Routes } from 'react-router-dom';
import Register from './routers/Registration/Register';
import Home from './routers/Home/Home';
import LikesPage from './routers/LikesPage/LikesPage';
import { UserContext } from './Contexts/UserContext';
import useAuthProvider from './hooks/useAuthProvider';
import CrudSeller from './routers/Seller/CRUD/CrudSeller';
import ProductInfo from './routers/Product/ProductInfo';
import PaymentForm from './routers/Payment/PaymentForm.tsx';
import HeadBlock from './components/HeadBlock/HeadBlock';
function App() {
  const userProvider = useAuthProvider();

  return (
    <>
      <HeadBlock />
      <UserContext.Provider value={userProvider}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/register" element={<Register />}></Route>
          <Route path="/likes" element={<LikesPage />}></Route>
          <Route path="/seller" element={<CrudSeller />}></Route>
          <Route path="/product/:productName" element={<ProductInfo />}></Route>
          <Route path="/payment" element={<PaymentForm />}></Route>
        </Routes>
      </UserContext.Provider>
    </>
  );
}

export default App;
