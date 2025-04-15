# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas no projeto ShopMax.MVC

### Design
    - Será avaliado na entrega final

### Funcionalidade
  * Pontos positivos:
    - Interface web com HTML/CSS
    - Implementação com Razor Pages/Views

## Back End
### Arquitetura
  * Pontos positivos:
    - Estrutura em camadas na pasta src:
      * ShopMax.API
      * ShopMax.Business
      * ShopMax.Data
      * ShopMax.MVC

  * Pontos negativos:
    - Arquitetura mais complexa que o necessário com 4 camadas
    - Separação desnecessária entre Business e Data (nesse caso uma única camada "Core" atende), mas tudo bem não está demasiadamente complexo.

### Funcionalidade
  * Pontos positivos:
    - Suporte a múltiplos bancos de dados (SQL Server e SQLite)
    - Implementação do ASP.NET Identity
    - Configuração de Seed de dados mencionada

  * Pontos negativos:
    - MVC não implementa as mesmas funcionalidades da API no registro do usuário.

### Modelagem
  * Pontos positivos:
    - Modelagem simples e funcional

  * Pontos negativos:
    - Nomenclatura em ingles e implementação em portugues é um grande erro e causa muita confusão.

## Projeto
### Organização
  * Pontos positivos:
    - Estrutura organizada com pasta src na raiz
    - Arquivo solution (ShopMax.sln) na raiz
    - .gitignore adequado
    - Separação clara dos projetos

### Documentação
  * Pontos positivos:
    - README.md presente com:
      * Estrutura do projeto bem detalhada
      * Tecnologias utilizadas
      * Instruções de execução
    - Arquivo FEEDBACK.md presente
    - Documentação da API via Swagger

### Instalação
  * Pontos positivos:
    - Suporte a múltiplos bancos (SQL Server e SQLite)
    - Configuração de Seed de dados mencionada
    - Instruções detalhadas de instalação
    - URLs específicas para acesso local