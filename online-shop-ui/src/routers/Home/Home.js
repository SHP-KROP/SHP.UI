import './Home.scss';
import Feedback from '../../components/FeedbackATop/Feedback';
import HeadBlock from '../../components/HeadBlock/HeadBlock';
import ProductCard from '../../components/Card/ProductCard';
import SideMenuList from '../../components/SideMenuList/SideMenuList';
import useProductCardFilling from './Logic/ProductLogic';
import Basket from '../../components/Basket/Basket';
import { useState } from 'react';
import useBasketFilling from './Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from './Logic/Basket/hooks/useBasketHandlers';

function Home() {
  const [isBasketOpen, setBasketOpen] = useState(() => false);
  const menu = useMenuFilling();
  const productCards = useProductCardFilling();

  const [basket, setBasket] = useBasketFilling();
  const [
    handleClickAddInBasket,
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ] = useBasketHandlers({ basket, setBasket });
  console.log(basket);
  const [currentPage, setCurrentPage] = useState(0);

  const handlePageClick = ({ selected }) => {
    setCurrentPage(selected);
  };
  const productPerPage = 6;
  const pageCount = Math.ceil(productCards.length / productPerPage);
  const offset = currentPage * productPerPage;

  const currentPageProducts = productCards.slice(
    offset,
    offset + productPerPage
  );

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
