using System;

namespace Antivirus.Heuristic
{
    public class Condition
    {
        public string Y { get; set; }
        public bool Value { get; set; }
        public Func<byte[], bool> F { get; set; }

        public Condition(string y, bool value, Func<byte[], bool> f)
        {
            Y = y;
            Value = value;
            F = f;
        }
    }

    public class FuzzySet
    {
        public string Value { get; set; }
        public double Mu { get; set; }

        public FuzzySet(string value, double mu)
        {
            Value = value;
            Mu = mu;
        }
    }

    public class FuzzySets
    {
        public FuzzySet Infected { get; set; }
        public FuzzySet Uninfected { get; set; }

        public FuzzySets(FuzzySet infected, FuzzySet uninfected)
        {
            Infected = infected;
            Uninfected = uninfected;
        }
    }

    public class Conclusion
    {
        public string Variable { get; set; }
        public FuzzySets FuzzySets { get; set; }
        public double Alpha { get; set; }
        public Conclusion(string variable, FuzzySets fuzzySets, double alpha)
        {
            Variable = variable;
            FuzzySets = fuzzySets;
            Alpha = alpha;
        }
    }

    public class Production
    {
        public Condition Condition { get; set; }
        public Conclusion Conclusion { get; set; }
        public double Beta { get; set; }

        public Production(Condition condition, Conclusion conclusion, double beta)
        {
            Condition = condition;
            Conclusion = conclusion;
            Beta = beta;
        }
    }
}
