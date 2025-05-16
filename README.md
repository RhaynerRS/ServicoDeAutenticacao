# 🛡️ Microserviço de Autenticação

Este é um microserviço de autenticação baseado em **JWT (JSON Web Token)** com emissão própria, **controle de sessão via Redis** e **persistência de dados em MongoDB**. Ele permite autenticação, logout, validação e gerenciamento de usuários com base em **roles** (perfis de acesso).

---

## ⚙️ Tecnologias Utilizadas

- [.NET / ASP.NET Core](https://dotnet.microsoft.com/)
- [MongoDB](https://www.mongodb.com/) — Armazenamento de usuários
- [Redis](https://redis.io/) — Armazenamento de sessões
- [JWT](https://jwt.io/) — Emissão e validação de tokens de autenticação
- [Swagger](https://swagger.io/tools/swagger-ui/) — Documentação interativa da API

---

## 📦 Endpoints Disponíveis

### 🔐 Autenticação

- `POST /api/usuario`  
  Criação de um novo usuário.

- `POST /api/usuario/login`  
  Autenticação de usuário. Retorna um JWT se as credenciais forem válidas.

- `POST /api/usuario/logout`  
  Finaliza a sessão do usuário e invalida o token no Redis.

### ⚙️ Gerenciamento

- `DELETE /api/usuario/{Guid}`  
  Remove um usuário pelo Guid.

- `PATCH /api/usuario/{Guid}?role={Role}`  
  Atualiza a Role de um usuário

- `GET /api/usuario/validar`  
  Valida o token JWT atual e retorna os dados da sessão associada.

---

## 🔐 Segurança

- Tokens JWT assinados localmente com chave secreta.
- Sessões de usuários armazenadas em Redis com tempo de expiração configurável.
- Autorização baseada em **roles** definidas no payload do JWT.

---

## 🧪 Executando o Projeto
### Pré-requisitos
- .NET 8 ou superior
- MongoDB em execução
- Redis em execução

### Configure as conexões de banco no appsettings.Development.json 
```json
{
    "ConnectionStrings": {
        "Redis": "{connection-string}",
        "MongoDb": "{connection-string}"
    },
    "MongoDb.Database": "{cluster-name}",
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    }
}
```

### Rodando localmente
```sh
    dotnet build
    dotnet run
```

Acesse a documentação Swagger em: http://localhost:6596/swagger

## ✅ Futuras Melhorias
- Suporte a refresh token
- Integração com OAuth2/Social login
- Auditoria e logs de login/logout
- Notificações de login por e-mail
