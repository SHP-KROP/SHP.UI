import React from 'react';
import Modal from '@mui/material/Modal';
import Fade from '@mui/material/Fade';
import Backdrop from '@mui/material/Backdrop';
import { styled } from '@mui/material/styles';
import { TextField } from '@mui/material';
import Button from '@mui/material/Button';
import InputAdornment from '@mui/material/InputAdornment';
import UseHandlers from '../../../Helper/Handlers';
import CloseIcon from '@mui/icons-material/Close';
const StyledBackdrop = styled(Backdrop)(({ theme }) => ({
  backgroundColor: 'inherit',
}));
export default function SellerForm({ onClose, isFormOpen }) {
  const [handleModalClose] = UseHandlers();
  return (
    <div className="sellerPage">
      <Modal
        aria-labelledby="transition-modal-title"
        aria-describedby="transition-modal-description"
        open={isFormOpen}
        onClose={handleModalClose}
        closeAfterTransition
        BackdropComponent={StyledBackdrop}
        BackdropProps={{
          timeout: 500,
        }}
      >
        <Fade in={isFormOpen}>
          <div
            className="seller__form"
            style={{
              background: 'white',
              display: isFormOpen ? 'none' : 'absolute',
            }}
          >
            <form>
              <Button variant="contained" onClick={onClose}>
                Exit
              </Button>
              <div className="seller__productname">
                <TextField
                  fullWidth
                  required
                  id="outlined-required"
                  label="Name"
                />
              </div>
              <div className="seller__productdescrp">
                <TextField
                  fullWidth
                  required
                  id="outlined-required"
                  label="Description"
                />
              </div>
              <div className="seller__amountAndPrice">
                <TextField
                  required
                  label="Amount"
                  type="number"
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
                <TextField
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
              <div className="seller__acceptproduct">
                <Button type="submit" variant="contained">
                  Add a product
                </Button>
              </div>
            </form>
          </div>
        </Fade>
      </Modal>
    </div>
  );
}
