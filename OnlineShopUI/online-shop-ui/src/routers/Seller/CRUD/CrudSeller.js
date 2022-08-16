import React from 'react';
import Header from '../../../components/HeaderFunctionality/Header';
import UseHandlers from '../../../Helper/Handlers';
import { TextField } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import InputAdornment from '@mui/material/InputAdornment';
import TableProducts from '../TableProducts/TableProducts';
import SellerForm from '../SellerForm/SellerForm';
export default function CrudSeller() {
  const [isOpen, setOpen] = UseHandlers();
  return (
    <div className="sellerPage">
      <Header />
      <div className="seller">
        <div className="seller__products">
          <SellerForm onClose={() => setOpen(false)} isFormOpen={isOpen} />
          <TableProducts
            onClickChange={() => setOpen(true)}
            isFormOpen={isOpen}
          />

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
        </div>
      </div>
    </div>
  );
}
