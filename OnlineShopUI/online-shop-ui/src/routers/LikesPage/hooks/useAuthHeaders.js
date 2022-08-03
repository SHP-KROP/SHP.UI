export default function useAuthHeaders() {
  let token = localStorage.getItem('token');

  return { headers: { Authorization: `Bearer ${token}` } };
}
