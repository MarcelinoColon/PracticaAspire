import { useMutation } from '@apollo/client/react';
import { gql } from '@apollo/client/core';
import { useForm } from 'react-hook-form';
import { useNavigate, Link } from 'react-router-dom';

// Pedimos los datos completos tras crear para no romper el caché
const CREATE_PRODUCT = gql`
  mutation CreateProduct($input: CreateTravelProductInput!) {
    createProduct(input: $input) {
      id
      name
      basePrice
      category
    }
  }
`;

interface ProductForm {
  name: string;
  category: string;
  basePrice: number;
}

function Admin() {
  const navigate = useNavigate();
  const { register, handleSubmit, formState: { errors } } = useForm<ProductForm>();
  
  const [createProduct, { loading, error }] = useMutation(CREATE_PRODUCT, {
    refetchQueries: ['GetDashboardData'] 
  });

  const onSubmit = async (data: ProductForm) => {
    try {
      await createProduct({
        variables: {
          input: {
            name: data.name,
            category: data.category,
            basePrice: Number(data.basePrice),
            amenities: [],
            itineraries: []
          }
        }
      });
      navigate('/');
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <div style={{ padding: '2rem', fontFamily: 'system-ui', maxWidth: '600px', margin: '0 auto' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h1>Crear Nuevo Paquete</h1>
        <Link to="/">Volver</Link>
      </div>
      <hr style={{ marginBottom: '2rem' }} />

      {error && <p style={{ color: 'red' }}>Error: {error.message}</p>}

      <form onSubmit={handleSubmit(onSubmit)} style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
        <div>
          <label style={{ display: 'block', marginBottom: '0.5rem' }}>Nombre del Paquete</label>
          <input 
            {...register('name', { required: true })} 
            style={{ width: '100%', padding: '0.5rem' }} 
          />
        </div>
        <div>
          <label style={{ display: 'block', marginBottom: '0.5rem' }}>Categoría (Ej: Aventura, Relax)</label>
          <input 
            {...register('category', { required: true })} 
            style={{ width: '100%', padding: '0.5rem' }} 
          />
        </div>
        <div>
          <label style={{ display: 'block', marginBottom: '0.5rem' }}>Precio Base (USD)</label>
          <input 
            type="number" step="0.01" 
            {...register('basePrice', { required: true, min: 1 })} 
            style={{ width: '100%', padding: '0.5rem' }} 
          />
        </div>
        <button 
          type="submit" 
          disabled={loading}
          style={{ padding: '0.75rem', background: loading ? '#ccc' : '#16a34a', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer', marginTop: '1rem' }}>
          {loading ? 'Guardando...' : 'Guardar Paquete'}
        </button>
      </form>
    </div>
  );
}

export default Admin;