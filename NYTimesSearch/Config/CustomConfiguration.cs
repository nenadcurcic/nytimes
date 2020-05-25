using System;
using System.Configuration;

namespace NYTimesSearch.Config
{
    public class CustomConfiguration : ConfigurationSection
    {
        public static CustomConfiguration Settings { get; } = ConfigurationManager.GetSection("CustomConfig") as CustomConfiguration;

        [ConfigurationProperty("apiKey", IsRequired = true)]
        public string ApiKey
        {
            get { return (string)this["apiKey"]; }
            set { this["apiKey"] = value; }
        }


        [ConfigurationProperty("apiUrl", IsRequired = true)]
        public string ApiUrl
        {
            get { return (string)this["apiUrl"]; }
            set { this["apiUrl"] = value; }
        }
    }
}