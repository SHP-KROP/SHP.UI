import { useState, useMemo } from 'react';

export default function useAuthProvider() {
  const [user, setUser] = useState(JSON.parse(localStorage.getItem('user')));
  const userProvider = useMemo(() => ({ user, setUser }), [user, setUser]);

  return userProvider;
}
