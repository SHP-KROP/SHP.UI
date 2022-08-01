import React from 'react';
import useLikes from './hooks/useLikes';

export default function LikesPage() {
  const [isLoading, likedProducts, likeProductById, unlikeProductById] = useLikes();

  return (
    <div className="likesproduct">
      {isLoading ? (
        <h1>Loading</h1>
      ) : (
        likedProducts && likedProducts.map((product) => <p>{JSON.stringify(product)}</p>)
      )}
      <button onClick={() => likeProductById(1)}>LIKE PRODUCT</button>
      <button onClick={() => unlikeProductById(1)}>UNLIKE PRODUCT</button>
    </div>
  );
}
