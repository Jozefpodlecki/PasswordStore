using PasswordStore.Common;
using PasswordStore.Model;
using PasswordStore.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordStore.ViewModel
{
    public class PasswordContextPopupViewModel : INotifyPropertyChanged
    {
        private readonly CredentialService _credentialService;
        public ReadOnlyCollection<Credential> Credentials { get; set; }
        private readonly Window _window;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SelectionChangedCommand { get; set; }

        public PasswordContextPopupViewModel(
            CredentialService credentialService,
            Window window)
        {
            _credentialService = credentialService;
            _window = window;
            Credentials = _credentialService.Credentials.AsReadOnly();
            SelectionChangedCommand = new Command<Credential>(SetPasswordToClipboard);
        }

        public void SetPasswordToClipboard(Credential credential)
        {
            Clipboard.SetText(_credentialService.GetPassword(credential));

            _window.Close();
        }
    }
}
