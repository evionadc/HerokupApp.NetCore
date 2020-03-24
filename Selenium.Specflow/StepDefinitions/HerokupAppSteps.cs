using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Specflow.Pages;
using System;
using TechTalk.SpecFlow;

namespace Selenium.Specflow.StepDefinitions
{
    [Binding]
    public class HerokupAppSteps
    {
        private RemoteWebDriver driver;
        private ConfiguracaoPage config;
        private TaskPage task;
        private readonly String nomeTarefa = "Tarefa automatizada";
        private readonly String tags = "testes,selenium,";

        public HerokupAppSteps(RemoteWebDriver _driver)
        {
            this.driver = _driver;
            this.config = new ConfiguracaoPage(driver);
            this.task = new TaskPage(driver);
        }


        [Given(@"que cliquei em configurações")]
        public void DadoQueCliqueiEmConfiguracoes()
        {
            config.ClicarEmConfiguracoes();
        }

        [Given(@"que clico em criar uma tarefa")]
        public void DadoQueClicoEmCriarUmaTarefa()
        {
            task.ClicarEmNovaTarefa();
        }

        [When(@"definir a empresa")]
        public void QuandoDefinirAEmpresa()
        {
            config.SetEmpresa();
        }

        [When(@"clicar em Salvar")]
        public void QuandoClicarEmSalvar()
        {
            config.Salvar();
        }

        [When(@"preencher os campos")]
        public void QuandoPreencherOsCampos()
        {

            task.PreencherTarefa(nomeTarefa, tags);
        }

        [Then(@"valido que foi salvo com sucesso")]
        public void EntaoValidoQueFoiSalvoComSucesso()
        {
            config.ObterMensagem();
        }

        [Then(@"valido que a tarefa foi criada com sucesso")]
        public void EntaoValidoQueATarefaFoiCriadaComSucesso()
        {
            task.ObterTarefacriada(nomeTarefa);
        }
    }
}
