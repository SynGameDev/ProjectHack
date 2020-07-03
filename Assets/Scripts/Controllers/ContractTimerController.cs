using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractTimerController : MonoBehaviour {
    private ContractInfo _Contract;
    [Header("Time Settings")]
    [SerializeField] private float _HoursToExpire;
    [SerializeField] private float _HoursToComplete;
    private float CompleteTimeDetails;
    [SerializeField] private bool _ContractAccepted;

    [Header("Objects")]
    [SerializeField] private Image _TimerIcon;

    [Header("Timer Colors")]
    [SerializeField] private Color _StartColor;
    [SerializeField] private Color _EndColor;


    private void Awake() {
                _TimerIcon = GameObject.FindGameObjectWithTag("TimerIcon").GetComponent<Image>();


    }

    private void Start() {
        
        CompleteTimeDetails = _HoursToComplete;
    }

    private void SetTimers() {
        _HoursToComplete = MinToExpire(_HoursToComplete);
        _HoursToExpire = MinToExpire(_HoursToExpire);
    }

    private void Update() {
        if(_ContractAccepted) {
            _HoursToComplete -= 1 * Time.deltaTime;

            if(_HoursToComplete <= 0)
                FailContract();

            // Lerp the color
            var timer_percetnage = _HoursToComplete / CompleteTimeDetails;
            var lerpcolor = _TimerIcon.color;
            lerpcolor = Color.Lerp(_EndColor, _StartColor, timer_percetnage);

            _Contract.ContractButton.GetComponentInChildren<Image>().color = lerpcolor;
            _Contract.ContractButton.GetComponentInChildren<Image>().fillAmount = timer_percetnage;

            if(GameController.Instance.GetActiveContract() == _Contract) {
                _TimerIcon.fillAmount = timer_percetnage;
               _TimerIcon.color = lerpcolor;
            }


            
        } else {
            _HoursToExpire -= 1 * Time.deltaTime;

            if(_HoursToExpire <= 0) 
                ExpireContract();

            var TimerPercentage = _HoursToExpire / CompleteTimeDetails;

            _Contract.ContractButton.GetComponentInChildren<Image>().fillAmount = TimerPercentage;

            //Lerp the color
            var lerpcolor = _TimerIcon.color;
            lerpcolor = Color.Lerp(_EndColor, _StartColor, TimerPercentage);
            _Contract.ContractButton.GetComponentInChildren<Image>().color = lerpcolor;
        }
    }

    private void ResetContractAccepted() {
        _ContractAccepted = true;
    }

    private void FailContract() {
        GameController.Instance.CompleteContract();
    }

    private void ExpireContract() {
        GameController.Instance.DeclineContract();
    }

    private float MinToExpire(float timer) {
        return timer = (timer * 60) / DateTimeController.Instance.GetTimeScale();
    }

    public void SetContract(ContractInfo contract) {
         _Contract = contract;
         _HoursToComplete = _Contract.TimeToComplete;
         _HoursToExpire = _Contract.TimeToExpire;
         SetTimers(); 
    }
    public void SetStatus(bool Status) => _ContractAccepted = Status;
    
}
