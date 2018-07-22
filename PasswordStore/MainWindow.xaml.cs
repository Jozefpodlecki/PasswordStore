using PasswordStore.Model;
using PasswordStore.Service;
using PasswordStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PasswordStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MenuItem CopyPasswordToClipboardMenuItem { get; }
        public MenuItem ExportPasswordsMenuItem { get; }
        public NotifyIcon TrayBarMenu { get; }

        public MainWindow()
        {
            InitializeComponent();

            var config = new Configuration();

            if (!Directory.Exists(config.ApplicationFolderPath))
            {
                Directory.CreateDirectory(config.ApplicationFolderPath);
            }
            
            var credentialService = new CredentialService(config);

            var model = new TrayBarMenuViewModel(credentialService, config, this);
            DataContext = model;

            Visibility = Visibility.Hidden;

            TrayBarMenu = new NotifyIcon
            {
                Text = Constants.ApplicationName,
                Icon = new Icon(Properties.Resources.IconInverted, 40, 40)
            };

            var menu = new ContextMenu();

            CopyPasswordToClipboardMenuItem = menu.MenuItems.Add("Copy password to clipboard", model.CopyPasswordToClipboard);
            menu.MenuItems.Add("Import passwords", model.ImportPasswords);
            ExportPasswordsMenuItem = menu.MenuItems.Add("Export passwords", model.ExportPasswords);
            menu.MenuItems.Add("Set encryption", model.SetEncryption);
            menu.MenuItems.Add("About", model.About);
            menu.MenuItems.Add("Exit", model.Exit);

            TrayBarMenu.ContextMenu = menu;
            TrayBarMenu.Visible = true;
            
            if (!credentialService.Credentials.Any())
            {
                TrayBarMenu.ShowBalloonTip(config.DefaultBalloonTipTimeout, "Passwords", "Please load passwords.", ToolTipIcon.Info);

                TrayBarMenu.BalloonTipClicked += model.ImportPasswords;

                CopyPasswordToClipboardMenuItem.Enabled = false;
                ExportPasswordsMenuItem.Enabled = false;
            }
        }
    }
}