<h1>HANDS ON:</h1>
<meta>Exercicio de fixação de autenticação com JWT</meta>
<p>Criar um arquivo de migracao: dotnet ef migrations add Estrutura_Inicial. Gerando o banco:dotnet ef database update. Criamos uma nova coluna para a tabela Usuario</p>
<p>Atualizar o banco de dados: dotnet ef migrations add AtualizacaoCpf_Usuario. Colocar update novamente. Para desfazer a ação: dotnet ef migrations remove. Add TokenConfigurations em appsettings: "Audience":quem pode acessar sua API, "Issuer":"ExemploIssuer", "Seconds":Tempo de acesso ao token.</p>
<p>Criar classe TokenConfigurations com as propriedades do appsettins dentro da classe Models. Criar classe SigningConfigurations na pasta Repositorio. Configurar startup.</p>