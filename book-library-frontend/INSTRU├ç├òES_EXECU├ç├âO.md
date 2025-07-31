# 📋 Instruções para Execução do Frontend

## 🎯 Objetivo
Este documento contém as instruções passo a passo para executar o frontend do Sistema de Biblioteca na sua máquina local e conectá-lo com sua API.

## 📋 Pré-requisitos

### ✅ Verificar se você tem:
1. **Node.js** (versão 16 ou superior)
   - Verificar: `node --version`
   - Download: https://nodejs.org/

2. **npm ou pnpm** (gerenciador de pacotes)
   - npm vem com Node.js
   - pnpm (mais rápido): `npm install -g pnpm`

3. **API BookLibraryAPI** rodando em `https://localhost:7071`
   - Confirme que está acessível em: https://localhost:7071/swagger

## 🚀 Passo a Passo

### 1. Baixar o Projeto
Copie a pasta `book-library-frontend` para sua máquina local.

### 2. Abrir Terminal
Navegue até a pasta do projeto:
```bash
cd caminho/para/book-library-frontend
```

### 3. Instalar Dependências
Execute um dos comandos:
```bash
# Opção 1: usando npm
npm install

# Opção 2: usando pnpm (mais rápido)
pnpm install
```

### 4. Configurar Variáveis de Ambiente
O arquivo `.env.local` já está configurado com:
```env
VITE_API_BASE_URL=https://localhost:7071/api/v1
```

Se sua API estiver em outra porta, edite este arquivo.

### 5. Executar o Frontend
Execute um dos comandos:
```bash
# Opção 1: usando npm
npm run dev

# Opção 2: usando pnpm
pnpm run dev
```

### 6. Acessar a Aplicação
Abra seu navegador em: **http://localhost:5173**

## 🔧 Configuração CORS na API

Para que o frontend funcione corretamente, sua API precisa estar configurada para aceitar requisições CORS.

### No seu projeto BookLibraryAPI, adicione no `Program.cs`:

```csharp
// Adicionar serviços CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Mais abaixo no pipeline, ANTES de app.UseAuthorization():
app.UseCors("AllowFrontend");
```

## 🧪 Testando a Integração

### 1. Verificar Conexão
- Abra o frontend em http://localhost:5173
- Abra as **Ferramentas do Desenvolvedor** (F12)
- Vá para a aba **Console**
- Navegue para "Autores", "Gêneros" ou "Livros"
- Se houver erros de conexão, eles aparecerão no console

### 2. Testar CRUD Completo

#### Autores:
1. Clique em "Autores"
2. Clique em "Novo Autor"
3. Digite um nome e clique "Criar"
4. Teste editar e excluir

#### Gêneros:
1. Clique em "Gêneros"
2. Clique em "Novo Gênero"
3. Digite um nome e clique "Criar"
4. Teste editar e excluir

#### Livros:
1. **Primeiro**, crie pelo menos um autor e um gênero
2. Clique em "Livros"
3. Clique em "Novo Livro"
4. Preencha título, selecione autor e gênero
5. Clique "Criar"
6. Teste editar e excluir

## 🐛 Solução de Problemas

### ❌ Erro: "Failed to fetch" ou "Network Error"
**Causa**: API não está rodando ou problema de CORS
**Solução**:
1. Verifique se a API está rodando em https://localhost:7071
2. Acesse https://localhost:7071/swagger no navegador
3. Configure CORS na API (veja seção acima)

### ❌ Erro: "Cannot resolve dependency"
**Causa**: Dependências não instaladas
**Solução**:
```bash
rm -rf node_modules package-lock.json
npm install
# ou
rm -rf node_modules pnpm-lock.yaml
pnpm install
```

### ❌ Erro de certificado HTTPS
**Causa**: Certificado local não confiável
**Solução**:
1. No navegador, acesse https://localhost:7071
2. Clique em "Avançado" → "Continuar para localhost"
3. Recarregue o frontend

### ❌ Porta 5173 já em uso
**Solução**: O Vite automaticamente tentará a próxima porta disponível (5174, 5175, etc.)

## 📱 Funcionalidades Disponíveis

### ✅ Interface Completa
- **Home**: Dashboard com navegação
- **Autores**: CRUD completo
- **Gêneros**: CRUD completo  
- **Livros**: CRUD com relacionamentos
- **Design responsivo**: Funciona em desktop e mobile

### ✅ Funcionalidades Técnicas
- Validação de formulários
- Confirmação para exclusões
- Estados de loading
- Tratamento de erros
- Navegação intuitiva

## 📞 Suporte

Se encontrar problemas:

1. **Verifique o console** do navegador (F12 → Console)
2. **Verifique se a API** está respondendo em https://localhost:7071/swagger
3. **Confirme as configurações** CORS na API
4. **Teste os endpoints** da API diretamente no Swagger

## 🎉 Pronto!

Se tudo estiver funcionando, você terá:
- ✅ Frontend rodando em http://localhost:5173
- ✅ API rodando em https://localhost:7071
- ✅ CRUD completo funcionando
- ✅ Interface moderna e responsiva

**Parabéns! Seu sistema de biblioteca está completo e funcionando!** 🚀

