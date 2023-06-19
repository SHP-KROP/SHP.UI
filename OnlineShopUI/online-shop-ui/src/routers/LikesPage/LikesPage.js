import React from 'react';
import useLikes from './hooks/useLikes';
import './LikesPage.scss';
import useBasketFilling from '../Home/Logic/Basket/hooks/useBasketFilling';
import useBasketHandlers from '../Home/Logic/Basket/hooks/useBasketHandlers';
import ProductCard from '../../components/Card/ProductCard';
import { Link } from 'react-router-dom';
import { makeStyles, createStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core';
const useStyles = makeStyles((theme) =>
  createStyles({
    formControl: {
      marginTop: theme.spacing(2),
      minWidth: 120,
    },
    submitButton: {
      marginTop: theme.spacing(2),
    },
  })
);
export default function LikesPage() {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] =
    useLikes();
  const [basket, setBasket] = useBasketFilling();
  const [handleClickAddInBasket] = useBasketHandlers({ basket, setBasket });

  const classes = useStyles();

  return (
    <div className="wrapper">
      <div className="likespage">
        {isLoading ? (
          <h1>Loading</h1>
        ) : (
          <>
            {likedProducts &&
              likedProducts.map((product) => (
                <div className="likeproduct">
                  <ProductCard
                    card={product}
                    handleClick={handleClickAddInBasket}
                    basket={basket}
                  />
                </div>
              ))}
          </>
        )}
      </div>
      <div className="back">
        <Button
          type="submit"
          variant="contained"
          color="primary"
          className={classes.submitButton}
        >
          <Link to="/" style={{ color: 'white' }}>
            Back to home
          </Link>
        </Button>
      </div>
    </div>
  );
}
