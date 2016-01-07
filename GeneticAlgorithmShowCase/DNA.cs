using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmShowCase
{
   

    internal class DNA
    {

        // The genetic sequence
        char[] genes;
        internal float fitness;
        
        
        // Constructor (makes a random DNA)
        internal DNA(int num)
        {
            genes = new char[num];
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = (char)GetLetter();  // Pick from range of chars
            }
        }
        public static char GetLetter()
        {
            string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?; .äüö:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
            Random rand = new Random(12314124);
            int num = Helper.GenerateRandomNumber(0, chars.Length - 1);
            return chars[num];

        }
       
        // Converts character array to a String
        internal string getPhrase()
        {
            return new string(genes);
        }

        // Fitness function (returns floating point % of "correct" characters)
        internal void GetFitness(string target)
        {
            int score = 0;
            for (int i = 0; i < genes.Length; i++)
            {
                if (genes[i] == target[i])
                {
                    score++;
                }
            }
            this.fitness = (float)score / (float)target.Length;
        }

        // Crossover
        internal DNA crossover(DNA partner)
        {
            // A new child
            DNA child = new DNA(genes.Length);

            int midpoint = (int)(Helper.GenerateRandomNumber(0,genes.Length)); // Pick a midpoint

            // Half from one, half from the other
            for (int i = 0; i < genes.Length; i++)
            {
                if (i > midpoint) child.genes[i] = genes[i];
                else child.genes[i] = partner.genes[i];
            }
            return child;
        }

        // Based on a mutation probability, picks a new random character
        internal void mutate(double mutationRate)
        {
            for (int i = 0; i < genes.Length; i++)
            {
                if (Helper.GenerateDoubleRandomNumber() < mutationRate)
                {
                    genes[i] = GetLetter();
                }
            }
        }
    }
}
