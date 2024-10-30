# Gerenciamento de Tarefas

Este é um sistema de gerenciamento de tarefas desenvolvido com Angular no front-end e .NET 8.0 no back-end. O sistema permite o cadastro de pessoas e tarefas, associando as tarefas às pessoas responsáveis e gerenciando o status de ambas.

## Funcionalidades
- Cadastro de pessoas.
- Cadastro e gerenciamento de tarefas.
- Associações entre pessoas e suas tarefas.
- Verificação de status: se todas as tarefas de uma pessoa estão concluídas, o status é alterado para disponível.
- Restrições para exclusão de pessoas com tarefas pendentes.

## Tecnologias Utilizadas
- Angular
- .NET 8.0
- Entity Framework Core
- SQL Server
- Bootstrap (para estilos)

## Pré-requisitos
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (com npm)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configuração do Ambiente
1. Clone o repositório:
   git clone https://github.com/ShiroUsag1/Gerenciador-de-Tarefas.git
   cd Gerenciador-de-Tarefas

2. Configure o banco de dados SQL Server:
- Crie um banco de dados chamado TaskManagementDb.
- Execute as migrações para criar as tabelas.
- dotnet ef database update

3. Configure a string de conexão no appsettings.json:
    "ConnectionStrings": {"DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True"}
    
### Execução
Inicie o back-end:
    dotnet run

Em outro terminal, inicie o front-end:
    cd client
    npm install
    ng serve

- O front-end estará disponível em `http://localhost:4200`.
- O back-end estará disponível em `http://localhost:5098`.

