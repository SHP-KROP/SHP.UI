import { useContext } from 'react';
import { UserContext } from './UserContext';

export default function useAuth() {
  return useContext(UserContext);
}
