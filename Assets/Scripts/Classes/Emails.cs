using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Email
{
    public string ToUser;
    public string FromUser;
    public string Content;
    public string Subject;
    public bool Draft;
}
