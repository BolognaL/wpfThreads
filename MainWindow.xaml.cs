using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace lorenzo.bologna._4i.wpfthreads2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int giri = 1000;
        int counter = 0;
        static readonly object locker = new object();
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread1 = new Thread(incremento1); //thread secondario
            thread1.Start();
            Thread thread2 = new Thread(incremento2); //thread secondario
            thread2.Start();

        }
        //i 2 thread non possono usare uno le risorse dell'altro 
        private void incremento1() //processo lento che dobbiamo lanciare 
        {
            for (int x = 0; x < giri; x++)
            {
                counter++;
                Dispatcher.Invoke( //fa si che i 2 thread possono collaborare (rinfreasca lo schermo)
                    () =>
                    {
                        lblCounter1.Text = counter.ToString(); //posso lanciare qunati thread voglio che lavoreranno sempre sullo stesso txt ma sovrapposti 
                    }
                    );
                
                Thread.Sleep(10);
            }
        }
        private void incremento2() 
        {
            for (int x = 0; x < giri; x++)
            {
                lock (locker) 
                {
                    counter++;
                }
                
                Dispatcher.Invoke( 
                    () =>
                    {
                        lblCounter2.Text = counter.ToString(); 
                    }
                    );

                Thread.Sleep(10);
            }
        }
    }
}
