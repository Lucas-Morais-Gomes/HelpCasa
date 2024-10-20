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
```
HelpCasa_Backend/
│
├── Controllers/              # Controladores da API
│   └── AuthController.cs     # Controlador para autenticação (login e registro)
│
├── Data/                     # Contexto do banco de dados
│   └── ApplicationDbContext.cs # Configuração do contexto do banco de dados
│
├── Models/                   # Modelos do projeto
│   ├── Usuario.cs            # Classe base para usuários
│   ├── Empregado.cs          # Classe para empregados
│   ├── Empregador.cs         # Classe para empregadores
│   └── Servico.cs            # Classe para serviços
│
├── Program.cs                # Ponto de entrada da aplicação
└── Startup.cs                # Configurações iniciais da API
```

## Como Executar o Projeto
1. Clone o repositório:
   ```
   git clone https://github.com/Lucas-Morais-Gomes/HelpCasa
   cd HelpCasa_Backend
   ```

2. Configure a string de conexão no arquivo `appsettings.json` para apontar para seu banco de dados PostgreSQL.

3. Instale as dependências:
   ```bash
   dotnet restore
   ```

4. Inicie o servidor:
   ```bash
   dotnet run
   ```

## Contribuições
Sinta-se à vontade para contribuir com melhorias ou novas funcionalidades. Faça um fork do repositório e envie um pull request.

---

# HelpCasa Frontend

## Descrição
HelpCasa Frontend é a interface do usuário desenvolvida em React para interagir com a API HelpCasa. O frontend permite que os usuários se registrem, façam login e gerenciem suas contas.

## Tecnologias Utilizadas
- **React**: Biblioteca para construir interfaces de usuário.
- **React Router**: Biblioteca para gerenciar navegação em aplicações React.
- **Axios**: Biblioteca para fazer requisições HTTP.

## Funcionalidades
- **Tela de Login**: Permite que usuários existentes façam login.
- **Tela de Cadastro**: Permite que novos usuários se cadastrem como empregador ou empregado.
- **Navegação**: Permite que os usuários naveguem entre as telas de login, cadastro e a página principal.

## Estrutura do Projeto
O projeto está estruturado da seguinte forma:

```
HelpCasa_Frontend/
│
├── src/
│   ├── components/           # Componentes da aplicação
│   │   ├── Login.js          # Componente de Login
│   │   ├── Register.js       # Componente de Cadastro
│   │   └── Dashboard.js       # Componente da Tela Principal
│   │
│   ├── App.js                # Componente Principal da Aplicação
│   ├── index.js              # Ponto de entrada da aplicação
│   └── styles/               # Folhas de estilo
│       ├── App.css
│       └── Login.css
│
└── package.json              # Dependências do projeto
```

## Como Executar o Frontend
1. Navegue até a pasta do frontend:
   ```bash
   cd HelpCasa_Frontend
   ```

2. Instale as dependências:
   ```bash
   npm install
   ```

3. Inicie o servidor de desenvolvimento:
   ```bash
   npm start
   ```

## Contribuições
Sinta-se à vontade para contribuir com melhorias ou novas funcionalidades. Faça um fork do repositório e envie um pull request.
