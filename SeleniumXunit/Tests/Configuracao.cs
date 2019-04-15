using SeleniumXunit.Core;
using Xunit;
using SeleniumXunit.Pages;

namespace SeleniumXunit.Tests
{
    public class Configuracao : CoreTest
    {

        ConfiguracaoPage cp = new ConfiguracaoPage();

        [Fact(DisplayName = "Alterar a Empresa")]
        public void Teste01AlterarEmpresa()
        {

            cp.ClicarEmConfiguracoes();
            cp.SetEmpresa();
            cp.Salvar();

            Assert.Equal("Perfil atualizado com sucesso.", cp.ObterMensagem());

        }
    }
}
