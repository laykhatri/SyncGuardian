using Serilog;
using SyncGuardianWpf.Services.Interfaces;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SyncGuardianWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;
        public MainWindow(ILogger logger)
        {
            _logger = logger;
            InitializeComponent();
        }
    }
}
