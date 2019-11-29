using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Antivirus.Heuristic
{
    public class HeuristicAnalyzer
    {
        public List<Production> Productions { get; set; }

        public HeuristicAnalyzer()
        {
            var fuzzySetJmpFirst = new FuzzySets(new FuzzySet("infected", 0.95), new FuzzySet("uninfected", 0.05));
            var fuzzySetManyZeros = new FuzzySets(new FuzzySet("infected", 0.9), new FuzzySet("uninfected", 0.1));
            var fuzzySetDangerousWords = new FuzzySets(new FuzzySet("infected", 0.7), new FuzzySet("uninfected", 0.5));

            var fuzzySetNotJmpFirst = new FuzzySets(new FuzzySet("infected", 0.1), new FuzzySet("uninfected", 0.6));
            var fuzzySetNotManyZeros = new FuzzySets(new FuzzySet("infected", 0.15), new FuzzySet("uninfected", 0.7));
            var fuzzySetNotDangerousWords = new FuzzySets(new FuzzySet("infected", 0.2), new FuzzySet("uninfected", 0.4));

            Productions = new List<Production>
            {
                new Production(new Condition("x1", true, HeuristicFunctions.CheckJmpFirstBytes), new Conclusion("y", fuzzySetJmpFirst, 0.95), 1),
                new Production(new Condition("x2", false, HeuristicFunctions.CheckNotJmpFirstBytes), new Conclusion("y", fuzzySetNotJmpFirst, 0.5), 1),

                new Production(new Condition("x3", true, HeuristicFunctions.CheckManyZeros), new Conclusion("y", fuzzySetManyZeros, 0.85), 1),
                new Production(new Condition("x4", false, HeuristicFunctions.CheckNotManyZeros), new Conclusion("y", fuzzySetNotManyZeros, 0.6), 1),

                new Production(new Condition("x5", true, HeuristicFunctions.CheckDangerousWords), new Conclusion("y", fuzzySetDangerousWords, 0.7), 1),
                new Production(new Condition("x6", false, HeuristicFunctions.CheckNotDangerousWords), new Conclusion("y", fuzzySetNotDangerousWords, 0.4), 1)
            };
        }

        public HeuristicDirectoryResult CheckDirectory(string path)
        {
            var directoryResult = new HeuristicDirectoryResult();

            foreach (var file in Directory.GetFiles(path))
            {
                var fileBytes = File.ReadAllBytes(file);

                double maxAlpha = Productions.Max(x => x.Conclusion.Alpha);
                double sumAlpha = 0;
                double infected = 0;
                double uninfected = 0;

                foreach (var production in Productions)
                {
                    production.Conclusion.Alpha /= maxAlpha;

                    if (production.Condition.F.Invoke(fileBytes) == production.Condition.Value)
                    {
                        sumAlpha += production.Conclusion.Alpha;
                        infected += production.Conclusion.Alpha * production.Conclusion.FuzzySets.Infected.Mu;
                        uninfected += production.Conclusion.Alpha * production.Conclusion.FuzzySets.Uninfected.Mu;
                    }
                }

                var infectedRes = sumAlpha != 0 ? infected / sumAlpha : 0;
                var uninfectedRes = sumAlpha != 0 ? uninfected / sumAlpha : 0;
                directoryResult.FileResults.Add(new HeuristicFileResult(new FileInfo(file).Name, infectedRes, uninfectedRes));
            }

            return directoryResult;
        }
    }
}
