#language: pt-Br
Funcionalidade: HerokupApp

@herokupapp
Cenario: Configurações
	Dado que cliquei em configurações
	Quando definir a empresa
	E clicar em Salvar
	Entao valido que foi salvo com sucesso

@herokupapp
Cenario: Criar uma tarefa
	Dado que clico em criar uma tarefa
	Quando preencher os campos
	Entao valido que a tarefa foi criada com sucesso