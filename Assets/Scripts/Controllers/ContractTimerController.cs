using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractTimerController : MonoBehaviour {
    private ContractInfo _Contract;
    [Header("Time Settings")]
    [SerializeField] private float _HoursToExpire;
    [SerializeField] private float _HoursToComplete;
    [SerializeField] private bool _ContractAccepted;

    private void Start() {
        _HoursToComplete = MinToExpire(_HoursToComplete);
        _HoursToExpire = MinToExpire(_HoursToExpire);
    }

    private void Update() {
        if(_ContractAccepted) {
            _HoursToComplete -= 1;

            if(_HoursToComplete <= 0)
                // TODO: Fail Task
        } else {
            _HoursToExpire -= 1;

            if(_HoursToExpire <= 0) 
                // TODO: Delete the task
        }
    }

    private void ResetContractAccepted() {
        _ContractAccepted = true;
    }

    private float MinToExpire(float timer) {
        return timer = (timer * 60) / DateTimeController.Instance.GetTimeScale();
    }
    
}
