using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace DetectiveGame
//push 
{
    class Case
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Evidence> FoundEvidence { get; } = new List<Evidence>();

        public List<Person> RelevantNpc { get; set; } = new List<Person>();

        public int CaseProgress { get; private set; } = 0;
        public Case(string title, string description, List<Person> relevantnpc)
        {
            Title = title;
            Description = description;
            RelevantNpc = relevantnpc;
        }

        public void ShowCaseDetails()
        {
            Console.WriteLine($"Case details: {Title} - {Description}");
        }

        public void GetRelevantNpcs()
        {
            foreach (Person npc in RelevantNpc)
            {
                Console.WriteLine("=== DEBUG FOREACH RUNNING === ");
                Console.WriteLine(npc.Name);
            }
        }

        public int GetTotalDialogueLines()
        {
            int value = 5;
            return value;
        }

        public void CaseLoader()
        {

        }

        public void ProgressCase(int totalLines)
        {
            int increment = 100 / totalLines;

            CaseProgress += increment;
            Console.WriteLine($"Case progress: {CaseProgress}%");

            if (CaseProgress >= 100)
            {
                CaseProgress = 0;
                Console.WriteLine("Case completed.");

            }
        }

        public void EndCase()
        {
            Console.WriteLine("Case completed.");
            PrintEvidence();
        }
        public void ResetProgress()
        {
            CaseProgress = 0;
            Console.WriteLine("Case has been finished early due to poor questioning.");
        }

        public void AddEvidence(Evidence evidence)
        {
            foreach (var existing in FoundEvidence)
            {
                if (existing.Name == evidence.Name)
                {
                    Console.WriteLine("=== DEBUG === already exists.");
                    return;
                }
            }
        FoundEvidence.Add(evidence);
        Console.WriteLine($"[EVIDENCE ADDED] {evidence.Name}: {evidence.Description}");

        }
        public void PrintEvidence()
        {
            Console.WriteLine();
            Console.WriteLine("=== EVIDENCE COLLECTED ===");

            if (FoundEvidence.Count == 0) 
            {
                Console.WriteLine("You didn't collect any evidence");
                return;
            }

            int index = 1;
            foreach (var e in FoundEvidence)
            {
                Console.WriteLine($"{index}. {e.Name}");
                Console.WriteLine($"    {e.Description}");
                index++;
            }
        }

        
    }


}
    
