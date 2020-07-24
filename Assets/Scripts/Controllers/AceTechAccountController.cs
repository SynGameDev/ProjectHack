using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AceTechAccountController : MonoBehaviour
{
    public static AceTechAccountController Instance;
    private List<AceTechAccount> Accounts = new List<AceTechAccount>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AceTechAccount FindAccByEmail(string Email)
    {
        foreach (var item in Accounts)
        {
            if (item.Email == Email)
                return item;
        }

        return null;
    }

    public AceTechAccount FindAccByUsername(string Username)
    {
        foreach (var item in Accounts)
        {
            if (item.Username == Username)
                return item;
        }

        return null;
    }
    
    public void AddAccount(AceTechAccount acc) => Accounts.Add(acc);
}
