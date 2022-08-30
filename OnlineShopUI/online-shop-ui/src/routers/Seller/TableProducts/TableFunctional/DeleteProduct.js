import CoreAPI from '../../../../API/CoreAPI';
import useAuthHeaders from '../../../../hooks/useAuthHeaders';

export default function useDeleteProduct() {
  const authHeaders = useAuthHeaders();

  return (name) => {
    CoreAPI.delete(`/product/${name}`, authHeaders);
  };
}
