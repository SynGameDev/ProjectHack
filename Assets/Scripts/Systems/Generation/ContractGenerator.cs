using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ContractGenerator
{
    private static List<string> _Actions = new List<string>();
    public static List<ApplicationClass> AppsToInstall = new List<ApplicationClass>();
    public static List<string> Objective = new List<string>();
    public static List<TextFile> TextFiles = new List<TextFile>();

    private static ContractInfo Contract = new ContractInfo(); // Create Contract
    private static TerminalInfo Terminal;

    public void CreateContract()
    {
        GenerateActions();
    }

    public ContractInfo NewContract()
    {
        // Create the terminal & Add it to the list of terminals.
        Terminal = GenerateTerminal();
        GameController.Instance.AddNewTerminal(Terminal);


        var totalObjectives = UnityEngine.Random.Range(1, 8); // Total number of objectives          
        for (int i = 0; i > totalObjectives; i++) // Loop through & create the objectives
        {
            CreateObjective();
        }


        Terminal.InstalledApplication = AppsToInstall; // Setup the installed applications on the system
        string[] data = Name(); // Get the names

        Contract.ContractOwner = new AceTechAccount();

        Contract.ContractDifficulty = 1; // Set the difficulty
        Contract.ContractStatus = "Pending"; // Status of the contract

        // TODO: Set a timer
        Contract.TimeToComplete = 3; // Set the time to complete
        Contract.TimeToExpire = 3; // Set the time to expire

        Contract.Terminal.Add(Terminal); // Add the terminal to the contract

        GameController.Instance.AddNewTerminal(Terminal); // add the terminal the game

        return Contract;


        // TODO: Decide whether to use a already made terminal or create a new one.

    }

    private TerminalInfo GenerateTerminal()
    {
        Terminal = new TerminalInfo(); // New Terminal class

        Terminal.EmailAccount = CreateAceTechAccount(); // Setup the email class
        Terminal.InstalledApplication = AppsToInstall; // Add the installed apps
        Terminal.BackDoorInstalled = false; // Setup the backdoor to false        TODO: This may change

        // TODO: Anti-virus based on players current level
        Terminal.AntiVirusLevel = Random.Range(0, 2); // Set a random value for the time been
        Terminal.HHD = Random.Range(5, 220); // Set Random Space Range
        Terminal.TemrinalName = CreateTerminalName(); // Setup the name of the terminal
        Terminal.TerminalType = "Desktop"; // TODO: Set Based on the contract type
        Terminal.TextFileList = GenerateRandomTextFiles();
        return Terminal;

    }

    private List<string> CreateObjective()
    {
        List<string> NewObjectiveList = new List<string>();
        var Object = _Actions[Random.Range(0, _Actions.Count - 1)]; // Get a action

        switch (Object)
        {
            case "Install":
                Objective.Add(CreateInstallObj());
                break;
            case "Uninstall":
                Objective.Add(CreateUninstallObj());
                break;
            case "Hide":
                Objective.Add(CreateHideObj());
                break;
            case "Show":
                Objective.Add(CreateShowObj());
                break;
            case "Delete":
                Objective.Add(CreateDeleteObj());
                break;
            default:
                Objective.Add(CreateHideObj());
                break;
        }

        return NewObjectiveList;
    }
    
    private string CreateInstallObj()
    {
        ApplicationClass App = SelectApp();                
        string Term = Contract.Terminal[0].TerminalIP;

        return "Install " + App.ApplicationName + " IP: " + Term;
    }

    private string CreateUninstallObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;

        if (!Objective.Contains("Install " + App.ApplicationName + " IP: " + Term))
        {
            AppsToInstall.Add(App);
        }

        return "Uninstall " + App.ApplicationName + " IP: " + Term;
    }

    private string CreateHideObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;

        if (!Objective.Contains("Install " + App.ApplicationName + " IP: " + Term))
        {
            Objective.Add("Install " + App.ApplicationName + " IP: " + Term);
        }

        return "Hide " + App.ApplicationName + " IP: " + Term;
    }

    private string CreateShowObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;

        AppsToInstall.Add(App);
        return "Show " + App.ApplicationName + " IP: " + Term;
    }

    private string CreateDeleteObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;
        var txt = new TextFile();
        TextFiles.Add(txt);

        return "Delete " + txt.FileName + "." + txt.ext + " IP: " + Term;

    }

    private ApplicationClass SelectApp()
    {
        var AppDB = ApplicationDatabase.Instance.GetSoftwareApps();
        return AppDB[Random.Range(0, AppDB.Count)];
    }

    
    private AceTechAccount CreateAceTechAccount()
    {
        AceTechAccount AceTech = new AceTechAccount();            // New Account Class
        string[] Names = Name();                    // Get random names
        
        // Setup the names
        AceTech.FirstName = Names[0];
        AceTech.LastName = Names[1];
        AceTech.Username = Names[2];

        // Email Account Details
        AceTech.Server = "@acemail.com";
        AceTech.Email = AceTech.Username + AceTech.Server;
        AceTech.Password = "Temp"; // TODO: Create Password Gen

        // Emails
        // TODO: generate Random Emails

        return AceTech;

    }

    private string[] Name()
    {
        var Names = new NamesData();                // New names database
        
        // Open the file and assign it to the names database
        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/Databases/NamesDatabase.json"))
        {
            Names = JsonUtility.FromJson<NamesData>(r.ReadToEnd());
        }

        // Select the random names
        string fname = Names.FirstNames[Random.Range(0, Names.FirstNames.Count)];
        string lname = Names.LastNames[Random.Range(0, Names.LastNames.Count)];
        string user = Names.Usernames[Random.Range(0, Names.Usernames.Count)];


        string[] data = {fname, lname, user};            // Add the names to an array
        return data;                // Return the array
    }

    private string GetMissionMessage()
    {
        List<string> Messages = new List<string>();                // Create a new list to store template messages

        // Open the Template databases & assign the message list to messages
        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/Databases/TemplateDatabase.json"))
        {
            Messages = JsonUtility.FromJson<TemplateDatabases>(r.ReadToEnd()).MissionMessage;
        }

        return Messages[Random.Range(0, Messages.Count)];                // Return a random message

    }

    private string CreateMessage(string template)
    {
        template = template.Replace("[user]",
            GameController.Instance.GetPlayerData().PlayerName); // Set the player name
        template = template.Replace("[owner]", Contract.ContractOwner.Username); // Set the from message
        return template;
    }

    private string CreateTerminalName()
    {
        var Account = Terminal.EmailAccount;
        // string[] CharArray = {"", "_", "-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};

        var NameSelection = Random.Range(1, 4);

        switch (NameSelection)
        {
            case 1:
                return Account.FirstName + "_PC";
            case 2:
                return Account.Username + "_PC";
            case 3:
                return Account.FirstName.Substring(0, 1) + Account.LastName;
        }

        return Account.FirstName = "_PC";
    }


    private List<TextFile> GenerateRandomTextFiles()
    {
        List<TextFile> Files = new List<TextFile>(); // List of files to add
        List<TextFile> Temp = new List<TextFile>(); // All the template files

        // Get the template files
        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/Databases/TemplateDatabase.json"))
        {
            Temp = JsonUtility.FromJson<TemplateDatabases>(r.ReadToEnd()).TextFileContent;
        }


        var file_num =
            Random.Range(0,
                8); // Set the amount of files needed TODO: Replace this with a method that will determine based on the mission type
        // Run a loop for the amount of files that will need to be on the system
        for (int i = 0; i < file_num; i++)
        {
            var file = Temp[Random.Range(0, Temp.Count)]; // Get a file
            // Check if the system already contains that file & then duplicate it
            if (Files.Contains(file))
            {
                file.FileName = file.FileName + "(" + CountFileDuplicates(file, Files) + ")";
            }

            Files.Add(file); // Add the file to the list
        }

        return Files;
    }

    private string TextFileListGeneration(string Content)
    {
        Content = Content.Replace("(FOOD)", GetFoodList());
        
        return Content;
    }

    private string GetFoodList()
    {
        string data = "";                // Blank script
        List<string> Foods = new List<string>();            // New List of foods
        var test = GetTemplateList().FoodTemp;            // Foods List template

        // Select random foods
        for (int i = 0; i < Random.Range(0, 16); i++)
        {
            Foods.Add(test[Random.Range(0, test.Count)]);
        }

        // Loop through the foods and count duplicates
        foreach (var item in Foods)
        {
            Foods = CountItemRepeat(item, Foods);
        }

        for (int i = 0; i < Foods.Count; i++)
        {
            if (i > 0)
            {
                data += "-" + Foods[i] + "\r\n";
            }
            else
            {
                data += "-" + Foods[i];
            }
        }
        
        return data;
    }

    private List<string> CountItemRepeat(string item, List<string> data)
    {
        var count = 0;
        foreach (var c in data)
        {
            if (item == c)
            {
                count++;
            }
        }

        data.Remove(item);

        string d = "";
        if (count > 0)
        {
            d = item + " x" + count;
        }
        else
        {
            d = item;
        }
        
        data.Add(d);

        return data;
    }

    private string CountFileDuplicates(TextFile file, List<TextFile> list)
    {
        var count = 0;            // Init the count
        // Foreach item in the list
        foreach (var item in list)
        {
            // If the item is each to the file then increase the count
            if (item == file)
            {
                count++;
            }
        }

        return count.ToString();
    }

    /// <summary>
    /// This Method will init the list of actions that can be used for the contract
    /// </summary>
    private void GenerateActions()
    {
        _Actions.Add("Install");
        _Actions.Add("Uninstall");
        _Actions.Add("Hide");
        _Actions.Add("Show");
        _Actions.Add("Delete");;
    }

    private TemplateDatabases GetTemplateList()
    {
        TemplateDatabases Temp = new TemplateDatabases();
        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/Databases/TemplateDatabase.json"))
        {
            return JsonUtility.FromJson<TemplateDatabases>(r.ReadToEnd());
        }

        return null;
    }
}

public class TemplateDatabases
{
    public List<Email> EmailTemp = new List<Email>();
    public List<TextFile> TextFileContent = new List<TextFile>();
    public List<string> MissionMessage = new List<string>();
    public List<string> FoodTemp = new List<string>();
}

public class NamesData
{
    public List<string> FirstNames = new List<string>();
    public List<string> LastNames = new List<string>();
    public List<string> Usernames = new List<string>();
    
}