# cashflow

Fluxo de caixa de alta disponibiliade

# Aspectos gerais

Na imagem CashFlow.png é definido o esquema arquitetural em núvem para o deployment desse produto;
Foi considerado para tanto o requisito não funcional ==> Alta disponilidade: a principal definição que conduz essa arquitetura para ter alta disponilidade está ligado à distribuição em pelo menos duas regiões de complemento de dispobilidade e também ter a repicação de dados de modo a manter bases de dados nas duas regiões respondendo às diversas instancias de Kubernets (pods) garantindo assim a independencia entre os clusters nas regiões;

## Considerações do código da solução

No desenvolvimento da aplicação, tendo em vista isso ser parte de uma aplicação complexa (Financeiro/Contabilidade) e poder no futuro evolir para um subsistema, a decisão foi adodar um estilo arquitetural em camadas baseado em Clean Architecture, conjugado com o padrão DDD (Domain Driven Design).

# Tomadas de decisão

## Base de dados e configurações

A sistema de gerenciamento de dados utilizado nesse desenvolvimento foi o PostgreSql e integrado com o processo de Migrations do EntityFramewor core, logo para colocar a aplicação em execução localmente é necessário ter uma servidor de dados do PostgreSql disponivel para ser configurado no Secrets.Json da aplicação (é necessário configurar os secrets na maquina local - evitando o compartilhamento de dados sensívieis da base de dados). Segue aqui um exemplo do conteúdo do arquivo Secrets.Json:
{
"ConnectionStr": "User ID=<username>;Password=<passwd>;Host=<url>;Database=<database-name>;Integrated Security=true;Pooling=true"
}

# Instruções de operação

## Login

Ao executar a aplicação, o sistema irá criar automaticamente a base de dados e abrir uma página na documentação swagger da aplicação, onde podem ser executados todos os endpoints da mesma. Lembrando que a aplicação foi desenvovida utiilzando processo de autenticação. Sendo assim é necessário ir ao endpoint de login POST /api/home/login e preencher os dados de um dos dois usuários que foram "Mocados":
. username: cashusr1 password: 123@
. username: cashusr2 password: 456@

## Operações de fluxo de caixa

Após isso é possivel copiar o token gerado na resposta da requisição e fazer a autenticação do Swagger (clicar no botão Autorize) e seguir as instruções lá expostas.
Consequentemente será possível cosumir os outros endpoints:
. GET /api/billings/{id}/bill-to-pay --> consultar uma certa conta a pagar pelo código;
. GET /api/billings/{id}/bill-to-receive --> consultar uma certa conta a receber pelo código;
. POST /api/billings/bill-to-pay --> inserir uma nova conta a pagar;
. POST /api/billings/bill-to-receive --> inserir uma nova conta a receber;
. GET /api/cashflows/consolidate --> consolidar as contas de uma certa data (nesse caso apenas um dia);

# Orientações para Evolução do Projeto

Em caso de alterações nas entidades de domínio, é necessário rodar o comando de migrações para que possa atualizar o modelo de dados no sistema de gerenciamento de dados. Segue abaixo o comando com suas especificações devido a complexidade da arquitetura do produto:
. Para incluir uma nova migração, digigar na linha de comando do terminal, na pasta raiz do projeto /src o seguinte comando: > dotnet ef migrations add <migration-name> --project CashFlow.Data --startup-project CashFlow.WebAPI  
 . Para excluir a última migração: > dotnet ef migrations remove <migration-name> --project CashFlow.Data --startup-project CashFlow.WebAPI
