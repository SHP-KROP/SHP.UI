import { useState } from 'react';

const UseHandlers = () => {
  const [isOpen, setOpen] = useState(() => false);
  const handleModalOpen = () => setOpen(true);
  const handleModalClose = () => setOpen(false);

  return [handleModalClose, handleModalOpen, isOpen, setOpen];
};
export default UseHandlers;
