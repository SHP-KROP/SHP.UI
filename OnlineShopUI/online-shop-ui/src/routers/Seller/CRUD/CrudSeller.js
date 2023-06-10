import React, { useState } from 'react';
import { TextField, Button } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import InputAdornment from '@mui/material/InputAdornment';
import SellerFormCreate from '../SellerForms/SellerFormCreate/SellerFormCreate';
import SellerPage from '../SellerPage/SellerPage';
import useSellerProduct from '../useSellerProduct/useSellerProduct';
import './CrudSeller.scss';
import { addProduct } from '../SellerForms/Logic/addProduct';
import SellerFormEdit from '../SellerForms/SellerFormEdit/SellerFormEdit';
import useAuthHeaders from '../../../hooks/useAuthHeaders';

export default function CrudSeller() {
  const [sellerProducts, setSellerProducts] = useSellerProduct();
  const [searchTerm, setSearchTerm] = useState('');
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [isEditFormOpen, setIsEditFormOpen] = useState(false);
  const [isCreateFormOpen, setIsCreateFormOpen] = useState(false);
  const authHeaders = useAuthHeaders();

  // Filter the products based on the search term
  const filteredProducts = sellerProducts.filter((product) =>
    product.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  // Function to handle search input change
  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  // Function to handle page change
  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  // Function to handle rows per page change
  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  // Function to handle closing the edit form
  const handleCloseEditForm = () => {
    setIsEditFormOpen(false);
  };

  // Function to handle opening the create form
  const handleOpenCreateForm = () => {
    setIsCreateFormOpen(true);
  };

  // Function to handle closing the create form
  const handleCloseCreateForm = () => {
    setIsCreateFormOpen(false);
  };
  // Function to handle adding a new product
  const handleAddProduct = async (productData) => {
    try {
      // Call the addProduct API and update the state accordingly
      const newProduct = await addProduct(productData, authHeaders);
      setSellerProducts([...sellerProducts, newProduct]);
    } catch (error) {
      console.error('Failed to add product:', error);
    }
  };

  return (
    <div className="sellerPage">
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
              value={searchTerm}
              onChange={handleSearchChange}
            />
            <Button variant="contained" onClick={handleOpenCreateForm}>
              Create
            </Button>
          </div>
          <SellerPage
            products={filteredProducts}
            page={page}
            rowsPerPage={rowsPerPage}
            handleChangePage={handleChangePage}
            handleChangeRowsPerPage={handleChangeRowsPerPage}
          />
          {isEditFormOpen && (
            <SellerFormEdit
              onClose={handleCloseEditForm}
              isShowing={true}
              setSellerProducts={handleAddProduct}
            />
          )}
          {isCreateFormOpen && (
            <SellerFormCreate
              setSellerProducts={handleAddProduct}
              onClose={handleCloseCreateForm}
            />
          )}
        </div>
      </div>
    </div>
  );
}
