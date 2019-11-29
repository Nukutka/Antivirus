using System;
using System.Collections.Generic;
using System.Text;

namespace Antivirus.Heuristic
{
    public class HeuristicDirectoryResult
    {
        public List<HeuristicFileResult> FileResults { get; set; }

        public HeuristicDirectoryResult()
        {
            FileResults = new List<HeuristicFileResult>();
        }
    }

    public class HeuristicFileResult
    {
        public string FileName { get; set; }
        public double Infected { get; set; }
        public double Uninfected { get; set; }

        public HeuristicFileResult(string fileName, double infected, double uninfected)
        {
            FileName = fileName;
            Infected = infected;
            Uninfected = uninfected;
        }
    }
}
