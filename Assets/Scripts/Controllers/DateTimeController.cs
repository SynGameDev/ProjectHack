using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateTimeController : MonoBehaviour
{
    public static DateTimeController Instance;

    [Header("Time Settings")]
    [SerializeField, Range(1, 4)] private float _TimeScale;
    [SerializeField] private int _Hour;
    [SerializeField] private float _Min;

    [SerializeField] private TextMeshProUGUI _Time;

    [Header("Date Settings")]
    [SerializeField] private int _Date;
    [SerializeField] private int _Month;
    [SerializeField] private int _Year;

    [SerializeField] private TextMeshProUGUI _DateText;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        _DateText.text = DateText();
    }

    private void Update() {
        UpdateTime();
    }

    private void UpdateTime() {
        _Min += 1 * (_TimeScale * Time.deltaTime);
        if(_Min > 59) {
            _Hour += 1;
            _Min = 0;
            if(_Hour > 23) {
                _Hour = 0;
                SceneController.Instance.OpenEndOfDayPopup();
                IncreaseDate();
            }
        }

        _Time.text = TimeText();
    }

    private string TimeText() {
        string _TimeText = "";
        string MinText = "";
        string HourText = "";

        MinText = (_Min >= 9.5f) ? _Min.ToString("F0") : "0" + _Min.ToString("F0");
        HourText = (_Hour >= 10) ? _Hour.ToString("F0") : "0" + _Hour.ToString("F0");

        var prefix = (_Hour > 12) ? "PM" : "AM";

        _TimeText = HourText + ":" + MinText + " " + prefix;
        return _TimeText;
    }

    private void IncreaseDate() {
        _Date += 1;             // Increase Date
        if(_Date > CheckLastDate()) {
            _Date = 1;
            _Month += 1;
            if(_Month > 12) {
                _Month = 1;
                _Year += 1;
            }
        }
        _DateText.text = DateText();
        
    }

    private int CheckLastDate() {
        switch(_Month) {
            case 1:
                return 31;
            case 2:
                return 28;
            case 3:
                return 31;
            case 4:
                return 30;
            case 5:
                return 31;
            case 6:
                return 30;
            case 7:
                return 31;
            case 8:
                return 31;
            case 9:
                return 30;
            case 10:
                return 31;
            case 11:
                return 30;
            case 12:
                return 31;
        }

        return 31;
    }

    private string DateText() {
        return _Date.ToString("F0") + "-" + _Month.ToString("F0") + "-" + _Year.ToString("F0");
    }

    public int[] GetDateArray() {
        int[] data = {_Date, _Month, _Year};
        return data;
    }

    // Getters
    public float GetMin() => _Min;
    public int GetHour() => _Hour;

    public int GetDay() => _Date;
    public int GetMonth() => _Month;
    public int GetYear() => _Year;

    public string GetDateTime() {
        return TimeText() + " " + DateText();
    }
    // Setters

    public void SetTime(int hour, float min, int day, int month, int year) {
        _Min = min;
        _Hour = hour;

        _Date = day;
        _Month = month;
        _Year = year;
    }

    public float GetMinutesInDay() {
        var time_to_seconds = (24 * 60) / _TimeScale;
        return time_to_seconds;
    }

    public float GetTimeScale() {
        return _TimeScale;
    }

}
