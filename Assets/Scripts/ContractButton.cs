using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractButton : MonoBehaviour
{
    private ContractInfo _Contract;

    public void ViewContract() => GameController.Instance.DisplayContract(_Contract);

    public void SetContract(ContractInfo con) => _Contract = con; 
}
