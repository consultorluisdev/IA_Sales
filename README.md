🚀 IA Sales - Plataforma Inteligente de Vendas

Sistema completo de automação de vendas com inteligência artificial, focado em aumentar conversão, otimizar campanhas e facilitar a gestão de clientes.

📌 Sobre o Projeto

O IA Sales é uma plataforma moderna que integra:

🤖 Inteligência Artificial para geração de campanhas
📊 Gestão de clientes (CRM)
🛒 Controle de pedidos
📢 Automação de marketing
🌐 Catálogo online (landing page)

Ideal para negócios como:

Moda 👗
Imobiliárias 🏠
Construção 🏗️
Comércio local 🛍️
🧠 Funcionalidades
✅ Cadastro e autenticação de usuários (JWT)
✅ Gestão de clientes
✅ Criação e gerenciamento de pedidos
✅ Campanhas automatizadas com IA
✅ Integração com APIs externas (AI Agent)
✅ Dashboard de vendas
✅ Catálogo de produtos (em desenvolvimento)
🏗️ Arquitetura do Sistema
Frontend (Next.js)
        ↓
Backend (.NET API)
        ↓
Banco de Dados (PostgreSQL / SQLite)
        ↓
Serviços externos (IA / APIs)
🛠️ Tecnologias Utilizadas
Backend
.NET 8
Entity Framework Core
JWT Authentication
Swagger
Frontend
Next.js
React
TailwindCSS / ShadCN UI
Mobile (planejado)
React Native / Flutter
Banco de Dados
PostgreSQL
SQLite (dev)
DevOps
Docker (em implementação)
⚙️ Como Rodar o Projeto
🔹 Backend (.NET)
cd backend
dotnet restore
dotnet run

Acesse:
👉 https://localhost:5001/swagger

🔹 Frontend (Next.js)
cd frontend
npm install
npm run dev

Acesse:
👉 http://localhost:3000

🐳 Docker (Em breve)
docker-compose up --build
🔐 Autenticação

A API utiliza autenticação via JWT.

Exemplo de Login:
POST /api/auth/login

{
  "email": "admin@email.com",
  "password": "123456"
}
📂 Estrutura do Projeto
/backend
 ├── Controllers
 ├── Services
 ├── Data
 ├── Models

/frontend
 ├── pages
 ├── components
 ├── services
📈 Roadmap

API base funcional

CRUD de clientes

Sistema de pedidos

Integração com IA

Dashboard avançado

Catálogo público

Deploy em produção

App Mobile

💡 Diferenciais
🔥 Foco em vendas reais (não só sistema bonito)
⚡ Integração com IA para automação
📊 Pensado para pequenos e médios negócios
🚀 Arquitetura escalável
🤝 Contribuição

Sinta-se à vontade para contribuir!

git clone https://github.com/seu-usuario/ia-sales
📞 Contato

Luis Fernando Lopes
💼 Desenvolvedor Full Stack
📧 luisfernandolopes0@gmail.com

⭐ Se esse projeto te ajudou...

Deixe uma ⭐ no repositório!

"Tecnologia sem execução não gera resultado. Aqui a IA vende por você."
