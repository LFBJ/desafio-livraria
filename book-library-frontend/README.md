# Sistema de Biblioteca - Frontend

Frontend React para o sistema de gerenciamento de biblioteca, permitindo CRUD completo de Autores, Gêneros e Livros.

## 🚀 Tecnologias Utilizadas

- **React 18** - Biblioteca para construção da interface
- **React Router DOM** - Roteamento da aplicação
- **Tailwind CSS** - Framework CSS para estilização
- **shadcn/ui** - Componentes de interface modernos
- **Lucide React** - Ícones
- **Axios** - Cliente HTTP para comunicação com a API
- **Vite** - Build tool e servidor de desenvolvimento

## 📋 Pré-requisitos

- Node.js (versão 16 ou superior)
- npm ou pnpm
- API BookLibraryAPI rodando em `https://localhost:7071`

## 🔧 Instalação e Configuração

### 1. Clone ou baixe o projeto

```bash
# Se estiver usando git
git clone [url-do-repositorio]
cd book-library-frontend

# Ou extraia o arquivo ZIP do projeto
```

### 2. Instale as dependências

```bash
# Usando npm
npm install

# Ou usando pnpm (recomendado)
pnpm install
```

### 3. Configure as variáveis de ambiente

Copie o arquivo `.env.example` para `.env.local`:

```bash
cp .env.example .env.local
```

Edite o arquivo `.env.local` se necessário:

```env
VITE_API_BASE_URL=https://localhost:7071/api/v1
```

### 4. Execute o projeto

```bash
# Usando npm
npm run dev

# Ou usando pnpm
pnpm run dev
```

O projeto estará disponível em `http://localhost:5173`

## 📱 Funcionalidades

### ✅ Autores
- Listar todos os autores
- Criar novo autor
- Editar autor existente
- Excluir autor
- Validação de formulários

### ✅ Gêneros
- Listar todos os gêneros
- Criar novo gênero
- Editar gênero existente
- Excluir gênero
- Validação de formulários

### ✅ Livros
- Listar todos os livros com autor e gênero
- Criar novo livro (com seleção de autor e gênero)
- Editar livro existente
- Excluir livro
- Relacionamentos com autores e gêneros
- Validação de formulários

### 🎨 Interface
- Design moderno e responsivo
- Navegação intuitiva
- Modais para criação/edição
- Confirmações para exclusão
- Estados de carregamento
- Tratamento de erros

## 🏗️ Estrutura do Projeto

```
src/
├── components/          # Componentes reutilizáveis
│   ├── ui/             # Componentes shadcn/ui
│   └── Layout.jsx      # Layout principal
├── lib/                # Utilitários e configurações
│   ├── api.js          # Configuração do Axios
│   └── services.js     # Serviços da API
├── pages/              # Páginas da aplicação
│   ├── Home.jsx        # Página inicial
│   ├── Autores.jsx     # Gerenciamento de autores
│   ├── Generos.jsx     # Gerenciamento de gêneros
│   └── Livros.jsx      # Gerenciamento de livros
├── App.jsx             # Componente principal
└── main.jsx            # Ponto de entrada
```

## 🔗 Integração com a API

O frontend está configurado para consumir a API BookLibraryAPI nos seguintes endpoints:

- **Autores**: `/api/v1/autor`
- **Gêneros**: `/api/v1/genero`
- **Livros**: `/api/v1/livro`

### Métodos HTTP utilizados:
- `GET` - Listar e buscar por ID
- `POST` - Criar novos registros
- `PUT` - Atualizar registros existentes
- `DELETE` - Excluir registros

## 🛠️ Scripts Disponíveis

```bash
# Desenvolvimento
npm run dev          # Inicia servidor de desenvolvimento
pnpm run dev

# Build para produção
npm run build        # Gera build otimizado
pnpm run build

# Preview da build
npm run preview      # Visualiza a build localmente
pnpm run preview
```

## 🔧 Configuração da API

Certifique-se de que sua API BookLibraryAPI está:

1. **Rodando** em `https://localhost:7071`
2. **Configurada** para aceitar requisições CORS do frontend
3. **Com os endpoints** `/api/v1/autor`, `/api/v1/genero`, `/api/v1/livro` funcionando

### Exemplo de configuração CORS na API (.NET):

```csharp
// No Program.cs ou Startup.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// No pipeline
app.UseCors("AllowFrontend");
```

## 🐛 Solução de Problemas

### Erro de CORS
Se encontrar erros de CORS, verifique se a API está configurada para aceitar requisições do frontend.

### Erro de conexão
- Verifique se a API está rodando em `https://localhost:7071`
- Confirme se os endpoints estão respondendo
- Verifique o console do navegador para mais detalhes

### Problemas de certificado HTTPS
Se estiver usando HTTPS local, pode ser necessário aceitar o certificado no navegador.

## 📝 Notas de Desenvolvimento

- O projeto usa **Tailwind CSS** para estilização
- Componentes **shadcn/ui** para interface consistente
- **React Router** para navegação SPA
- **Axios** com interceptors para tratamento de erros
- Formulários controlados com validação
- Estados de loading e error handling

## 🚀 Deploy

Para fazer deploy em produção:

1. Configure a variável `VITE_API_BASE_URL` com a URL da API em produção
2. Execute `npm run build` ou `pnpm run build`
3. Sirva os arquivos da pasta `dist/` em um servidor web

## 📄 Licença

Este projeto foi desenvolvido como parte do desafio técnico para sistema de biblioteca.

