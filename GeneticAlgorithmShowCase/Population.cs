using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace GeneticAlgorithmShowCase
{
    // The Nature of Code
    // Daniel Shiffman
    // http://natureofcode.com

    // Genetic Algorithm, Evolving Shakespeare

    // A class to describe a population of virtual organisms
    // In this case, each organism is just an instance of a DNA object

    // The Nature of Code
    // Daniel Shiffman
    // http://natureofcode.com

    // Genetic Algorithm, Evolving Shakespeare

    // A class to describe a population of virtual organisms
    // In this case, each organism is just an instance of a DNA object

    public class Population
    {

        double mutationRate;           // Mutation rate
        DNA[] population;             // Array to hold the current population
        System.Collections.Generic.List<DNA> matingPool;    // ArrayList which we will use for our "mating pool"
        String target;                // Target phrase
        int generations;              // Number of generations
        bool finished;             // Are we finished evolving?
        int perfectScore;

        public Population(String p, double m, int num)
        {
            target = p;
            mutationRate = m;
            population = new DNA[num];
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new DNA(target.Length);
            }
            calcFitness();
            matingPool = new List<DNA>();
            finished = false;
            generations = 0;

            perfectScore = 1;
        }

        // Fill our fitness array with a value for every member of the population
        public void calcFitness()
        {
            for (int i = 0; i < population.Length; i++)
            {
                population[i].GetFitness(target);
            }
        }

        // Generate a mating pool
        public void naturalSelection()
        {
            // Clear the ArrayList
            matingPool.Clear();

            double maxFitness = 0;
            for (int i = 0; i < population.Length; i++)
            {
                if (population[i].fitness > maxFitness)
                {
                    maxFitness = (double)population[i].fitness;
                }
            }

            // Based on fitness, each member will get added to the mating pool a certain number of times
            // a higher fitness = more entries to mating pool = more likely to be picked as a parent
            // a lower fitness = fewer entries to mating pool = less likely to be picked as a parent
            for (int i = 0; i < population.Length; i++)
            {

                float fitness = map((float)population[i].fitness, 0, (float)maxFitness, 0, 1);
                fitness = (double.IsNaN(fitness)) ? 0 : fitness;
                int n = (int)(fitness * 100);  // Arbitrary multiplier, we can also use monte carlo method
                for (int j = 0; j < n; j++) // and pick two random numbers
                {             
                    matingPool.Add(population[i]);
                }
                
            }
        }
        public  float map( float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        // Create a new generation
        public void generate()
        {
            // Refill the population with children from the mating pool
            for (int i = 0; i < population.Length; i++)
            {
                
                int a = (int)(Helper.GenerateRandomNumber(0,matingPool.Count));
                int b = (int)(Helper.GenerateRandomNumber(0, matingPool.Count));
                DNA partnerA = matingPool.ElementAt(a);
                DNA partnerB = matingPool.ElementAt(b);
                DNA child = partnerA.crossover(partnerB);
                child.mutate(mutationRate);
                population[i] = child;
               
            }
            generations++;
        }


        // Compute the current "most fit" member of the population
        public string getBest()
        {
            double worldrecord = 0.0;
            int index = 0;
            for (int i = 0; i < population.Length; i++)
            {
                if (population[i].fitness > worldrecord)
                {
                    index = i;
                    worldrecord = population[i].fitness;
                }
            }

            if (worldrecord == perfectScore) finished = true;
            return population[index].getPhrase();
        }

        public bool isFinished()
        {
            return finished;
        }

        public int getGenerations()
        {
            return generations;
        }

        // Compute average fitness for the population
        public double getAverageFitness()
        {
            double total = 0;
            for (int i = 0; i < population.Length; i++)
            {
                total += population[i].fitness;
            }
            return total / (population.Length);
        }

        public string allPhrases()
        {
            String everything = "";

            int displayLimit = Math.Min(population.Length, 50);


            for (int i = 0; i < displayLimit; i++)
            {
                everything += population[i].getPhrase() + "\n";
            }
            return everything;
        }
    }


}
