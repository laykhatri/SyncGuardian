using Serilog;
using System;
using System.IO;
using System.Windows;
using System.Windows.Shapes;

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
            InitializeComponent();
            _logger = logger;
            _logger.Information("MainWindow");
        }
    }
}
