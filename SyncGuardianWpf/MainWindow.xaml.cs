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
        public MainWindow()
        {
            CreateLogsFolder();
            InitializeComponent();
        }

        private void CreateLogsFolder()
        {
            var assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            var logsPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(assemblyPath, @"..\..\..\","Logs"));
            if (!Directory.Exists(logsPath))
            {
                Directory.CreateDirectory(logsPath);
            }
        }
    }
}
