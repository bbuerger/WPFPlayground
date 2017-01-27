using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Reflection;
using System.Windows.Threading;

namespace WPFPlayground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tilte = "NO UIO";
            var newThread = new Thread(() =>
            {
                // Create our context, and install it:
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
                Window test = new MainWindow();
                test.Title = tilte;

                test.ShowDialog();
            });
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.IsBackground = true;
            newThread.Start();
        }

    }

}
