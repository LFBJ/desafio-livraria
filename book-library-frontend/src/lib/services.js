import api from './api';

// Serviços para Gêneros
export const generoService = {
  getAll: () => api.get('/genero'),
  getById: (id) => api.get(`/genero/${id}`),
  create: (data) => api.post('/genero', data),
  update: (id, data) => api.put(`/genero/${id}`, data),
  delete: (id) => api.delete(`/genero/${id}`),
};

// Serviços para Autores
export const autorService = {
  getAll: () => api.get('/autor'),
  getById: (id) => api.get(`/autor/${id}`),
  create: (data) => api.post('/autor', data),
  update: (id, data) => api.put(`/autor/${id}`, data),
  delete: (id) => api.delete(`/autor/${id}`),
};

// Serviços para Livros
export const livroService = {
  getAll: () => api.get('/livro'),
  getById: (id) => api.get(`/livro/${id}`),
  create: (data) => api.post('/livro', data),
  update: (id, data) => api.put(`/livro/${id}`, data),
  delete: (id) => api.delete(`/livro/${id}`),
};

