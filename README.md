# Projeto de API - Arquitetura Limpa (Clean Architecture), CQRS, DDD, MediatR, Repository e Unit of Work
Este projeto é uma API construída seguindo os princípios de Clean Architecture, Domain-Driven Design (DDD), CQRS (Command Query Responsibility Segregation), MediatR, e os padrões Repository e Unit of Work. O objetivo deste projeto é ilustrar uma implementação de uma API bem estruturada, modular e escalável, com foco em boas práticas de arquitetura de software.

![GitHub repo views](https://komarev.com/ghpvc/?username=andredobbss&repo=FastReportAPI&color=blue)

## Arquitetura do Projeto
### Clean Architecture
A arquitetura do projeto segue os princípios da Clean Architecture, que visa separar as responsabilidades de forma clara entre as camadas do sistema, garantindo uma boa organização do código e facilidade para manutenção e testes.

O projeto é dividido nas seguintes camadas:

- Domain: Contém a lógica de negócio e as entidades do domínio.
- Application: Contém os casos de uso da aplicação, incluindo os handlers do MediatR e os serviços que interagem com o domínio.
- Infrastructure: Contém a implementação das dependências externas, como persistência de dados (banco de dados), serviços de envio de e-mail, etc.
- API: Contém os controladores e a configuração do servidor Web API.
  
### Domain-Driven Design (DDD)
No coração da aplicação, a arquitetura segue os princípios de Domain-Driven Design (DDD), onde a modelagem do domínio e o comportamento do sistema estão intimamente relacionados. As entidades e agregados são modelados de acordo com as regras de negócio.

### CQRS (Command Query Responsibility Segregation)
O padrão CQRS é utilizado para separar as responsabilidades de leitura e escrita no sistema. Ele permite otimizar e escalar operações de leitura e escrita independentemente, e também melhora a segurança ao permitir que as ações sejam realizadas em fluxos separados.

- Comandos (Command): Representam ações que alteram o estado do sistema.
- Consultas (Query): São responsáveis por retornar dados, sem modificar o estado.
### MediatR
O MediatR é utilizado para simplificar a comunicação entre as camadas do sistema. Ele permite a implementação de um modelo de mediador para enviar e tratar comandos, consultas e notificações de maneira desacoplada. Esse padrão facilita a extensibilidade e a testabilidade do sistema.

### Repository e Unit of Work
O padrão Repository é utilizado para abstrair o acesso aos dados, oferecendo uma interface consistente para manipulação de entidades no banco de dados. O Unit of Work coordena as operações de leitura e gravação, garantindo que todas as mudanças feitas no banco de dados sejam tratadas de forma transacional e consistentes.

### Tecnologias Utilizadas
- .NET 8 (Core)
- ASP.NET Core Web API
- Entity Framework Core (para acesso ao banco de dados relacional)
- FluentValidation (para validação de dados)
- MediatR (para mediar comandos, consultas e notificações)
- PostgreSQL (para persistência de dados)
- MongoDB (para leitura de dados)
- Swagger (para documentação da API)
- xUnit (para testes unitários)
