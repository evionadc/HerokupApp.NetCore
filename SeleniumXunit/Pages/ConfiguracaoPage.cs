using System;
using Faker;
using OpenQA.Selenium;
using SeleniumXunit.Core;

namespace SeleniumXunit.Pages
{
    public class ConfiguracaoPage : CorePage
    {
        public void ClicarEmConfiguracoes()
        {

            ClicarBotao(By.XPath("//ul[@class='nav luna-nav']/li/a[@href='/user_settings']"));

        }

        public void SetEmpresa()
        {

            var com = Company.Name();

            EsperarElemento("profile-company");
            Escrever("profile-company", com);

        }

        public void Salvar()
        {

            ClicarBotao("form-submit-button");
        }

        public string ObterMensagem()
        {
            EsperarElemento(By.XPath("//form//div[@class='panel-body']"));
            return obterTexto(By.XPath("//form//div[@class='panel-body']"));
        }
    }
}
