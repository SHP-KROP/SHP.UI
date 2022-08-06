import { useContext } from 'react';
import './Home.scss';
import Feedback from '../../components/FeedbackATop/Feedback';
import HeadBlock from '../../components/HeadBlock/HeadBlock';
import ProductCard from '../../components/Card/ProductCard';
import SideMenuList from '../../components/SideMenuList/SideMenuList';
import Banner from '../../components/Banner/Banner';
import useMenuFilling from '../Home/Logic/MenuLogic';
import useProductCardFilling from '../Home/Logic/ProductLogic';
import Basket from '../../components/Basket/Basket';
import { useState } from 'react';
import useBasketFilling from './Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from './Logic/Basket/hooks/useBasketHandlers';
import { UserContext } from '../../Helper/hook/UserContext';
import useAuth from '../../Helper/hook/useAuth';

function Home() {
  const [isBasketOpen, setBasketOpen] = useState(() => false);
  const menu = useMenuFilling();
  const productCards = useProductCardFilling();
  const { user, setUser } = useAuth();
  console.log(user);
  const [basket, setBasket] = useBasketFilling();
  const [
    handleClickAddInBasket,
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ] = useBasketHandlers({ basket, setBasket });

  return (
    <>
      <div className="MainPage">
        <Feedback />
        <Basket
          onClose={() => setBasketOpen(false)}
          opened={isBasketOpen}
          basket={basket}
          handleClickIncreaseBasketCount={handleClickIncreaseBasketCount}
          handleClickDecreaseBasketCount={handleClickDecreaseBasketCount}
          handleClickRemoveFromBasket={handleClickRemoveFromBasket}
        />
        <HeadBlock
          productsInBasketCount={basket.length}
          basketOpen={isBasketOpen}
          onClickCart={() => setBasketOpen(true)}
        />
        <div className="blocks">
          <div className="blockSideMenus">
            {menu.map((sideMenuListItem) => (
              <SideMenuList
                nameCategory={sideMenuListItem.nameCategory}
                subCategories={sideMenuListItem.subCategories}
              />
            ))}
          </div>
          <div className="blockBanners">
            <Banner />
            <Banner />
          </div>
          <div className="blockCards">
            {productCards.map((productCardItem) => (
              <ProductCard
                card={productCardItem}
                handleClick={handleClickAddInBasket}
                basket={basket}
              />
            ))}
          </div>
        </div>
      </div>
    </>
  );
}

export default Home;
