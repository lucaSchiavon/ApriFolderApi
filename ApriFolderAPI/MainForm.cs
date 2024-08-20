using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ApriFolderAPI
{
    public partial class MainForm : Form
    {
        private NotifyIcon _trayIcon;
        private ContextMenuStrip _trayMenu;
        public MainForm()
        {
            InitializeComponent();
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            // Creazione del menu della tray icon
            _trayMenu = new ContextMenuStrip();
            _trayMenu.Items.Add("Esci",null, OnExit);

            // Creazione della tray icon
            _trayIcon = new NotifyIcon
            {
                Text = "OpenFolderClient",
                Icon = new Icon(SystemIcons.Application, 40, 40),
                ContextMenuStrip = _trayMenu,
                Visible = true
            };
        }
        private void OnExit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            Application.Exit();
        }
        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Nasconde la finestra
            ShowInTaskbar = false; // Nasconde l'icona nella taskbar
            base.OnLoad(e);
        }

    }
}
