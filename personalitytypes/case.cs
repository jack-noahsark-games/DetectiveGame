using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace DetectiveGame
//push 
{
    class Case
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public List<Evidence> AvailableEvidence { get; set; } = new List<Evidence>();

        public List<Evidence> FoundEvidence { get; } = new List<Evidence>();

        public List<Person> RelevantNpc { get; set; } = new List<Person>();

        public int CaseProgress { get; private set; } = 0;
        public Case(string title, string description, List<Person> relevantnpc, List <Evidence> availableevidence)
        {
            Title = title;
            Description = description;
            RelevantNpc = relevantnpc;
            AvailableEvidence = availableevidence;
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
            int totalDialogueLines = 0;
            Console.WriteLine("=== DEBUG RUNNING GETTOTALLINES ===");
            foreach (Person npc in RelevantNpc)
            {
                foreach (var personalityEntry in npc.PersonalityDialogue)
                {
                    foreach (var topicEntry in personalityEntry.Value)
                    {
                        totalDialogueLines += topicEntry.Value.Count;
                    }
                }
            }
            return totalDialogueLines;
        }

        public int GetTotalEvidenceAmount()
        {
            int totalEvidenceCount = 0;
            foreach (Evidence e in AvailableEvidence)
            {
                Console.WriteLine($"===DEBUG : evidence name = {e.Name} ===");
                totalEvidenceCount = AvailableEvidence.Count;
            }
            return totalEvidenceCount;
        }

        public void EndCase()
        {
            Console.WriteLine("Case completed.");
            PrintEvidence();
        }
        public Evidence GetEvidenceById(string id)
        {
            foreach (var evidence in AvailableEvidence)
            {
                if (evidence.Id == id)
                {
                    return evidence;
                }
            }

            return null;
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
    
