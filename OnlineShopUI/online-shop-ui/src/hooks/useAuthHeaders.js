import useAuth from './useAuth';

export default function useAuthHeaders() {
  const { user } = useAuth();

  return { headers: { Authorization: user ? `Bearer ${user.token}` : null } };
}
