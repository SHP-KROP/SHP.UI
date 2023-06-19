import React, { useState } from 'react';
import ProductCard from '../../components/Card/ProductCard';
import SideMenuList from '../../components/SideMenuList/SideMenuList';
import useProductCardFilling from './Logic/ProductLogic';
import ReactPaginate from 'react-paginate';
import SkipNextIcon from '@mui/icons-material/SkipNext';
import SkipPreviousIcon from '@mui/icons-material/SkipPrevious';
import SearchIcon from '@mui/icons-material/Search';
import {
  Search,
  SearchIconWrapper,
  StyledInputBase,
} from './SearchStyles/SearchStyle';
import TextField from '@mui/material/TextField';
import './Home.scss';
import { useForm } from 'react-hook-form';

function Home({
  basket,
  handleClickAddInBasket,
  handleClickRemoveFromBasket,
  setBasket,
}) {
  const {
    register,
    watch,
    setValue,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [currentPage, setCurrentPage] = useState(0);
  const [searchTerm, setSearchTerm] = useState('');
  const productCards = useProductCardFilling();
  const productPerPage = 8;
  const offset = currentPage * productPerPage;
  const searchValue = watch('searchTerm', '');
  const minPrice = watch('minPrice', '');
  const maxPrice = watch('maxPrice', '');
  const filteredProductCards = productCards.filter((productCardItem) => {
    const nameMatch = productCardItem.name
      .toLowerCase()
      .includes(searchValue.toLowerCase());

    const priceMatch =
      (minPrice === '' || productCardItem.price >= Number(minPrice)) &&
      (maxPrice === '' || productCardItem.price <= Number(maxPrice));
    return nameMatch && priceMatch;
  });
  const pageCount = Math.ceil(filteredProductCards.length / productPerPage);

  const currentPageProducts = filteredProductCards.slice(
    offset,
    offset + productPerPage
  );

  const handlePageClick = ({ selected }) => {
    setCurrentPage(selected);
  };

  const handleSearch = (data) => {
    setSearchTerm(data.searchTerm);
    setCurrentPage(0);
  };

  return (
    <>
      <div className="MainPage">
        <div className="blocks">
          <div className="blockSideMenus">
            <SideMenuList />
          </div>
          <div className="blockCards">
            <div className="search">
              <form
                onSubmit={handleSubmit(handleSearch)}
                style={{
                  width: '50%',
                  display: 'flex',
                  flexDirection: 'column',
                  alignItems: 'center',
                }}
              >
                <Search>
                  <SearchIconWrapper>
                    <SearchIcon />
                  </SearchIconWrapper>
                  <StyledInputBase
                    placeholder="Search…"
                    inputProps={{ 'aria-label': 'search' }}
                    {...register('searchTerm')}
                    value={searchValue}
                    onChange={(e) => setValue('searchTerm', e.target.value)}
                  />
                </Search>
                <div
                  className="filter"
                  style={{ display: 'flex', gap: '5%', marginTop: '2%' }}
                >
                  <TextField
                    id="minPrice"
                    type="number"
                    placeholder="Min Price"
                    InputLabelProps={{
                      shrink: true,
                      min: 0,
                    }}
                    {...register('minPrice', {
                      min: {
                        value: 0,
                        message: 'Price must be positive',
                      },
                    })}
                    InputProps={{ inputProps: { min: 0 } }}
                    error={!!errors.minPrice}
                    helperText={errors.minPrice?.message}
                  />
                  <TextField
                    id="maxPrice"
                    type="number"
                    placeholder="Max Price"
                    InputLabelProps={{
                      shrink: true,
                    }}
                    {...register('maxPrice', {
                      min: {
                        value: 0,
                        message: 'Price must be positive',
                      },
                    })}
                    InputProps={{ inputProps: { min: 0 } }}
                    error={!!errors.maxPrice}
                    helperText={errors.maxPrice?.message}
                  />
                </div>
              </form>
            </div>

            {searchTerm && currentPageProducts.length === 0 ? (
              <div className="noResults">
                No products found with the given name.
              </div>
            ) : (
              <>
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
                      basket={basket}
                      key={productCardItem.id}
                      card={productCardItem}
                      addToBasket={handleClickAddInBasket}
                      handleClickRemoveFromBasket={handleClickRemoveFromBasket}
                    />
                  ))}
                </div>
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );
}

export default Home;
