using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P1GreenD
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            //My variables
            string[] gasNames = new string[100];
            double[] molecularWeights = new double[100];

            double Pressure = 0.0;
            double R = 8.3145;
            double TempKelvin = 0.0;
            double TempCelsius = 0.0;
            double Volume = 0.0;
            double NumMolesGas = 0.0;
            double Mass = 0.0;
            double MolWeight = 0.0;

            DisplayHeader();

            //User Enters information
            string gasName = "";

            Console.Write("Enter Temperature: ");
            TempCelsius = double.Parse(Console.ReadLine());

            Console.Write("Enter Volume: ");
            Volume = double.Parse(Console.ReadLine());

            Console.Write("Enter Mass of Gas: ");
            Mass = double.Parse(Console.ReadLine());

            //Call to Function
            

            GetMolecularWeights(ref gasNames, ref molecularWeights, out int count);

            DisplayGasNames(gasNames, count);
            
            do
            {
                Console.Write("Enter Gas Name: ");
                gasName = Console.ReadLine();

                MolWeight = GetMolecularWeightFromName(gasName, gasNames, molecularWeights, count);
                if (MolWeight == 0.0)
                {
                    continue;
                }

                Pressure = GetPressure(Mass, Volume, TempCelsius, MolWeight);
                DisplayPressure(Pressure);

                Console.Write("Do you want to do another (y/n: ");
                var replay = Console.ReadLine();

                if (replay != "y")
                {
                    break;
                }
            } while (true);

            Console.WriteLine("Goodbye");
        }

        //Display Header function
        static void DisplayHeader()
        {
            Console.WriteLine("Name: Daria Green");
            Console.WriteLine("Program Title: Ideal Gas Calculator");
            Console.WriteLine("Program Objective: To calculate the Pressure in both pascals and PSI");
        }

        //Get Molecular Weights Function
        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            //Reading Data from file
            string line = null;
            count = 0;
            StreamReader readData = new StreamReader("MolecularWeightsGasesAndVapors.csv");
            line = readData.ReadLine(); //reads the header

            while ((line = readData.ReadLine()) != null)
            {
                var gasName = line.Split(',')[0];
                var molecularWeight = line.Split(',')[1];

                var molecularWeightDouble = double.Parse(molecularWeight);

                gasNames[count] = gasName;
                molecularWeights[count] = molecularWeightDouble;

                ++count;

            }
        }
        //DisplayGasNames Funtion
        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            for (int index = 0; index < countGases;)
            {
                var columnOne = gasNames[index];
                ++index;
                var columnTwo = "";
                if (index < countGases)
                {
                    columnTwo = gasNames[index];
                }
                ++index;
                var columnThree = "";
                if (index < countGases)
                {
                    columnThree = gasNames[index];
                }
                Console.WriteLine("{0,20}{1,20}{2,20}",
                columnOne,
                columnTwo,
                columnThree); //StackOverflow: https://stackoverflow.com/questions/4449021/how-can-i-align-text-in-columns-using-console-writeline
            }
        }
        //GetMolecularWeightFromName setup here   
        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
           var index = Array.IndexOf(gasNames, gasName);

                if (index < 0 )
                {
                    Console.WriteLine("ERROR: Gas Not Found");
                    return 0;
                }

                var molecularWeight = molecularWeights[index];
                return molecularWeight;

        }
        static double NumberOfMoles(double mass, double molecularWeight)
        {
            return mass / molecularWeight;
        }
        static double CelsiusToKelvin(double celsius)
        {
            return celsius + 273.15;
        }
        static double GetPressure(double mass, double vol, double temp, double molecularWeight)
        {
             var tempKelvin = CelsiusToKelvin(temp);
             var numberOfMoles = NumberOfMoles(mass, molecularWeight);

             return (numberOfMoles * 8.3145 * tempKelvin) / vol;
        }

        static double PaToPsi(double pascals)
        {
            return pascals * 0.0001450377;
        }
        private static void DisplayPressure(double pressure)
        {
            var psi = PaToPsi(pressure);
            Console.WriteLine("Pascals: " + pressure);
            Console.WriteLine("PSI: " + psi);
        }
    }
}
  












         

