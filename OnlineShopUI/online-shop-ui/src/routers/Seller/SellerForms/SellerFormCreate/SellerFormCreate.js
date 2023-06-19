import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import {
  TextField,
  Button,
  Switch,
  FormControlLabel,
  InputLabel,
  Input,
} from '@mui/material';
import './SellerFormCreate.scss';
import CoreAPI from '../../../../API/CoreAPI';
export default function SellerFormCreate({ onCancel, setSellerProducts }) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [photo, setPhoto] = useState(null);
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  const { token } = user;
  const config = {
    headers: {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'multipart/form-data',
    },
  };
  // Function to handle form submission
  const onSubmit = (data) => {
    setSellerProducts(data);
    const formData = new FormData();
    if (photo) {
      formData.append('photo', photo);
    }
    CoreAPI.post('/UserProfile/photo-to-product', formData, config);
  };
  const handlePhotoChange = (event) => {
    const selectedPhoto = event.target.files[0];
    setPhoto(selectedPhoto);
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
        <InputLabel htmlFor="photo-input">Photo</InputLabel>
        <Input
          id="photo-input"
          type="file"
          {...register('photo')}
          onChange={handlePhotoChange}
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
