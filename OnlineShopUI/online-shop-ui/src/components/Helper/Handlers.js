import { useState } from 'react';
const Handlers = () => {
  const [isOpen, setOpen] = useState(() => false);
  const handleLogInModalOpen = () => setOpen(true);
  const handleLogInModalClose = () => setOpen(false);

  return [handleLogInModalClose, handleLogInModalOpen, isOpen];
};
export default Handlers;
