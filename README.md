# **ShopMax - Sistema de gestão simplificada de produtos e categorias em um formato tipo e-commerce marketplace**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **ShopMax**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver um sistema de gestão simplificada de produtos e categorias permite aos usuários criar, editar, visualizar e excluir categorias e produtos, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.

### **Autor(es)**
- **Rafael Secco**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o marketplace.
- **API RESTful:** Exposição dos recursos do marketplace para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server e SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

mba-shopmax/
├─ src/
│  ├─ ShopMax.API/ - API RESTful
│  ├─ ShopMax.Business/ - Modelos, interfaces, regras de negócios
│  ├─ ShopMax.Data/ - Modelos de Dados e Configuração do EF Core
│  └─ ShopMax.MVC/ - Projeto MVC
├─ .gitignore - Arquivo de Ignoração do Git
├─ FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
├─ README.md - Arquivo de Documentação do Projeto
└─ ShopMax.sln - Arquivo da Solução do Projeto

## **5. Funcionalidades Implementadas**

- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir categorias e produtos.
- **Autenticação e Autorização:** Todos os usuários são considerados vendedores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server e SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/rafsecco/mba-shopmax.git`
   - `cd mba-shopmax`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - `cd src/ShopMax.MVC/`
   - `dotnet run`
   - Acesse a aplicação em: https://localhost:7017

4. **Executar a API:**
   - `cd src/ShopMax.API/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7035/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

https://localhost:7035/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
