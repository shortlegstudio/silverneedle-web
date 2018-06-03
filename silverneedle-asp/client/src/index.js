import React from 'react';
import ReactDOM from 'react-dom';
import './style/semantic.min.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { makeMainRoutes } from './routes';

const routes = makeMainRoutes();

ReactDOM.render(routes, document.getElementById('root'));
registerServiceWorker();
