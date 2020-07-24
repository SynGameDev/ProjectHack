using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[System.Serializable]
public class EmailDatabaseClass
{
    public EmailAccount AccountInfo;
    public List<Email> ReceivedEmailed = new List<Email>();
    public List<Email> SentEmails = new List<Email>();
    public List<Email> DraftEmails = new List<Email>();
}


