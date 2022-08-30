import CoreAPI from '../../../../API/CoreAPI';
import useAuthHeaders from '../../../../hooks/useAuthHeaders';

export default function AddProduct(value) {
  const authHeaders = useAuthHeaders();
  console.warn(value);
  CoreAPI.post('/product', {}, authHeaders);
}
