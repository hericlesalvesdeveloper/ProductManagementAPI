# ✅ Product Management API

API REST desenvolvida com **.NET 8**, **ASP.NET Core** e **Entity Framework Core**, com foco em boas práticas de arquitetura, separação de responsabilidades e tratamento global de erros.

---

# 🚀 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger (OpenAPI)
- C#
---

# 📂 Estrutura do Projeto

O projeto foi organizado separando responsabilidades para facilitar manutenção e escalabilidade.

- **Controllers** → Responsáveis pelos endpoints da API  
- **Services** → Camada de regras de negócio  
- **Repositories** → Acesso e manipulação de dados  
- **Models / Entities** → Representação das entidades do domínio  
- **DTOs** → Objetos de transferência de dados entre camadas  
- **Data** → Configuração do `DbContext`  
- **Exceptions** → Classes de exceções personalizadas da aplicação  
- **Middleware** → Tratamento global de erros da aplicação
- **Interfaces** → Interfaces do projeto

Essa estrutura segue o princípio de **separação de responsabilidades**, reduzindo acoplamento entre as camadas da aplicação.

---

# ⚙️ Configuração do Banco de Dados

⚠️ **A connection string não está versionada no repositório.**

Para proteger informações sensíveis, utilize o `dotnet user-secrets`.

### 1️⃣ Inicializar o user-secrets

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;port=3306;database=ProductManagementDb;user=SEU_USUARIO;password=SUA_SENHA"
dotnet ef database update
```
---
# 🧠 Boas Práticas Aplicadas

- Uso de métodos assíncronos (async/await) para evitar bloqueio de threads e melhorar a escalabilidade da aplicação.
- Separação entre Controller, Service e Repository.
- Implementação de DTOs para evitar exposição direta das entidades.
- Implementação de tratamento global de exceções utilizando Middleware.
- Criação de exceções customizadas para regras de negócio.
- Centralização da conversão de exceptions para status HTTP no middleware.
- Uso de migrations para versionamento do banco de dados.

---

# 🛡️ Tratamento Global de Exceções
A aplicação implementa um middleware global de tratamento de erros, responsável por capturar exceções não tratadas e retornar respostas padronizadas para a API.

Esse middleware:

-Intercepta exceções lançadas em qualquer camada da aplicação
-Realiza logging do erro
-Converte exceções em status HTTP apropriados
-Retorna respostas JSON padronizadas

Exemplo de mapeamento de exceções:

| Exception | Status Code |
|-----------|------------|
| BusinessException | 400 |
| NotFoundException | 404 |
| Exception | 500 |

Esse padrão centraliza o tratamento de erros e evita duplicação de lógica nos controllers.

---

## 📌 Endpoints

Base URL:

---
# GET /api/Product

Retorna uma lista paginada de produtos.

### Parâmetros de Query

| Parâmetro | Tipo | Descrição |
|-----------|------|-----------|
| page | int | Número da página |
| pageSize | int | Quantidade de registros por página |

### Exemplo de requisição

```
GET /api/Product?page=1&pageSize=10
```

### Resposta (200 OK)

```json
[
  {
    "id": 1,
    "name": "Notebook",
    "price": 3500,
    "quantity": 10
  }
]
```

## 🔍 GET /api/Product/{id}

Retorna um produto específico pelo **ID**.

### Parâmetros

| Parâmetro | Tipo | Descrição |
|-----------|------|-----------|
| id | int | Identificador do produto |

### Exemplo de requisição

```
GET /api/Product/1
```

### Resposta (200 OK)

```json
{
  "id": 1,
  "name": "Notebook",
  "price": 3500,
  "quantity": 10
}
```

# ➕ POST /api/Product
Cria um novo produto.

Body da requisição

```
{
  "name": "Notebook",
  "price": 3500,
  "quantity": 10
}
```
Resposta
| Status Code | Descrição |
|-------------|-----------|
| 201 | Produto criado com sucesso |

# 🗑️ DELETE /api/Product/{id}
Remove um produto do sistema.

| Parâmetro | Tipo | Descrição |
|-----------|------|-----------|
| id | int | Identificador do produto |

Exemplo de requisição
```
DELETE /api/Product/1
```
Resposta
```
204 No Content
```



















