# CrudApi - Gerenciamento de Estudantes

Este projeto consiste em uma API CRUD para gerenciamento de estudantes, utilizando .NET e Entity Framework Core.

## Tecnologias Utilizadas

- .NET 9
- Entity Framework Core
- Banco de Dados Relacional (SQLite)

## Funcionalidades

- Criar um novo estudante
- Listar todos os estudantes ativos
- Atualizar o nome de um estudante
- Remover um estudante (soft delete)

## Endpoints

### Criar um Estudante

```http
POST /students
```
**Body:**
```json
{
  "name": "Nome do Estudante"
}
```
**Respostas:**
- `200 OK`: Estudante criado com sucesso.
- `409 Conflict`: O nome já está em uso.

### Listar Estudantes Ativos

```http
GET /students
```
**Respostas:**
- `200 OK`: Retorna a lista de estudantes ativos.

### Atualizar Nome do Estudante

```http
PUT /students/{id}
```
**Body:**
```json
{
  "name": "Novo Nome"
}
```
**Respostas:**
- `200 OK`: Estudante atualizado com sucesso.
- `404 Not Found`: Estudante não encontrado.

### Remover Estudante (Soft Delete)

```http
DELETE /students/{id}
```
**Respostas:**
- `200 OK`: Estudante desativado com sucesso.
- `404 Not Found`: Estudante não encontrado.

## Como Executar o Projeto

1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/CrudApi.git
   ```
2. Navegue até o diretório do projeto:
   ```sh
   cd CrudApi
   ```
3. Instale as dependências:
   ```sh
   dotnet restore
   ```
4. Configure a string de conexão no `appsettings.json`.
5. Execute as migrações do banco de dados:
   ```sh
   dotnet ef database update
   ```
6. Inicie a API:
   ```sh
   dotnet run
   ```
