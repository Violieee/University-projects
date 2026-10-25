import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App.js';
import './styles/style.css';
import './styles/react.css';
createRoot(document.getElementById('root')).render(React.createElement(React.StrictMode, null,
    React.createElement(App, null)));
