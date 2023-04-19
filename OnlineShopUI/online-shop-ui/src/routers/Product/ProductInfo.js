import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import CoreAPI from '../../API/CoreAPI';
import HeadBlock from '../../components/HeadBlock/HeadBlock';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import OutlinedInput from '@mui/material/OutlinedInput';
import FormControl from '@mui/material/FormControl';
import './ProductInfo.scss';

export default function ProductInfo() {
  const params = useParams();
  const [product, setProduct] = useState();
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    setIsLoading(true);
    const fetchProduct = async () => {
      await CoreAPI.get(`/product/${params.productName}`).then((response) => {
        setProduct(response.data);
        setIsLoading(false);
      });
    };
    fetchProduct();
  }, [params.productName]);
  console.log(product);
  return (
    <>
      {isLoading ? (
        <div>Loading...</div>
      ) : (
        <div className="product">
          <HeadBlock />
          <div className="product__content">
            <div className="product__logo">
              <img src={product.photoUrl} alt="logo" />
            </div>
            <div className="product__info">
              <div className="product__name">{product.name}</div>
              <div className="product__data">
                <div className="product__description">
                  <p>Description:</p>
                  {product.description}
                </div>

                <div className="product__amount">
                  <p>Amount:</p>
                  <FormControl>
                    <OutlinedInput
                      className="product__amount-input"
                      id="outlined-adornment-amount"
                      defaultValue={product.amount}
                      inputProps={{
                        sx: { padding: '2%', textAlign: 'center' },
                      }}
                      readOnly
                      style={{ width: '10ch' }}
                    />
                  </FormControl>
                </div>
                <div className="product__price">
                  <p>Price:</p>
                  {product.price}$
                </div>
              </div>
              <div className="product__functionality">
                <div className="product__button-buy">
                  <Button variant="outlined" color="success">
                    <Link to={'/payments'} style={{ color: 'inherit' }}>
                      Buy
                    </Link>
                  </Button>
                </div>
                <div className="product__counter">
                  <Box
                    component="form"
                    sx={{
                      '& .MuiTextField-root': { m: 1, width: '25ch' },
                    }}
                    noValidate
                    autoComplete="off"
                  >
                    <TextField
                      id="outlined-number"
                      label="Quantity"
                      type="number"
                      inputProps={{
                        sx: { padding: '6%', textAlign: 'center' },
                      }}
                      InputLabelProps={{
                        shrink: true,
                      }}
                      style={{ width: '10ch' }}
                    />
                  </Box>
                </div>
                <div className="product__button-buy">
                  <Button variant="outlined" color="secondary">
                    Add to cart
                  </Button>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}
