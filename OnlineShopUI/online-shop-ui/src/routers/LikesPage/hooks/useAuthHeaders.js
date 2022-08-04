export default function useAuthHeaders() {
  let token = localStorage.getItem('token');

  return { headers: { Authorization: token ? `Bearer ${token}` : null } };
}
