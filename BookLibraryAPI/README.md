📚 Book Library API
API RESTful em .NET 8 para gerenciamento de gêneros, autores e livros, utilizando MySQL como banco de dados.

🧰 Tecnologias
ASP.NET Core 8

Entity Framework Core

MySQL

Swagger (documentação automática)

RESTful API

⚙️ Configuração do Ambiente
1. Clone o repositório
bash
Copy
Edit
git clone https://github.com/seu-usuario/book-library-api.git
cd book-library-api
2. Configure o banco de dados MySQL
Crie o banco de dados:

sql
Copy
Edit
CREATE DATABASE biblioteca CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
3. Configure o arquivo appsettings.Development.json
Crie um arquivo appsettings.Development.json na raiz do projeto com a seguinte configuração:

json
Copy
Edit
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=biblioteca;user=root;password=123456"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
🔐 Importante: esse arquivo está ignorado no .gitignore por conter dados sensíveis.

4. Restore de pacotes e aplicação das migrations
bash
Copy
Edit
dotnet restore
dotnet ef database update
🚀 Executando o Projeto
bash
Copy
Edit
dotnet run
Acesse a documentação Swagger:

bash
Copy
Edit
https://localhost:5001/swagger
🔧 Endpoints disponíveis (via Swagger)
/api/v1/genero

/api/v1/autor

/api/v1/livro

Todos com suporte a:
GET, GET /{id}, POST, PUT /{id}, DELETE /{id}.

📁 Estrutura do Projeto
pgsql
Copy
Edit
📦 BookLibraryApi
 ┣ 📜 Program.cs
 ┣ 📜 appsettings.json
 ┣ 📜 appsettings.Development.json (ignorado)
 ┣ 📂 Controllers
 ┣ 📂 Models
 ┣ 📂 DTOs
 ┣ 📂 Migrations
🧪 Testes
Ainda não implementados. Pode-se usar xUnit ou NUnit com injeção de dependência mockada.

📌 Observações
Projeto segue boas práticas como separação por DTOs, versionamento de rota e uso de EF Core com migrations.

A API está preparada para expansão futura (como autenticação, paginação, etc).

