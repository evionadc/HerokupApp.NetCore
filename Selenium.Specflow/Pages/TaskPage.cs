using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Specflow.Core;

namespace Selenium.Specflow.Pages
{
    public class TaskPage : CorePage


    {
        private RemoteWebDriver driver;
        public TaskPage(RemoteWebDriver _driver) : base(_driver)
        {
            this.driver = _driver;
        }

        public void ClicarEmNovaTarefa()
        {
            ClicarBotao("insert-button");

        }

        public void PreencherTarefa(String nomeTarefa, String tags)
        {

            EsperarElemento("form-submit-button");
            Escrever("title", nomeTarefa);
            Escrever("dueDate", new DateTime().ToShortDateString());
            Escrever(By.XPath("//*[@id='add-task']/div[4]/div/div/input"), tags);

        }

        public void ClicarEmSalvar()
        {

            ClicarBotao("form-submit-button");
        }

        public String ObterTarefacriada(String nomeTarefa)
        {
            EsperarElemento(By.XPath("//table[@id='tasks']/tbody/tr/td/a[.='" + nomeTarefa + "']"));
            return obterTexto(By.XPath("//table[@id='tasks']/tbody/tr/td/a[.='" + nomeTarefa + "']"));
        }

        public void clicarEmEditarTarefa(String nomeTarefa)
        {
            EsperarElemento(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']/td[5]/div/button[@id='edit-button']"));
            ClicarBotao(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']/td[5]/div/button[@id='edit-button']"));
        }

        public void editarTarefa(String nomeTarefa)
        {

            EsperarElemento("form-submit-button");
            Escrever(By.Name("title"), nomeTarefa);
            ClicarRadio(By.Name("done"));

        }

        public String ObterStatusTarefa(String nomeTarefa)
        {
            EsperarElemento(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']/td[1]/span"));
            return obterTexto(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']/td[1]/span"));
        }

        public void ClicarEmExcluirTarefa(String nomeTarefa)
        {
            EsperarElemento(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']/td[5]/div/button[@id='delete-button']"));
            ClicarBotao(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']/td[5]/div/button[@id='delete-button']"));
        }

        public void ConfirmarExclusao()
        {
            EsperarElemento(By.XPath("//*[@class='btn btn-danger']"));
            ClicarBotao(By.XPath("//*[@class='btn btn-danger']"));
            Thread.Sleep(1000);
        }

        public Boolean ObterTarefa(String nomeTarefa)
        {

            return obterElemento(By.XPath("//table[@id='tasks']/tbody/tr[td/a='" + nomeTarefa + "']"));
        }
    }
}
