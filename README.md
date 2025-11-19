# FIAP Cloud Games (FCG) - Tech Challenge Fase 1

Este repositório contém o MVP (Minimum Viable Product) da plataforma **FIAP Cloud Games**, desenvolvido como parte da avaliação da **Fase 1** da Pós-Graduação Arquitetura de Sistemas .NET.

O objetivo desta fase foi criar uma API RESTful robusta em **.NET 8** para gestão de usuários e jogos, aplicando práticas avançadas de desenvolvimento de software.

## 🚀 Funcionalidades

O sistema atende aos seguintes requisitos funcionais e técnicos:

**Cadastro de Usuários:** Criação de contas com validação rigorosa de e-mail e senha (mínimo 8 caracteres, letras, números e especiais).
**Autenticação e Autorização:** Login seguro gerando Token JWT (JSON Web Token) com controle de acesso baseado em Roles (Usuario vs Administrador).
**Cadastro de Jogos:** Endpoint protegido (apenas Administradores) para cadastrar novos jogos na plataforma.
**Segurança:** Senhas armazenadas como Hashes seguros (BCrypt).
**Documentação:** Swagger UI configurado para testes interativos da API.

## 🏗️ Arquitetura e Tecnologias

O projeto foi construído seguindo os princípios do **Domain-Driven Design (DDD)**, garantindo um código limpo, testável e desacoplado.

**Linguagem:** C# (.NET 8).
**Arquitetura:** Monolito Modular (Camadas: API, Application, Domain, Infrastructure)[.
**Banco de Dados:** SQL Server (via Entity Framework Core).
**ORM:** Entity Framework Core com Migrations.
**Testes:** xUnit (Testes Unitários para validação de regras de negócio).
**Segurança:** BCrypt.Net para hashing e System.IdentityModel para JWT.

### Estrutura da Solução

* `FCG.Domain`: O coração do projeto. Contém as Entidades (`Usuario`, `Jogo`), Interfaces de Repositório e Regras de Negócio. Não depende de ninguém.
* `FCG.Application`: Orquestra os fluxos (Casos de Uso). Contém os Services (`UsuarioService`, `JogoService`, `TokenService`) e DTOs.
* `FCG.Infrastructure`: Implementa o acesso a dados. Contém o `AppDbContext`, as configurações do EF Core e a implementação dos Repositórios.
* `FCG.API`: A camada de entrada. Contém os Controllers e a configuração de Injeção de Dependência.
* `FCG.Tests`: Projeto de testes unitários para garantir a qualidade do domínio.

## ⚙️ Como Rodar o Projeto Localmente

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.
* SQL Server (Express, Developer ou LocalDB) rodando.
* Git instalado.

### Passo a Passo

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/GabrielThales/FiapCloudGame
    cd FiapCloudGames
    ```

2.  **Configure o Banco de Dados:**
    Abra o arquivo `FCG.API/appsettings.json`. Verifique se a `ConnectionStrings:DefaultConnection` aponta para o seu servidor SQL local.
    *Exemplo:* `Server=localhost\\SQLEXPRESS;Database=FCG_DB;Trusted_Connection=True;TrustServerCertificate=True;`

3.  **Aplique as Migrations:**
    Abra o terminal na raiz do projeto e execute:
    ```bash
    dotnet ef database update --project FCG.Infrastructure --startup-project FCG.API
    ```
    *Isso criará o banco de dados `FCG_DB` e as tabelas automaticamente.*

4.  **Execute a API:**
    ```bash
    dotnet run --project FCG.API
    ```

5.  **Acesse o Swagger:**
    A aplicação iniciará (geralmente na porta 5xxx ou 7xxx). Verifique o terminal para ver a URL HTTPS.
    Acesse: `https://localhost:[SUA_PORTA]/swagger`

## 🧪 Como Rodar os Testes

O projeto conta com testes unitários que validam as regras de negócio (ex: impedir cadastro de jogo com preço negativo ou usuário com e-mail inválido).

Para executar os testes, rode o comando na raiz:
```bash
dotnet test
