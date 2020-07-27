using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractGenerationController : MonoBehaviour
{
    [Header("Timer Values")] 
    [SerializeField] private float _MaxTime;
    [SerializeField] private float _MinTime;
    
    [Header("Current Timer Settings")]
    private float _TimeToNextContract;
    private float _CurrentTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _TimeToNextContract = Random.Range(_MinTime, _MaxTime);
    }

    // Update is called once per frame
    void Update()
    { 
        Countdown();   
    }

    private void Countdown()
    {
        var TotalContracts = GameController.Instance.GetPlayerData().ContractSpaces;
        var AvaContracts = GameController.Instance.GetAvailableContracts().Count;

        if (AvaContracts < TotalContracts)
        {
            if (_CurrentTimer > _TimeToNextContract)
            {
                CreateContract();

                _TimeToNextContract = Random.Range(_MinTime, _MaxTime);            // Setup the next time in seconds.
                _CurrentTimer = 0;
            }

            _CurrentTimer += 1 * Time.deltaTime;
        }
    }

    private void CreateContract()
    {
        NewContractSystem NewContract = new NewContractSystem();
        ContractInfo Contract = new ContractInfo();
        Contract = NewContract.CreateNewContract();

        GameController.Instance.AddContract(Contract);            // Create the contract
        GameController.Instance.AddContractButton(Contract);
        
    }
    
    
}
