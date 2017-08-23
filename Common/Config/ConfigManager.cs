using System;
using System.Configuration;

namespace Common.Config
{
    public class ConfigManager
    {

        public void aaa()
        {
            var a = AppDomain.CurrentDomain.BaseDirectory + @"..\Common\app.config";
        }
    }
}
