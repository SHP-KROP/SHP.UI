import CoreAPI from '../../../../API/CoreAPI';
import useAuthHeaders from '../../../../hooks/useAuthHeaders';

export default function AddProduct(value) {
  const authHeaders = useAuthHeaders();
  CoreAPI.post('/product', {}, authHeaders);
}
