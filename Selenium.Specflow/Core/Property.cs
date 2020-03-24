using System;
namespace Selenium.Specflow.Core
{
    public class Property
    {
        public Property() { }

        public static Browsers _browser = Browsers.Chrome;

        public enum Browsers
        {
            Chrome,
            Firefox

        }

    }
}
