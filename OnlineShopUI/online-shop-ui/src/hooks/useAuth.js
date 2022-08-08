import { useContext } from 'react';
import { UserContext } from '../Contexts/UserContext';

export default function useAuth() {
  return useContext(UserContext);
}
