import { createRoot } from 'react-dom/client';

// third party
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';

// project imports
import App from 'App';
import * as serviceWorker from 'serviceWorker';
import { store } from 'store';

// style + assets
import 'assets/scss/style.scss';
import config from './config';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
// ==============================|| REACT DOM RENDER  ||============================== //

const queryClient = new QueryClient()


const container = document.getElementById('root');
const root = createRoot(container); // createRoot(container!) if you use TypeScript
root.render(
  <Provider store={store}>
    <BrowserRouter basename={config.basename}>
        <QueryClientProvider client={queryClient}>
            <App />
        </QueryClientProvider>
    
    </BrowserRouter>
  </Provider>
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
