import React, { useState } from 'react';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import Button from '@mui/material/Button';
import { columnsTable } from '../../../components/mock/data';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import { styled } from '@mui/material/styles';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import useSellerProduct from '../useSellerProduct/useSellerProduct';
import useDeleteProduct from './TableFunctional/DeleteProduct';
import useAddProduct from './TableFunctional/AddProduct';
import '../CRUD/CrudSeller.scss';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.primary.dark,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 15,
  },
}));
const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));

export default function TableProducts({ isShowing, setIsShowing }) {
  const [page, setPage] = useState(0);
  const [sellerProducts, setSellerProducts] = useSellerProduct();
  const [rowsPerPage, setRowsPerPage] = useState(10);

  const deleteProduct = useDeleteProduct();
  const addProduct = useAddProduct();

  const onDeleteProduct = (name) => {
    deleteProduct(name);
    setSellerProducts(sellerProducts.filter((x) => x.name !== name));
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  return (
    <Paper sx={{ width: '100%', overflow: 'hidden' }}>
      <TableContainer sx={{ maxHeight: 500 }}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {columnsTable.map((column) => (
                <StyledTableCell
                  key={column.id}
                  align={column.align}
                  style={{ minWidth: column.minWidth }}
                >
                  {column.label}
                </StyledTableCell>
              ))}
              <TableCell
                className="seller__addproduct"
                style={{ padding: 0 }}
                size="small"
              >
                <Button
                  fullWidth
                  variant="contained"
                  sx={{ padding: '8%' }}
                  onClick={setIsShowing}
                >
                  Add a product
                </Button>
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {sellerProducts.map((card) => {
              return (
                <StyledTableRow
                  hover
                  role="checkbox"
                  tabIndex={-1}
                  key={card.code}
                >
                  {columnsTable.map((column) => {
                    const value = card[column.id];
                    return (
                      <TableCell key={column.id} align={column.align}>
                        {column.format && typeof value === 'number'
                          ? column.format(value)
                          : value}
                      </TableCell>
                    );
                  })}
                  <div className="seller__productfunc">
                    <IconButton
                      color="primary"
                      component="label"
                      onClick={setIsShowing}
                    >
                      <EditIcon />
                    </IconButton>
                    <IconButton
                      color="primary"
                      component="label"
                      onClick={() => onDeleteProduct(card.name)}
                    >
                      <DeleteIcon />
                    </IconButton>
                  </div>
                </StyledTableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </Paper>
  );
}
