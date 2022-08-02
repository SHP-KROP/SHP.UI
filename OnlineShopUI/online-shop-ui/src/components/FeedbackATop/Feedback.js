import React from 'react';
import './Feedback.scss';

const Feedback = () => {
  return (
    <div className="feedback">
      <div className="feedback__contact">
        <ul>
          <li>
            <a href="#">Chat with us</a>
          </li>
          <li>+420 336 775 664</li>
          <li>info@freshnesecom.com</li>
        </ul>
        <ul>
          <li>
            <a href="#">Blog</a>
          </li>
          <li>
            <a href="#">About Us</a>
          </li>
          <li>
            <a href="#">Careers</a>
          </li>
        </ul>
      </div>
      <hr />
    </div>
  );
};

export default Feedback;
