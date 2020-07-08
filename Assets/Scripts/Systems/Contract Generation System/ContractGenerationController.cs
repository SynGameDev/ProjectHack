using Boo.Lang;
using System;
using UnityEngine;
using UnityEngine.Video;

public class ContractGenerationController : MonoBehaviour
{
    
    

    private void Awake()
    {
        InitializeActions();
    }

    private void Start()
    {
        TerminalGenerator = this.gameObject.GetComponent<TerminalGeneration>();
    }



    
}