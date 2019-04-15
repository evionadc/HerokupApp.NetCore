using System;
using SeleniumXunit.Pages;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]


namespace SeleniumXunit.Core

{
    public class CoreTest : IDisposable
    {

        public CoreTest() {

            Inicializacao init = new Inicializacao();

            DriverFactory.GetDriver().Url = "https://mark7.herokuapp.com";

            init.Realizarlogin();

        }


       

        public void Dispose()
        {
            DriverFactory.KillDriver();
        }

    }
}
