using PasswordStore.Service;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PasswordStore.ViewModel
{
    public class TrayBarMenuViewModel
    {
        private readonly CredentialService _credentialService;
        private readonly Configuration _configuration;
        private readonly MainWindow _window;
        private readonly OpenFileDialog _openFileDialog;
        private readonly SaveFileDialog _saveFileDialog;

        public TrayBarMenuViewModel(
            CredentialService credentialService,
            Configuration configuration,
            MainWindow window)
        {
            _credentialService = credentialService;
            _configuration = configuration;
            _window = window;

            _openFileDialog = new OpenFileDialog();
            _openFileDialog.FileName = _configuration.ImportPasswordsFileName;
            _openFileDialog.Filter = Constants.ImportFilters;
            _openFileDialog.DefaultExt = Constants.DefaultImportFilter;

            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.FileName = _configuration.ExportPasswordsFileName;
            _saveFileDialog.Filter = Constants.ExportFilters;
            _saveFileDialog.DefaultExt = Constants.DefaultExportExtension;
        }

        public void CopyPasswordToClipboard(object sender, EventArgs e)
        {
            var window = new PasswordContextPopup(_credentialService);
            
            window.ShowDialog();
        }

        public void ImportPasswords(object sender, EventArgs e)
        {
            var result = _openFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            _credentialService.ImportCredentials(_openFileDialog.FileName);

            if (_credentialService.Credentials.Any())
            {
                _window.CopyPasswordToClipboardMenuItem.Enabled = true;
                _window.ExportPasswordsMenuItem.Enabled = true;
            }
            else
            {
                _window.TrayBarMenu.ShowBalloonTip(_configuration.DefaultBalloonTipTimeout, "Passwords", "Uploaded file has no entries. Please load passwords.", ToolTipIcon.Info);
                _window.CopyPasswordToClipboardMenuItem.Enabled = false;
                _window.ExportPasswordsMenuItem.Enabled = false;
            }
        }

        public void About(object sender, EventArgs e)
        {
            var window = System.Windows.Application.Current.Windows.OfType<AboutWindow>().FirstOrDefault();

            if (window == null)
            {
                window = new AboutWindow();
                window.ShowDialog();
            }
            else
            {
                window.Focus();
            }
        }

        public void ExportPasswords(object sender, EventArgs e)
        {
            var result = _saveFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            _credentialService.ExportCredentials(_saveFileDialog.FileName);
        }

        public void SetEncryption(object sender, EventArgs e)
        {
            _credentialService.SetEncryption();
        }

        public void Exit(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
