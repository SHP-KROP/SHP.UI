import React, { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { TextField, Button } from '@mui/material';
import './SellerFormEdit.scss';
const SellerFormEdit = ({ product, onCancel, onUpdate }) => {
  const { register, handleSubmit, setValue } = useForm();

  const onSubmit = (data) => {
    const updatedProduct = {
      ...product,
      ...data,
    };
    onUpdate(updatedProduct);
  };

  useEffect(() => {
    setValue('name', product.name);
    setValue('description', product.description);
    setValue('amount', product.amount);
    setValue('price', product.price);
  }, [product, setValue]);

  return (
    <div className="seller-form-edit-container">
      <h2>Edit Product</h2>
      <form onSubmit={handleSubmit(onSubmit)} className="seller-form">
        <TextField label="Name" {...register('name')} fullWidth />
        <TextField label="Description" {...register('description')} fullWidth />
        <TextField
          label="Amount"
          {...register('amount', { min: 0 })}
          fullWidth
          inputProps={{ min: 0 }}
        />
        <TextField
          label="Price"
          {...register('price', { min: 0 })}
          fullWidth
          inputProps={{ min: 0, step: '0.01' }}
        />
        <div className="seller-form-button">
          <Button type="submit" variant="contained">
            Update
          </Button>
          <Button variant="contained" onClick={onCancel}>
            Cancel
          </Button>
        </div>
      </form>
    </div>
  );
};

export default SellerFormEdit;
