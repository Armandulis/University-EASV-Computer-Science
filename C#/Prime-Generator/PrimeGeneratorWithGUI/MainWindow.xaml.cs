using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace PrimeGeneratorWithGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PrimeGenerator primeGenerator;

        public MainWindow()
        {
            InitializeComponent();
            primeGenerator = new PrimeGenerator();
        }

        private async void GeneratePrimesInParaller(object sender, RoutedEventArgs e)
        {
            ObservableCollection<long> primes = new ObservableCollection<long>();
            Stopwatch stopwatch = new Stopwatch();
            getFromNumber(out long fromNumber);
            getToNumber(out long toNumber);
            stopwatch.Reset();
            stopwatch.Start();
            GeneartionParallerButton.Content = "Generating..";

            Task Generation = Task.Factory.StartNew(() =>
            {
                return primeGenerator.generatePrimesInParaller(fromNumber, toNumber);
            }).ContinueWith((task) =>
            {
                primes = new ObservableCollection<long>(task.Result);
            });

            await Generation;
            GeneartionParallerButton.Content = "Generate Primes in Paraller";
            stopwatch.Stop();
            GeneartionTimeParallerLabel.Content = "Generated in: " + stopwatch.Elapsed;
            AmountGeneratedParallerLabel.Content = primes.Count();
        }

        private async void GeneratePrimes(object sender, RoutedEventArgs e)
        {
            ObservableCollection<long> primes = new ObservableCollection<long>();
            Stopwatch stopwatch = new Stopwatch();
            getFromNumber(out long fromNumber);
            getToNumber(out long toNumber);
            stopwatch.Start();
            GeneartionButton.Content = "Generating..";

            Task Generation = Task.Factory.StartNew(() =>
            {
                return primeGenerator.GetPrimesSequential(fromNumber, toNumber);
            }).ContinueWith((task) =>
            {
                primes = new ObservableCollection<long>(task.Result);
            });

            await Generation;

            GeneartionButton.Content = "Generate Primes";
            stopwatch.Stop();
            GenerationTimeLabel.Content = "Generated in: " + stopwatch.Elapsed;
            AmountGeneratedLabel.Content = primes.Count();
        }

        private void getFromNumber(out long fromNumber)
        {
            long.TryParse(FromNumberInput.Text, out fromNumber);
            return;
        }

        private void getToNumber(out long toNumber)
        {
            long.TryParse(ToNumberInput.Text, out toNumber);
            return;
        }
    }
}
