using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Antivirus.Heuristic
{
    public static class HeuristicFunctions
    {
        public static bool CheckJmpFirstBytes(byte[] fileBytes)
        {
            return fileBytes[0] == 0xE8 || fileBytes[0] == 0xE9 || fileBytes[0] == 0xFF || fileBytes[0] == 0xEA;
        }

        public static bool CheckNotJmpFirstBytes(byte[] fileBytes)
        {
            return !(fileBytes[0] == 0xE8 || fileBytes[0] == 0xE9 || fileBytes[0] == 0xFF || fileBytes[0] == 0xEA);
        }

        public static bool CheckManyZeros(byte[] fileBytes)
        {
            int maxZeroSequence = 0;
            int currentZeroSequence = 0;

            foreach (var fileByte in fileBytes)
            {

                if (fileByte == 0x00) currentZeroSequence++;
                else
                {
                    if (currentZeroSequence > maxZeroSequence)
                    {
                        maxZeroSequence = currentZeroSequence;
                    }

                    currentZeroSequence = 0;
                }
            }

            if (currentZeroSequence > maxZeroSequence) maxZeroSequence = currentZeroSequence;

            return maxZeroSequence > 50;
        }

        public static bool CheckNotManyZeros(byte[] fileBytes)
        {
            int maxZeroSequence = 0;
            int currentZeroSequence = 0;

            foreach (var fileByte in fileBytes)
            {

                if (fileByte == 0x00) currentZeroSequence++;
                else
                {
                    if (currentZeroSequence > maxZeroSequence)
                    {
                        maxZeroSequence = currentZeroSequence;
                    }

                    currentZeroSequence = 0;
                }
            }

            if (currentZeroSequence > maxZeroSequence) maxZeroSequence = currentZeroSequence;

            return !(maxZeroSequence > 50);
        }

        public static bool CheckDangerousWords(byte[] fileBytes)
        {
            var words = new string[] { "Virus", "virus", "Infected", "infected", "VIRUS", "INFECTED" };

            foreach (var word in words.Select(x => Encoding.ASCII.GetBytes(x)))
            {
                if (fileBytes.Length < word.Length) continue;

                var index = Enumerable.Range(0, fileBytes.Length - word.Length + 1);

                for (int i = 0; i < word.Length; i++)
                {
                    index = index.Where(x => fileBytes[x + i] == word[i]).ToArray();
                }

                if (index.Any()) return true;
            }

            return false;
        }

        public static bool CheckNotDangerousWords(byte[] fileBytes)
        {
            var words = new string[] { "Virus", "Вирус", "virus", "вирус", "Infected", "Заражен", "infected", "заражен" };

            foreach (var word in words.Select(x => Encoding.ASCII.GetBytes(x)))
            {
                if (fileBytes.Length < word.Length) continue;

                var index = Enumerable.Range(0, fileBytes.Length - word.Length + 1);

                for (int i = 0; i < word.Length; i++)
                {
                    index = index.Where(x => fileBytes[x + i] == word[i]).ToArray();
                }

                if (index.Any()) return false;
            }

            return true;
        }
    }
}
