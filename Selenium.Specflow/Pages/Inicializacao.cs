using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Specflow.Core;

namespace Selenium.Specflow.Pages
{
    public class Inicializacao : CorePage
    {
        private RemoteWebDriver driver;

        public Inicializacao(RemoteWebDriver _driver) : base(_driver)
        {
            this.driver = _driver;
        }
        public void Realizarlogin()
        {
            Escrever("login_email", "michell.soares@outlook.com");
            Escrever("login_password", "jasmim30");

            ClicarBotao("lockedtop_0$ctl07$lockedtop_0$btnLoginctl07$lockedtop_0$ctl05$");

            EsperarElemento(By.XPath("//*[@id='task-board']//h3"));

        }
    }
}
