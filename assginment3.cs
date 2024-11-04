using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };
            // You can convert string to double by
            // double.Parse(str)

            int stdCount = data.GetLength(0) - 1;
            // ---------- TODO ----------
            double[] MathScore= new double[stdCount];
            double[] ScienceScore= new double[stdCount];
            double[] EnglishScore= new double[stdCount];
            double[] TotalScore = new double[stdCount];

            for (int i = 1; i <= stdCount ; i++) {
                MathScore[i-1] = double.Parse(data[i,2]);
                ScienceScore[i-1] = double.Parse(data[i ,3]);
                EnglishScore[i-1] = double.Parse(data[i ,4]);
                TotalScore[i-1] = double.Parse(data[i,2]) + double.Parse(data[i,3]) + double.Parse(data[i,4]);
            }
            double averageMath = MathScore.Average();
            double averageScience = ScienceScore.Average();
            double averageEnglish = EnglishScore.Average();
            double Maxmath = MathScore.Max();
            double Minmath = MathScore.Min();
            double MaxScience = ScienceScore.Max();
            double MinScience = ScienceScore.Min();
            double MaxEnglish = EnglishScore.Max();
            double MinEnglish = EnglishScore.Min();
            var studentsWithTotalScores = new
            {
                Names = new string[stdCount],
                TotalScores = TotalScore
            };

            for (int i = 1; i <= stdCount; i++)
            {
                studentsWithTotalScores.Names[i - 1] = data[i, 1];
            }

            var rankedStudents = studentsWithTotalScores.Names
                .Select((name, index) => new 
                {
                    Name = name,
                    Score = studentsWithTotalScores.TotalScores[index]
                })
                .OrderByDescending(s => s.Score)
                .Select((s, index) => new { s.Name, Rank = index + 1 })
                .ToList();

            Console.WriteLine("Average Scores:");
            Console.WriteLine($"Math: {averageMath:F2}");
            Console.WriteLine($"Science: {averageScience:F2}");
            Console.WriteLine($"English: {averageEnglish:F2}");
            Console.WriteLine();
            Console.WriteLine("Max and min Scores: ");
            Console.WriteLine($"Math: ({Maxmath}, {Minmath})");
            Console.WriteLine($"Science: ({MaxScience}, {MinScience})");
            Console.WriteLine($"English: ({MaxEnglish}, {MinEnglish})");
            Console.WriteLine("Students rank by total scores:");
            var finalOutput = studentsWithTotalScores.Names
                .Select((name, index) => new 
                {
                    Name = name,
                    Rank = rankedStudents.First(r => r.Name == name).Rank
                })
                .ToList();
                
            foreach (var student in finalOutput)
            {
                string suffix = "th";
                if (student.Rank  == 1 )
                    suffix = "st";
                else if (student.Rank == 2 )
                    suffix = "nd";
                else if (student.Rank == 3 )
                    suffix = "rd";

                Console.WriteLine($"{student.Name}: {student.Rank}{suffix}");
            }
            // --------------------
        }
    }
}