using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Specflow.Core
{
    public class CorePage
    {

        [ThreadStatic]
        private static RemoteWebDriver driver;

        static int contador = 1;
        public static string dirEvidencia = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\Debug\\Evidencias";

        public CorePage(RemoteWebDriver _driver)
        {
            _driver = driver;
        }

        public static IWebDriver GetDriver()
        {

            return driver;
        }

        /********* TextField e TextArea ************/

        public void Escrever(By by, String texto)
        {
            GetDriver().FindElement(by).Clear();
            GetDriver().FindElement(by).SendKeys(texto);
        }

        public void Escrever(String id_campo, String texto)
        {
            Escrever(By.Id(id_campo), texto);
        }

        public String obterValorCampo(String id_campo)
        {
            return GetDriver().FindElement(By.Id(id_campo)).GetAttribute("value");
        }

        /********* Radio e Check ************/

        public void ClicarRadio(By by)
        {
            GetDriver().FindElement(by).Click();
        }

        public void ClicarRadio(String id)
        {
            ClicarRadio(By.Id(id));
        }

        public Boolean isRadioMarcado(String id)
        {
            return GetDriver().FindElement(By.Id(id)).Selected;
        }

        public void ClicarCheck(String id)
        {
            GetDriver().FindElement(By.Id(id)).Click();
        }

        public Boolean IsCheckMarcado(String id)
        {
            return GetDriver().FindElement(By.Id(id)).Selected;
        }

        /********* Combo ************/

        public void SelecionarCombo(String id, String valor)
        {
            IWebElement element = GetDriver().FindElement(By.Id(id));
            SelectElement combo = new SelectElement(element);
            combo.SelectByText(valor);
        }

        public void DeselecionarCombo(String id, String valor)
        {
            IWebElement element = GetDriver().FindElement(By.Id(id));
            SelectElement combo = new SelectElement(element);
            combo.DeselectByText(valor);
        }

        public String ObterValorCombo(String id)
        {
            IWebElement element = GetDriver().FindElement(By.Id(id));
            SelectElement combo = new SelectElement(element);
            return combo.SelectedOption.Text;
        }

        public List<String> ObterValoresCombo(String id)
        {
            IWebElement element = GetDriver().FindElement(By.Id("elementosForm:esportes"));
            SelectElement combo = new SelectElement(element);
            var allISelectedOptions = combo.AllSelectedOptions;
            List<String> valores = new List<string>();
            foreach (IWebElement opcao in allISelectedOptions)
            {
                valores.Add(opcao.Text);
            }
            return valores;
        }

        public int ObterQuantidadeOpcoesCombo(String id)
        {
            IWebElement element = GetDriver().FindElement(By.Id(id));
            SelectElement combo = new SelectElement(element);
            List<IWebElement> options = new List<IWebElement>();
            options = (System.Collections.Generic.List<OpenQA.Selenium.IWebElement>)combo.Options;
            return options.Count;
        }

        public Boolean VerificarOpcaoCombo(String id, String opcao)
        {
            IWebElement element = GetDriver().FindElement(By.Id(id));
            SelectElement combo = new SelectElement(element);
            var options = (System.Collections.Generic.List<OpenQA.Selenium.IWebElement>)combo.Options;
            foreach (IWebElement option in options)
            {
                if (option.Text.Equals(opcao))
                {
                    return true;
                }
            }
            return false;
        }

        public void SelecionarComboPrime(String radical, String valor)
        {
            ClicarRadio(By.XPath("//*[@id='" + radical + "_input']/../..//span"));
            ClicarRadio(By.XPath("//*[@id='" + radical + "_items']//li[.='" + valor + "']"));
        }

        /********* Botao ************/

        public void ClicarBotao(By by)
        {
            GetDriver().FindElement(by).Click();
        }
        public void ClicarBotao(String id)
        {
            ClicarBotao(By.Id(id));
        }

        public void ClicarBotaoPorTexto(String texto)
        {
            ClicarBotao(By.XPath("//button[.='" + texto + "']"));
        }

        public String obterValueElemento(String id)
        {
            return GetDriver().FindElement(By.Id(id)).GetAttribute("value");
        }

        /********* Link ************/

        public void ClicarLink(String link)
        {
            GetDriver().FindElement(By.LinkText(link)).Click();
        }

        /********* Textos ************/

        public String obterTexto(By by)
        {
            return GetDriver().FindElement(by).Text;
        }

        public String obterTexto(String id)
        {
            return obterTexto(By.Id(id));
        }

        /********* Alerts ************/

        public String alertaObterTexto()
        {
            IAlert alert = GetDriver().SwitchTo().Alert();
            return alert.Text;
        }

        public String alertaObterTextoEAceita()
        {
            IAlert alert = GetDriver().SwitchTo().Alert();
            String valor = alert.Text;
            alert.Accept();
            return valor;

        }

        public String alertaObterTextoENega()
        {
            IAlert alert = GetDriver().SwitchTo().Alert();
            String valor = alert.Text;
            alert.Dismiss();
            return valor;

        }

        public void AlertaEscrever(String valor)
        {
            IAlert alert = GetDriver().SwitchTo().Alert();
            alert.SendKeys(valor);
            alert.Accept();
        }

        /********* Frames e Janelas ************/

        public void EntrarFrame(String id)
        {
            GetDriver().SwitchTo().Frame(id);
        }

        public void SairFrame()
        {
            GetDriver().SwitchTo().DefaultContent();
        }

        public void TrocarJanela(String id)
        {
            GetDriver().SwitchTo().Window(id);
        }

        /************** JS *********************/

        public Object ExecutarJS(String cmd, String param)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)GetDriver();
            return js.ExecuteScript(cmd, param);
        }

        /************** Tabela *********************/

        public IWebElement ObterCelula(String colunaBusca, String valor, String colunaBotao, String idTabela)
        {
            //procurar coluna do registro
            IWebElement tabela = GetDriver().FindElement(By.XPath("//*[@id='" + idTabela + "']"));
            int idColuna = ObterIndiceColuna(colunaBusca, tabela);

            //encontrar a linha do registro
            int idLinha = ObterIndiceLinha(valor, tabela, idColuna);

            //procurar coluna do botao
            int idColunaBotao = ObterIndiceColuna(colunaBotao, tabela);

            //clicar no botao da celula encontrada
            IWebElement celula = tabela.FindElement(By.XPath(".//tr[" + idLinha + "]/td[" + idColunaBotao + "]"));
            return celula;
        }

        public void ClicarBotaoTabela(String colunaBusca, String valor, String colunaBotao, String idTabela)
        {
            IWebElement celula = ObterCelula(colunaBusca, valor, colunaBotao, idTabela);
            celula.FindElement(By.XPath(".//input")).Click();

        }

        protected int ObterIndiceLinha(String valor, IWebElement tabela, int idColuna)
        {
            IList<IWebElement> linhas = tabela.FindElements(By.XPath("./tbody/tr/td[" + idColuna + "]"));
            int idLinha = -1;
            for (int i = 0; i < linhas.Count; i++)
            {
                if (linhas[i].Text.Equals(valor))
                {
                    idLinha = i + 1;
                    break;
                }
            }
            return idLinha;
        }

        protected int ObterIndiceColuna(String coluna, IWebElement tabela)
        {
            IList<IWebElement> colunas = tabela.FindElements(By.XPath(".//th"));
            int idColuna = -1;
            for (int i = 0; i < colunas.Count; i++)
            {
                if (colunas[i].Text.Equals(coluna))
                {
                    idColuna = i + 1;
                    break;
                }
            }
            return idColuna;
        }

        /*************Esperas********************/

        public void EsperarElemento(By by)
        {
            WebDriverWait wait = new WebDriverWait(GetDriver(), new TimeSpan(00, 00, 10));
#pragma warning disable CS0618 // O tipo ou membro é obsoleto
            wait.Until(ExpectedConditions.ElementIsVisible(by));
#pragma warning restore CS0618 // O tipo ou membro é obsoleto
        }

        public void EsperarElemento(String id)
        {
            EsperarElemento(By.Id(id));
        }

        /*****Elementos*************/

        public Boolean obterElemento(By by)
        {
            try
            {
                if (GetDriver().FindElement(by).Displayed)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static String capturaImagem()
        {
            try
            {

                CreateDirectory(dirEvidencia);
                string feature = ExtentTestManager.FeatureName;
                string scenario = ExtentTestManager.ScenarioName;
                CreateDirectory($@"{dirEvidencia}\{feature}\{scenario}\prints");
                var dir = $@"{dirEvidencia}\{feature}\{scenario}\prints";
                var path2 = $@"{dir}";
                var contador_aux = contador++;
                var dataImagem = DateTime.Now.ToString("dd_MMMM_hh_mm_ss_tt");
                var imagemFinal = "Imagem_" + contador_aux + dataImagem + ".png";

                string img = $@"{path2}\" + imagemFinal;

                DriverFactory.Screenshot(img);
                var finalpath = "prints" + "\\" + imagemFinal;
                return finalpath;
            }
            catch
            {
                return "";
            }
        }
        private static void CreateDirectory(string path)
        {
            try
            {

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Não foi possível criar o diretorio" + e.ToString());
            }
        }
    }
}
