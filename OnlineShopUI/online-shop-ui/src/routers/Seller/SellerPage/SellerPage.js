// SellerPage.js

import React, { useState, useEffect } from 'react';
import {
  Table,
  TableContainer,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Paper,
} from '@mui/material';
import TablePagination from '@mui/material/TablePagination';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import SellerFormEdit from '../SellerForms/SellerFormEdit/SellerFormEdit';
import useAuthHeaders from '../../../hooks/useAuthHeaders';
import { deleteProduct } from '../SellerForms/deleteProduct';
import { editProduct } from '../SellerForms/editProduct';

const SellerPage = ({
  products,
  page,
  rowsPerPage,
  handleChangePage,
  handleChangeRowsPerPage,
}) => {
  const authHeaders = useAuthHeaders();
  const [sellerProducts, setSellerProducts] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [editProductId, setEditProductId] = useState(null);

  useEffect(() => {
    // Simulate loading delay
    setTimeout(() => {
      setSellerProducts([...products]);
      setIsLoading(false);
    }, 2000); // Adjust the delay time as needed
  }, [products]);

  const handleDelete = async (product) => {
    const { name } = product;
    if (window.confirm('Are you sure you want to delete this product?')) {
      try {
        // Call the deleteProduct API and update the state accordingly
        await deleteProduct(name, authHeaders);
        const updatedProducts = products.filter((p) => p.id !== product.id);
        setSellerProducts(updatedProducts);
      } catch (error) {
        console.error('Failed to delete product:', error);
      }
    }
  };

  const handleEdit = (product) => {
    setEditProductId(product.id);
  };

  const handleCancelEdit = () => {
    setEditProductId(null);
  };

  const handleUpdateProduct = async (updatedProduct) => {
    try {
      const { id } = updatedProduct;
      // Call the editProduct API and update the state accordingly
      await editProduct(id, updatedProduct, authHeaders);
      const updatedProducts = sellerProducts.map((product) =>
        product.id === id ? updatedProduct : product
      );
      setSellerProducts(updatedProducts);
      setEditProductId(null);
    } catch (error) {
      console.error('Failed to update product:', error);
    }
  };

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Description</TableCell>
              <TableCell>Amount</TableCell>
              <TableCell>Price</TableCell>
              <TableCell>Edit</TableCell>
              <TableCell>Delete</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {sellerProducts
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((product) => (
                <TableRow key={product.id}>
                  <TableCell>{product.name}</TableCell>
                  <TableCell>{product.description}</TableCell>
                  <TableCell>{product.amount}</TableCell>
                  <TableCell>{product.price}</TableCell>
                  <TableCell>
                    {editProductId === product.id ? (
                      <EditIcon style={{ cursor: 'pointer' }} />
                    ) : (
                      <EditIcon
                        onClick={() => handleEdit(product)}
                        style={{ cursor: 'pointer' }}
                      />
                    )}
                  </TableCell>
                  <TableCell>
                    <DeleteIcon
                      onClick={() => handleDelete(product)}
                      style={{ cursor: 'pointer' }}
                    />
                  </TableCell>
                </TableRow>
              ))}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[5, 10, 25]}
        component="div"
        count={sellerProducts.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />

      {editProductId && (
        <SellerFormEdit
          product={sellerProducts.find(
            (product) => product.id === editProductId
          )}
          onCancel={handleCancelEdit}
          onUpdate={handleUpdateProduct}
        />
      )}
    </div>
  );
};

export default SellerPage;
