# AvaliacaoS2It (REFACTOR APÓS A ENTREGA DA PROPOSTA PARA O PROBLEMA)
Avaliacao conhecimentos .NET s2 It

Projeto desenvolvido utiliando Visual studio 2017 Versão 15.5.4

Desenvolvido utilizando EntityFramework (CodeFirst)

Necessário executar os comandos abaixo via NugetPackageConsole para realizar a criação da base de dados:

# Selecionar o projeto Desafio.s2.Identity
  Add-Migration asp-net-identity-initialize

Update-Database

# Selecionar o projeto Desafio.s2.Data
  Add-Migration event-store-initialize -Context EventStoreSQLContext

  Update-Database -Context EventStoreSQLContext

  Add-Migration avaliacao-s2-initialize -Context ApplicationDataContext

  Update-Database -Context ApplicationDataContext
  
  
  
  Desenvolvido por Marcos Freire - 2018.
