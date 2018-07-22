using PasswordStore.Model;
using PasswordStore.Service;
using PasswordStore.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace PasswordStore
{
    /// <summary>
    /// Interaction logic for PasswordContextPopup.xaml
    /// </summary>
    public partial class PasswordContextPopup : Window
    {
        public PasswordContextPopup(CredentialService credentialService)
        {
            InitializeComponent();

            var model = new PasswordContextPopupViewModel(credentialService,this);
            DataContext = model;
        }
    }
}
