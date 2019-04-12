using System;
using Xunit;

namespace SeleniumXunit.Core

{
    public class CoreTest : IDisposable
    {

        public CoreTest() {

            DriverFactory.GetDriver().Url = "http://www.google.com.br";

        }


       

        public void Dispose()
        {
            DriverFactory.KillDriver();
        }

    }
}
