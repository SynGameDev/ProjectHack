using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContractCompletedPopup : MonoBehaviour
{
    public static ContractCompletedPopup Instance;
    public TextMeshProUGUI StatusText;

    private void Awake() {
        Instance = this;
    }


}
