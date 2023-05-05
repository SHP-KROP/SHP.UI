import './Home.scss';
import HeadBlock from '../../components/HeadBlock/HeadBlock';
import ProductCard from '../../components/Card/ProductCard';
import SideMenuList from '../../components/SideMenuList/SideMenuList';
import useMenuFilling from './Logic/MenuLogic';
import useProductCardFilling from './Logic/ProductLogic';
import Basket from '../../components/Basket/Basket';
import { useState } from 'react';
import useBasketFilling from './Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from './Logic/Basket/hooks/useBasketHandlers';
import ReactPaginate from 'react-paginate';
import SkipNextIcon from '@mui/icons-material/SkipNext';
import SkipPreviousIcon from '@mui/icons-material/SkipPrevious';

function Home() {
  const [isBasketOpen, setBasketOpen] = useState(() => false);
  const productCards = useProductCardFilling();
  const [basket, setBasket] = useBasketFilling();
  const [
    handleClickAddInBasket,
    handleClickRemoveFromBasket,
    handleClickIncreaseBasketCount,
    handleClickDecreaseBasketCount,
  ] = useBasketHandlers({ basket, setBasket });
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
            <SideMenuList />
          </div>
          <div className="blockCards">
            <div className="pagination">
              <ReactPaginate
                previousLabel={<SkipPreviousIcon />}
                nextLabel={<SkipNextIcon />}
                breakLabel={'...'}
                pageCount={pageCount}
                marginPagesDisplayed={2}
                pageRangeDisplayed={5}
                onPageChange={handlePageClick}
                containerClassName={'pagination'}
                activeClassName={'active'}
              />
            </div>
            <div className="cards">
              {currentPageProducts.map((productCardItem) => (
                <ProductCard
                  key={productCardItem.id}
                  card={productCardItem}
                  handleClick={handleClickAddInBasket}
                  basket={basket}
                />
              ))}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Home;
