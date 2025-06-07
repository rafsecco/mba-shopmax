# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto MVC implementado com rotas funcionais para login e navegação de produtos.
    - Estrutura de views organizada.

### Design
  - Interface básica e funcional, atende ao mínimo necessário para uma interface administrativa.

### Funcionalidade
  * Pontos positivos:
    - CRUD funcional implementado na API.
    - Identity implementado corretamente nas duas camadas (API com JWT, MVC com cookies).
    - Uso de SQLite com migrations automáticas implementadas.
    - Modelagem das entidades e estrutura geral do domínio estão adequadas.

  * Pontos negativos:
    - A criação do vendedor só ocorre na API, não no MVC, contrariando o escopo que exige associação imediata do usuário registrado.
    - Ao criar um produto, o ID do vendedor não é recuperado do usuário logado. Isso compromete a segurança e abre brecha para inconsistências.
    - Um vendedor pode modificar produtos de outro, sem validação de domínio.
    - Seed de dados está presente apenas na camada MVC; a API não inicializa dados.
    - Camadas `Business` e `Data` adicionam complexidade desnecessária para um projeto deste porte — poderiam estar unificadas em uma única camada `Core`.

## Back End

### Arquitetura
  * Pontos positivos:
    - Separação entre camadas implementada com clareza.
    - Uso de DI e configuração modular está bem estruturado.

  * Pontos negativos:
    - Camadas `Business` e `Data` poderiam ser unificadas.
    - Implementação em inglês desconsidera a diretriz do projeto de usar nomes em português para as entidades e modelos.

### Funcionalidade
  * Pontos positivos:
    - Funcionalidades básicas operam na API conforme o esperado.

  * Pontos negativos:
    - Segurança na manipulação dos dados por vendedor está ausente.
    - Falta de verificação do usuário logado nas operações de domínio sensíveis.

### Modelagem
  * Pontos positivos:
    - Estrutura das entidades bem definida.
    - Modelos e ViewModels organizados e aplicáveis ao domínio.

## Projeto

### Organização
  * Pontos positivos:
    - Projeto organizado com uso da pasta `src`, solution `.sln` na raiz.
    - Documentação presente e Swagger configurado na API.

  * Pontos negativos:
    - Nenhum.

### Documentação
  * Pontos positivos:
    - `README.md` e `FEEDBACK.md` estão presentes.
    - Swagger ativo na API.

### Instalação
  * Pontos positivos:
    - SQLite configurado corretamente.

  * Pontos negativos:
    - Seed de dados não está implementado na API.

---

# 📊 Matriz de Avaliação de Projetos

| **Critério**                   | **Peso** | **Nota** | **Resultado Ponderado**                  |
|-------------------------------|----------|----------|------------------------------------------|
| **Funcionalidade**            | 30%      | 7        | 2,1                                      |
| **Qualidade do Código**       | 20%      | 7        | 1,4                                      |
| **Eficiência e Desempenho**   | 20%      | 7        | 1,4                                      |
| **Inovação e Diferenciais**   | 10%      | 8        | 0,8                                      |
| **Documentação e Organização**| 10%      | 8        | 0,8                                      |
| **Resolução de Feedbacks**    | 10%      | 5        | 0,5                                      |
| **Total**                     | 100%     | -        | **7,0**                                  |

## 🎯 **Nota Final: 7 / 10**
