import { useQuery } from '@apollo/client/react'
import { gql } from '@apollo/client/core'
import { Link } from 'react-router-dom';

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

interface DashboardVariables {
  agencyId: number;
}

function Dashboard() {
  const { loading, error, data } = useQuery<DashboardData, DashboardVariables>(GET_DASHBOARD_DATA, {
    variables: { agencyId: -1 },
    fetchPolicy: 'cache-and-network' 
  });

  if (loading) return <div style={{ padding: '2rem' }}>Cargando catálogo B2B...</div>;
  if (error) return <div style={{ padding: '2rem', color: 'red' }}>Error: {error.message}</div>;
  
  // Blindaje 1: Prevenir crash si el caché de Apollo devuelve la estructura rota
  if (!data || !data.products || !data.agencies) {
      return <div style={{ padding: '2rem' }}>Estructura de datos no disponible.</div>;
  }

  // Blindaje 2: Optional Chaining por si la agencia -1 dejó de existir en SQL
  const adminName = data.agencies.find(a => a.id === -1)?.name || 'Administración Desconocida';

  return (
    <div style={{ padding: '2rem', fontFamily: 'system-ui', maxWidth: '800px', margin: '0 auto' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h1>Portal B2B - {adminName}</h1>
        <Link to="/admin" style={{ padding: '0.5rem 1rem', background: '#3b82f6', color: 'white', textDecoration: 'none', borderRadius: '4px' }}>
          + Nuevo Paquete
        </Link>
      </div>
      <hr style={{ marginBottom: '2rem' }} />
      
      <h2>Catálogo de Venta</h2>
      <div style={{ display: 'grid', gap: '1.5rem' }}>
        {data.products.length === 0 ? (
           <p style={{ color: '#64748b' }}>No hay paquetes turísticos registrados.</p>
        ) : (
          data.products.map(p => (
            <div key={p.id} style={{ border: '1px solid #e2e8f0', padding: '1.5rem', borderRadius: '8px', boxShadow: '0 4px 6px -1px rgb(0 0 0 / 0.1)' }}>
              <h3 style={{ margin: '0 0 1rem 0' }}>{p.name}</h3>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <p style={{ margin: 0, color: '#64748b' }}>
                  {/* Blindaje 3: Evitar crash en el toFixed si el precio viene nulo */}
                  Precio Público: <s>${(p.basePrice ?? 0).toFixed(2)}</s>
                </p>
                <p style={{ margin: 0, color: '#16a34a', fontWeight: 'bold', fontSize: '1.25rem' }}>
                  Tu Precio (B2B): ${(p.b2bPrice ?? 0).toFixed(2)}
                </p>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  )
}

export default Dashboard