import CoreAPI from '../../../../API/CoreAPI';
export const deleteProduct = async (productName, authHeaders) => {
  try {
    await CoreAPI.delete(`/product/${productName}`, authHeaders);
  } catch (error) {
    throw new Error('Failed to delete product');
  }
};
