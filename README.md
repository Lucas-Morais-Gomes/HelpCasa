# HelpCasa

## Descrição
HelpCasa é uma API desenvolvida para conectar empregadores e empregados no setor de serviços domésticos. A API permite o registro e login de usuários, com diferentes tipos de usuários (empregador e empregado), oferecendo funcionalidades para gerenciar serviços e facilitar a comunicação entre as partes.

## Tecnologias Utilizadas
- **C#**: Linguagem de programação utilizada para desenvolver a API.
- **ASP.NET Core**: Framework utilizado para construir a API.
- **Entity Framework Core**: ORM utilizado para interagir com o banco de dados.
- **PostgreSQL**: Banco de dados utilizado para armazenar as informações dos usuários e serviços.
- **BCrypt**: Biblioteca utilizada para a criptografia de senhas.

## Funcionalidades
- **Registro de Usuários**: Permite que novos usuários se cadastrem como empregadores ou empregados, fornecendo as informações necessárias.
- **Login de Usuários**: Permite que usuários existentes realizem login utilizando e-mail e senha.
- **Gestão de Serviços**: Permite que empregados ofereçam serviços e que empregadores contratem esses serviços.

## Estrutura do Projeto
O projeto está estruturado da seguinte forma:

HelpCasa_Backend/ │ ├── Controllers/ # Controladores da API │ └── AuthController.cs # Controlador para autenticação (login e registro) │ ├── Data/ # Contexto do banco de dados │ └── ApplicationDbContext.cs # Configuração do contexto do banco de dados │ ├── Models/ # Modelos do projeto │ ├── Usuario.cs # Classe base para usuários │ ├── Empregado.cs # Classe para empregados │ ├── Empregador.cs # Classe para empregadores │ └── Servico.cs # Classe para serviços │ ├── Program.cs # Ponto de entrada da aplicação └── Startup.cs # Configurações iniciais da API


## Como Executar o Projeto
1. Clone o repositório:
   ```bash
   git clone https://github.com/Lucas-Morais-Gomes/HelpCasa
   cd HelpCasa_Backend

3. Configure a string de conexão no arquivo appsettings.json para apontar para seu banco de dados PostgreSQL.

4. Instale as dependências:
dotnet restore

5. Inicie o servidor:
dotnet run

## Contribuições
Sinta-se à vontade para contribuir com melhorias ou novas funcionalidades. Faça um fork do repositório e envie um pull request.
