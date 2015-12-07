using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scanner scanner;
            
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            scanner = new Scanner("192.168.1", 1, 20);
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(

                delegate (object o, DoWorkEventArgs args)
                {
                    List<Device> devices;
                    BackgroundWorker b = o as BackgroundWorker;
                    devices = scanner.ScanNetwork();
                    args.Result = devices;
                });
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args){
                    label1.Content = "Finished";
                    List<Device> devices = (List<Device>)args.Result;
                    foreach(Device d in devices)
                    {
                        this.listView.Items.Add(d.address + " " + d.name);
                    }
                    
            });
            bw.RunWorkerAsync();
        }
    }
}
