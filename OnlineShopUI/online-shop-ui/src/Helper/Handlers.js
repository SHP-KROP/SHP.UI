import { useState } from 'react';
const Handlers = () => {
  const [isOpen, setOpen] = useState(() => false);
  const handleModalOpen = () => setOpen(true);
  const handleModalClose = () => setOpen(false);

  return [handleModalClose, handleModalOpen, isOpen];
};
export default Handlers;
