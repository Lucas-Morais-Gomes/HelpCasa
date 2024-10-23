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
