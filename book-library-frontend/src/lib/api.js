import axios from 'axios';

// Configuração base da API
const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'https://localhost:7071/api/v1',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para tratamento de erros
api.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('Erro na API:', error);
    
    // Tratamento específico para diferentes tipos de erro
    if (error.response) {
      // Erro de resposta do servidor
      const { status, data } = error.response;
      console.error(`Erro ${status}:`, data);
      
      // Você pode adicionar tratamentos específicos aqui
      switch (status) {
        case 400:
          console.error('Dados inválidos enviados para a API');
          break;
        case 404:
          console.error('Recurso não encontrado');
          break;
        case 500:
          console.error('Erro interno do servidor');
          break;
        default:
          console.error('Erro desconhecido');
      }
    } else if (error.request) {
      // Erro de rede
      console.error('Erro de conexão com a API. Verifique se a API está rodando.');
    } else {
      // Erro de configuração
      console.error('Erro na configuração da requisição:', error.message);
    }
    
    return Promise.reject(error);
  }
);

export default api;

