import { useQuery } from '@apollo/client/react'
import { gql } from '@apollo/client/core'

const GET_DASHBOARD_DATA = gql`
  query GetDashboardData($agencyId: Int!) {
    products {
      id
      name
      basePrice
      b2bPrice(agencyId: $agencyId)
    }
    agencies {
      id
      name
    }
  }
`;

// 1. Definimos la estructura exacta que nos devuelve GraphQL
interface DashboardData {
  products: Array<{
    id: string;
    name: string;
    basePrice: number;
    b2bPrice: number;
  }>;
  agencies: Array<{
    id: number;
    name: string;
  }>;
}

// 2. Definimos la estructura de las variables que enviamos
interface DashboardVariables {
  agencyId: number;
}

function App() {
  // 3. Inyectamos las interfaces al hook (TData y TVariables)
  const { loading, error, data } = useQuery<DashboardData, DashboardVariables>(GET_DASHBOARD_DATA, {
    variables: { agencyId: -1 }
  });

  if (loading) return <div style={{ padding: '2rem' }}>Cargando catálogo B2B...</div>;
  if (error) return <div style={{ padding: '2rem', color: 'red' }}>Error: {error.message}</div>;
  
  // Seguridad extra para TypeScript: garantizamos que data no sea undefined
  if (!data) return <div style={{ padding: '2rem' }}>No hay datos disponibles.</div>;

  // 4. Ahora TypeScript conoce exactamente las propiedades y autocompleta
  const adminName = data.agencies.find(a => a.id === -1)?.name;

  return (
    <div style={{ padding: '2rem', fontFamily: 'system-ui', maxWidth: '800px', margin: '0 auto' }}>
      <h1>Portal B2B - {adminName}</h1>
      <hr style={{ marginBottom: '2rem' }} />
      
      <h2>Catálogo de Venta</h2>
      <div style={{ display: 'grid', gap: '1.5rem' }}>
        {data.products.map(p => (
          <div key={p.id} style={{ border: '1px solid #e2e8f0', padding: '1.5rem', borderRadius: '8px', boxShadow: '0 4px 6px -1px rgb(0 0 0 / 0.1)' }}>
            <h3 style={{ margin: '0 0 1rem 0' }}>{p.name}</h3>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <p style={{ margin: 0, color: '#64748b' }}>
                Precio Público: <s>${p.basePrice.toFixed(2)}</s>
              </p>
              <p style={{ margin: 0, color: '#16a34a', fontWeight: 'bold', fontSize: '1.25rem' }}>
                Tu Precio (B2B): ${p.b2bPrice.toFixed(2)}
              </p>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

export default App