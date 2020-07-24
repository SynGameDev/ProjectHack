using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AceTechAccount
{
    // Account Details
    public string FirstName;
    public string LastName;
    public string Username;
    public string Server;
    public string Email;
    public string Password;

    // Email Data
    public List<Email> SentEmails = new List<Email>();
    public List<Email> ReceivedEmails = new List<Email>();
    public List<Email> DraftEmails = new List<Email>();

}