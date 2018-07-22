using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace PasswordStore
{
    public class Configuration
    {
        public Encoding Encoding { get; set; }
        
        public string ApplicationDataPath { get; set; }
        public string ApplicationFolderName { get; set; }
        public string ApplicationFolderPath { get; set; }
        public string RSAFileName { get; set; }
        public string RSAFilePath { get; set; }
        public string ExportPasswordsFileName { get; set; }
        public string ImportPasswordsFileName { get; set; }
        public string PasswordsFileName { get; set; }
        public string PasswordsFilePath { get; set; }
        public int RSAKeySize { get; set; }
        public int DefaultBalloonTipTimeout { get; set; }

        public Configuration()
        {
            var applicationSettings = ConfigurationManager.GetSection(Constants.ApplicationSettings) as NameValueCollection;

            ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ApplicationFolderName = applicationSettings[Constants.ApplicationFolderName];
            ApplicationFolderPath = $"{ApplicationDataPath}\\{ApplicationFolderName}";

            RSAFileName = applicationSettings[Constants.RSAFileName];
            RSAFilePath = $"{ApplicationFolderPath}\\{RSAFileName}";
            RSAKeySize = int.Parse(applicationSettings[Constants.RSAKeySize]);
            PasswordsFileName = applicationSettings[Constants.PasswordsFileName];
            PasswordsFilePath = $"{ApplicationFolderPath}\\{PasswordsFileName}";
            ExportPasswordsFileName = applicationSettings[Constants.ExportPasswordsFileName];
            ImportPasswordsFileName = applicationSettings[Constants.ImportPasswordsFileName];

            DefaultBalloonTipTimeout = int.Parse(applicationSettings[Constants.DefaultBalloonTipTimeout]);
            var encoding = applicationSettings[Constants.PasswordBytesEncoding];
            Encoding = (Encoding)Activator.CreateInstance(Type.GetType("System.Text." + encoding));
        }
    }
}
