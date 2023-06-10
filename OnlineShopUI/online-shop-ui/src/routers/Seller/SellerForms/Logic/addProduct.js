import CoreAPI from '../../../../API/CoreAPI';
export const addProduct = async (productData, authHeaders) => {
  try {
    const response = await CoreAPI.post('/product', productData, authHeaders);
    return response.data;
  } catch (error) {
    throw new Error('Failed to add product');
  }
};
