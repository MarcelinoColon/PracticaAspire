import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
// Importaciones separadas para v4
import { ApolloClient, InMemoryCache, HttpLink } from '@apollo/client/core'
import { ApolloProvider } from '@apollo/client/react'
import App from './App.tsx'

const graphqlUrl = import.meta.env.VITE_GRAPHQL_URL || 'https://localhost:7122';

// Configuración estricta de v4 usando HttpLink
const client = new ApolloClient({
  link: new HttpLink({ uri: `${graphqlUrl}/graphql` }),
  cache: new InMemoryCache(),
});

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ApolloProvider client={client}>
      <App />
    </ApolloProvider>
  </StrictMode>,
)