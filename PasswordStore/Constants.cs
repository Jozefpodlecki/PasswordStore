using PasswordStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordStore
{
    public static class Constants
    {
        public const string Author = "Józef Podlecki";
        public const string ApplicationName = "Password Store";
        public const string ApplicationDescription = "Tray bar application for storing and retrieving passwords.";
        public const string Credits = "Icon made by Yannick from https://icon-icons.com";
        public const string ExportFilters = "XML (*.xml)|*.xml";
        public const string DefaultExportExtension = "xml";
        public const string DefaultBalloonTipTimeout = "DefaultBalloonTipTimeout";
        public const string DefaultImportFilter = "txt";
        public const string ImportFilters = "Text files (*.txt)|*.txt|XML Files (*.xml)|*.xml";

        public const string AboutWindowTitle = "About";
        public const string PasswordBytesEncoding = "PasswordBytesEncoding";
        public const string PasswordContextPopupWindowTitle = "Select context";
        public const string ApplicationSettings = "ApplicationSettings";
        public const string ApplicationFolderName = "ApplicationFolderName";
        public const string ExportPasswordsFileName = "ExportPasswordsFileName";
        public const string ImportPasswordsFileName = "ImportPasswordsFileName";
        public const string PasswordsFileName = "PasswordsFileName";
        public const string RSAFileName = "RSAFileName";
        public const string RSAKeySize = "RSAKeySize";
        public static Type RSAParametersType = typeof(RSAParameters);
        public static Type ListCredentialType = typeof(List<Credential>);
    }
}
