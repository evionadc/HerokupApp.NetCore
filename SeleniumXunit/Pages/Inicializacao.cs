using System;
using OpenQA.Selenium;
using SeleniumXunit.Core;

namespace SeleniumXunit.Pages
{
    public class Inicializacao : CorePage
    {
        public void Realizarlogin()
        {
            Escrever("login_email", "michell.soares@outlook.com");
            Escrever("login_password", "jasmim30");

            ClicarBotao("lockedtop_0$ctl07$lockedtop_0$btnLoginctl07$lockedtop_0$ctl05$");

            EsperarElemento(By.XPath("//*[@id='task-board']//h3"));

        }
    }
}
