import React from 'react';
import { useForm } from 'react-hook-form';
import { TextField, Button, Switch, FormControlLabel } from '@mui/material';
import './SellerFormCreate.scss';
export default function SellerFormCreate({ onCancel, setSellerProducts }) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();

  // Function to handle form submission
  const onSubmit = (data) => {
    setSellerProducts(data);
  };

  return (
    <div className="sellerForm">
      <form onSubmit={handleSubmit(onSubmit)} className="seller-form">
        <TextField
          label="Name"
          {...register('name', { required: true })}
          error={errors.name ? true : false}
          helperText={errors.name ? 'Name is required' : ''}
        />
        <TextField
          label="Description"
          {...register('description', { required: true })}
          error={errors.description ? true : false}
          helperText={errors.description ? 'Description is required' : ''}
        />
        <TextField
          label="Amount"
          type="number"
          {...register('amount', { required: true, min: 0 })}
          error={errors.amount ? true : false}
          helperText={errors.amount ? 'Amount is required' : ''}
          inputProps={{ min: 0 }}
        />
        <TextField
          label="Price"
          type="number"
          {...register('price', {
            required: true,
            pattern: /^[0-9]+$/,
            min: 0,
          })}
          error={errors.price ? true : false}
          helperText={
            errors.price ? 'Price is required and must be a numeric value' : ''
          }
          inputProps={{ min: 0 }}
        />
        <FormControlLabel
          control={<Switch {...register('isAvailable')} color="primary" />}
          label="Available"
        />
        <div className="seller-form-button">
          <Button variant="contained" type="submit">
            Add Product
          </Button>
          <Button variant="contained" onClick={onCancel}>
            Cancel
          </Button>
        </div>
      </form>
    </div>
  );
}
