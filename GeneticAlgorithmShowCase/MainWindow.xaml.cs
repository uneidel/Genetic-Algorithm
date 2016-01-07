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
namespace GeneticAlgorithmShowCase
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        BackgroundWorker worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

        }
        String target;
        int popmax;
        double mutationRate;
        Population population;

        void setup()
        {
            worker.WorkerReportsProgress = true;
            target = "Kaffee nächsten Freitag?";
            popmax = Convert.ToInt32(txtTotalPopulation.Text);
            mutationRate = Convert.ToDouble(txtMutationRate.Text);

            // Create a populationation with a target phrase, mutation rate, and populationation max
            population = new Population(target, mutationRate, popmax);
        }

        
     
        public class Info
        {
            public double AverageFitness { get; set; }
            public int PopulationMax { get; set; }
            public string AllPhrases { get; set; }
            public int Generations { get; set; }
            public string BestAnswer { get; set; }
            public double  MutationsRate { get; set; }

        }
    private void Run_Click(object sender, RoutedEventArgs e)
        {

            setup();
            worker.DoWork += delegate (object s, DoWorkEventArgs args)
            {
                    do
                    {           
                        // Generate mating pool
                        population.naturalSelection();
                        //Create next generation
                        population.generate();
                        // Calculate fitness
                        population.calcFitness();
                        // If we found the target phrase, stop
                        if (population.isFinished())
                        {
                        break;
                        }
                    Info i = new Info();
                    i.BestAnswer = population.getBest();
                    i.Generations = population.getGenerations();
                    i.AverageFitness = Math.Round(population.getAverageFitness(),4);
                    i.PopulationMax = popmax;
                    i.AllPhrases = population.allPhrases();
                    i.MutationsRate = mutationRate;
                    worker.ReportProgress(0,i);

                    }
                    while (true);
                };
            worker.ProgressChanged += delegate (object s, ProgressChangedEventArgs args)
            {
                Info info = (Info)args.UserState;
                txtBestAnswer.Content = info.BestAnswer;
                txtTotalGeneration.Content = info.Generations;
                txtAvgFitness.Content = info.AverageFitness.ToString();
                txtTotalPopulation.Text = Convert.ToString(popmax);
                txtMutationRate.Text = Convert.ToString(mutationRate);
                txtPhrases.Content = info.AllPhrases;

            };
            worker.RunWorkerCompleted += delegate (object se, RunWorkerCompletedEventArgs rwe)
            {

            };
            worker.RunWorkerAsync();
        }

       
    }
}