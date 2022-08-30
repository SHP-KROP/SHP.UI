import React from 'react';
import Header from '../../../components/HeaderFunctionality/Header';
import { TextField } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import InputAdornment from '@mui/material/InputAdornment';
import TableProducts from '../TableProducts/TableProducts';
import SellerForm from '../SellerForm/SellerForm';
import useModal from '../SellerForm/useModal/useModal';

export default function CrudSeller() {
  const { isShowing, toggle } = useModal();
  return (
    <div
      className="sellerPage"
      style={{ filter: isShowing ? 'blur(4px)' : 'none' }}
    >
      <Header />
      <div className="seller">
        <div className="seller__products">
          <div className="seller__rowfunc">
            <TextField
              label="Search"
              size="small"
              id="outlined-start-adornment"
              sx={{ m: 1, width: '20%' }}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <SearchIcon />
                  </InputAdornment>
                ),
              }}
            />
          </div>
          <SellerForm onClose={toggle} isShowing={isShowing} />
          <TableProducts setIsShowing={toggle} isShowing={isShowing} />
        </div>
      </div>
    </div>
  );
}
