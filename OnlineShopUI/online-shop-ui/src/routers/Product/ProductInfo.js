import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import CoreAPI from '../../API/CoreAPI';
import Button from '@mui/material/Button';
import OutlinedInput from '@mui/material/OutlinedInput';
import FormControl from '@mui/material/FormControl';
import './ProductInfo.scss';
import useBasketHandlers from '../Home/Logic/Basket/hooks/useBasketHandlers';
import useBasketFilling from '../Home/Logic/Basket/hooks/useBasketFilling';

export default function ProductInfo() {
  const params = useParams();
  const [product, setProduct] = useState();
  const [isLoading, setIsLoading] = useState(true);
  const [basket, setBasket] = useBasketFilling();
  const [handleClickAddInBasket] = useBasketHandlers({ basket, setBasket });
  const [isAddedToCart, setIsAddedToCart] = useState(false);

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

  useEffect(() => {
    const basketItems = JSON.parse(localStorage.getItem('basket')) || [];
    if (basketItems && basketItems.some((item) => item.id === product?.id)) {
      setIsAddedToCart(true);
    }
  }, [product]);

  const handleAddToCart = (product) => {
    handleClickAddInBasket(product);
    setIsAddedToCart(true);
    const basketItems = JSON.parse(localStorage.getItem('basket')) || [];
    localStorage.setItem('basket', JSON.stringify([...basketItems, product]));
  };

  return (
    <>
      {isLoading ? (
        <div>Loading...</div>
      ) : (
        <div className="product">
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
                  <Button variant="outlined" color="secondary">
                    <Link to={`/`}>Back</Link>
                  </Button>
                  <Button
                    variant="outlined"
                    color="secondary"
                    onClick={() => handleAddToCart(product)}
                    disabled={isAddedToCart}
                  >
                    {isAddedToCart ? 'Added to cart' : 'Add to cart'}
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
