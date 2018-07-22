using PasswordStore.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace PasswordStore.Service
{
    public class CredentialService
    {
        public List<Credential> Credentials { get; set; }
        private readonly Configuration _configuration;
        private readonly Encoding _encoding;
        private RSACryptoServiceProvider _rsaCryptoServiceProvider { get; set; }
        private XmlSerializer _serializer { get; set; }

        public CredentialService(Configuration configuration)
        {
            _configuration = configuration;
            _encoding = _configuration.Encoding;
            _rsaCryptoServiceProvider = new RSACryptoServiceProvider(_configuration.RSAKeySize);

            Load();
        }

        public void Load()
        {
            if (File.Exists(_configuration.RSAFilePath))
            {
                _serializer = new XmlSerializer(Constants.RSAParametersType);

                var reader = new StreamReader(_configuration.RSAFilePath);
                var rsaParams = (RSAParameters)_serializer.Deserialize(reader);

                reader.Close();

                _rsaCryptoServiceProvider.ImportParameters(rsaParams);
            }
            else
            {
                SetEncryption();
            }

            if (File.Exists(_configuration.PasswordsFilePath))
            {

                _serializer = new XmlSerializer(Constants.ListCredentialType);

                var reader = new StreamReader(_configuration.PasswordsFilePath);
                Credentials = (List<Credential>)_serializer.Deserialize(reader);

                reader.Close();
            }
            else
            {
                Credentials = new List<Credential>();
            }
        }

        public void ImportCredentials(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            var stream = new StreamReader(File.Open(filePath, FileMode.Open));

            FileStream passwordsStream = null;

            if (extension == ".xml")
            {
                try
                {
                    _serializer = new XmlSerializer(Constants.ListCredentialType);

                    var credentials = (List<Credential>)_serializer.Deserialize(stream);

                    foreach (var credential in credentials)
                    {
                        var passwordBytes = _encoding.GetBytes(credential.Password);
                        var passwordEncrypted = _rsaCryptoServiceProvider.Encrypt(passwordBytes, false);
                        credential.PasswordEncrypted = passwordEncrypted;
                        credential.Password = null;
                    }

                    stream.Close();

                    Credentials = credentials;

                    passwordsStream = File.Create(_configuration.PasswordsFilePath);

                    _serializer = new XmlSerializer(Constants.ListCredentialType);

                    _serializer.Serialize(passwordsStream, credentials);

                    passwordsStream.Close();
                }
                catch(Exception exception)
                {
                    MessageBox.Show("Error while importing","File is in wrong format");
                }

                return;
            }

            Credentials = new List<Credential>();

            do
            {
                var line = stream.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var items = line.Split(' ');

                if (items.Length < 3)
                {
                    System.Windows.MessageBox.Show("Error", "Line at should be Context Login Password" + items[0]);
                    return;
                }

                var passwordBytes = _encoding.GetBytes(items[2]);

                var passwordEncrypted = _rsaCryptoServiceProvider.Encrypt(passwordBytes, false);

                Credentials.Add(new Credential
                {
                    Name = items[0],
                    Login = items[1],
                    PasswordEncrypted = passwordEncrypted
                });
            }
            while (!stream.EndOfStream);

            stream.Close();

            passwordsStream = File.Create(_configuration.PasswordsFilePath);

            _serializer = new XmlSerializer(Constants.ListCredentialType);

            _serializer.Serialize(passwordsStream, Credentials);

            passwordsStream.Close();
        }

        internal void SetEncryption()
        {
            _serializer = new XmlSerializer(Constants.RSAParametersType);

            var rsaStream = File.Create(_configuration.RSAFilePath);

            var rsaParameters = _rsaCryptoServiceProvider.ExportParameters(true);

            _serializer.Serialize(rsaStream, rsaParameters);

            rsaStream.Close();
        }

        public void ExportCredentials(string filePath)
        {
            var credentials = new List<Credential>(Credentials);

            foreach (var credential in credentials)
            {
                credential.Password = _encoding.GetString(_rsaCryptoServiceProvider.Decrypt(credential.PasswordEncrypted,false));
                credential.PasswordEncrypted = null;
            }

            var passwordsStream = File.Create(filePath);

            _serializer = new XmlSerializer(Constants.ListCredentialType);

            _serializer.Serialize(passwordsStream, credentials);

            passwordsStream.Close();
        }

        public string GetPassword(Credential credential)
        {
            return _encoding.GetString(_rsaCryptoServiceProvider.Decrypt(credential.PasswordEncrypted, false));
        }
    }
}
