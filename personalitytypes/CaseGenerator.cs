using DetectiveGame;

public class CaseGenerator
{
    private CaseTemplate template1 = new CaseTemplate
    {
        Titles =
        {
            "Murder at the Three Tuns",
            "Strangling down the Alley"
        },
        Descriptions =
        {
            "A man has met a grisly end behind a local pub.",
            "Someone's been murdered, nobody knows who, it's a tragedy"
        },
        Locations =
        {
            "The Three Tuns Pub",
            "The Alley"
        },
        Suspects =
        {
            "Michael Barrymore",
            "Vincent Screwloose"
        },
        Witnesses =
        {
            "Barbara Windsor",
            "Codesworth Prinkletop"
        },
        EvidenceItems =
        {
            "Knife",
            "A snot filled rag"
        },

        AmountOfSuspects = 1,

        AmountOfWitnesses = 1
    };

    public string GenerateTitle()
    {
        return template1.Titles[0];
    }

    public string GenerateDescription()
    {
        return template1.Descriptions[0];
    }

    public string GenerateLocations()
    {
        return template1.Locations[0];
    }

    public string GenerateSuspects()
    {
        return template1.Suspects[0];
    }

    public string GenerateWitnesses()
    {
        return template1.Witnesses[0];
    }

    public string GenerateEvidenceItems()
    {
        return template1.EvidenceItems[0];
    }

    public TempCase GenerateCase()
    {
        string titles = GenerateTitle();
        string descriptions = GenerateDescription();
        string locations = GenerateLocations();
        string suspects = GenerateSuspects();
        string witnesses = GenerateWitnesses();
        string evidenceItems = GenerateEvidenceItems();

        return new TempCase(titles, descriptions, locations, suspects, witnesses, evidenceItems);


    }
}
