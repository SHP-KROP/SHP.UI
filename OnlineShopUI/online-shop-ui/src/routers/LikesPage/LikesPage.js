import React from 'react';
import useLikes from './hooks/useLikes';

export default function LikesPage() {
  const [isLoading, likedProducts] = useLikes();

  return <div className="likesproduct">{isLoading ? <h1>Loading</h1> : likedProducts.map(product => <p>{JSON.stringify(product)}</p>)}</div>;
}
