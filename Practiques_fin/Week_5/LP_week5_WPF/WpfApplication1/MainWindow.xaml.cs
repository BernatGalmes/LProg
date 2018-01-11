using System;
using System.Collections.Generic;
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
using System.ComponentModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();


        public MainWindow()
        {
            InitializeComponent();
            //listaPrimeros.Columns[0].Width = 70;
            InitializeBackgroundWorker();
        }

        // Set up the BackgroundWorker object by
        // attaching event handlers. 
        private void InitializeBackgroundWorker()
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork +=
                new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            worker_RunWorkerCompleted);
            worker.ProgressChanged +=
                new ProgressChangedEventHandler(
            worker_ProgressChanged);
        }

        /// <summary>
        /// Reports the state of the execution every time each prime number is calculated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            listaPrimeros.Items.Add(e.UserState);

            this.progress.Value = e.ProgressPercentage;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            //e.Result = ComputeFibonacci((int)e.Argument, worker, e);

            int max = (int)e.Argument;
            
            int i = 1;
            //int ini_porc = 0;
            while (i < max)
            {
                double porc_completed = (Convert.ToDouble(i) / Convert.ToDouble(max)) * 100;
                worker.ReportProgress(Convert.ToInt32(porc_completed), i);
                i = nextPrime(i, max);                
            }
            
        }

        private void worker_RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
           // this.progress.Value = 100;
            listaPrimeros.Items.Add("Fin.");
        }

        private void ComputarPrimeros(object sender, RoutedEventArgs e)
        {
            this.progress.Value = 0;
            int max = Int32.Parse(max_comp.Text);
            listaPrimeros.Items.Clear();
            if (cb_runBG.IsChecked==true) //ejecutamos en segundo plano
            {
                worker.RunWorkerAsync(max);
            }else
            {
                posaPrimers(max);
            }
        }

        private void posaPrimers(int max)
        {
            int i = 1;
            while (i < max)
            {
                listaPrimeros.Items.Add(i);
                i = nextPrime(i, max);
            }
        }


        private int nextPrime(int current, int max)
        {
            int aux = current +1;
            while (!isPrimer(aux) && aux < max)
            {
                aux++;
            }
            return aux;
        }

        /// <summary>
        /// Check if the given number is prime
        /// </summary>
        /// <param name="current">Number to check</param>
        /// <returns>true if the number is prime, false otherwhise</returns>
        private Boolean isPrimer(int current)
        {
            for (int i = 2; i < current; i++)
            {
                if (current % i == 0) return false;
            }
            return true;
        }
        
    }
}
