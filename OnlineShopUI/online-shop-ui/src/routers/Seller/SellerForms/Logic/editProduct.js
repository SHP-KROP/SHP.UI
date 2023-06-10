import CoreAPI from '../../../../API/CoreAPI';

export const editProduct = async (productId, productData, authHeaders) => {
  try {
    const response = await CoreAPI.put(
      `/product/${productId}`,
      productData,
      authHeaders
    );
    return response.data;
  } catch (error) {
    throw new Error('Failed to edit product');
  }
};
