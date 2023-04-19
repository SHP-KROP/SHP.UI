import React from 'react';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Backdrop from '@mui/material/Backdrop';
import { styled } from '@mui/material/styles';
import { TextField } from '@mui/material';
import Button from '@mui/material/Button';
import InputAdornment from '@mui/material/InputAdornment';
import CloseIcon from '@mui/icons-material/Close';
import IconButton from '@mui/material/IconButton';
import AddProduct from '../TableProducts/TableFunctional/AddProduct';
import { useForm } from 'react-hook-form';
import Switch from '@mui/material/Switch';
import FormControlLabel from '@mui/material/FormControlLabel';

const StyledBackdrop = styled(Backdrop)(({ theme }) => ({
  backgroundColor: 'inherit',
}));

export default function SellerForm({ onClose, isShowing }) {
  const { register, getValues } = useForm();
  const onAddProduct = (value) => {
    console.log(JSON.stringify(value));
  };
  return isShowing ? (
    <div className="sellerPage">
      <Modal
        aria-labelledby="transition-modal-title"
        aria-describedby="transition-modal-description"
        open={isShowing}
        onClose={onClose}
        closeAfterTransition
        BackdropComponent={StyledBackdrop}
        BackdropProps={{
          timeout: 500,
        }}
      >
        <Fade in={isShowing}>
          <div
            className="seller__form"
            style={{
              background: 'white',
            }}
          >
            <form>
              <div className="seller__productname">
                <TextField
                  {...register('name')}
                  fullWidth
                  required
                  id="outlined-required"
                  label="Name"
                />
              </div>
              <div className="seller__productdescrp">
                <TextField
                  {...register('description')}
                  fullWidth
                  id="outlined-required"
                  label="Description"
                />
              </div>
              <div className="seller__amountAndPrice">
                <TextField
                  {...register('amount')}
                  required
                  label="Amount"
                  type="number"
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
                <TextField
                  {...register('price')}
                  required
                  label="Price"
                  type="number"
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">$</InputAdornment>
                    ),
                  }}
                />
              </div>
              <div
                className="seller__acceptproduct"
                style={{ display: 'flex', justifyContent: 'space-around' }}
              >
                <Button
                  type="submit"
                  variant="contained"
                  onClick={() => {
                    const value = getValues();
                    value.isAvailable = false;
                    onAddProduct(value);
                  }}
                >
                  Add a product
                </Button>

                <FormControlLabel control={<Switch />} label="Available" />
              </div>
            </form>
            <div className="seller__formclose" style={{ alignSelf: 'start' }}>
              <IconButton color="primary" component="label" onClick={onClose}>
                <CloseIcon />
              </IconButton>
            </div>
          </div>
        </Fade>
      </Modal>
    </div>
  ) : null;
}
