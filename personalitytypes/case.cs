using System.Security.Cryptography.X509Certificates;

namespace DetectiveGame
//push 
{
    class Case
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public int CaseProgress { get; private set; } = 0;
        public Case(string title, string description)
        {
            Title = title;
            Description = description;
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

        }
        public void ResetProgress()
        {
            CaseProgress = 0;
            Console.WriteLine("Case has been finished early due to poor questioning.");
        }

        
    }


}
    
