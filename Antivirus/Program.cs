using Antivirus.Heuristic;
using Antivirus.Signature;
using System;
using System.Linq;
using System.Text;

namespace Antivirus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter directory: ");
            var directory = Console.ReadLine();

            var signatureAnalyzer = new SignatureAnalyzer();
            var heuristicAnalyzer = new HeuristicAnalyzer();

            var signatureAnalisysRes = signatureAnalyzer.CheckDirectory(directory);

            Console.WriteLine("Signature analysis:");
            foreach (var response in signatureAnalisysRes.FileResults)
            {
                Console.WriteLine($"{response.FileName} {string.Join(' ', response.Viruses)}");
            }

            var hueristicAnalisysRes = heuristicAnalyzer.CheckDirectory(directory);

            Console.WriteLine("\nHueristic analysis:");
            foreach (var response in hueristicAnalisysRes.FileResults)
            {
                Console.WriteLine($"{response.FileName} {Math.Round(response.Infected, 2)} {Math.Round(response.Uninfected, 2)}");
            }
        }
    }
}
