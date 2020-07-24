using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class EmailButton : MonoBehaviour, IPointerClickHandler
{
    private Email EmailData;
    [SerializeField] private TextMeshProUGUI _Email;
    [SerializeField] private TextMeshProUGUI _Subject;

    [Header("Background Colors")]
    [SerializeField] private Color _NotSelected;
    [SerializeField] private Color _Selected;
    private bool _IsViewing;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(_IsViewing)
        {
            _IsViewing = false;
            this.gameObject.GetComponent<Image>().color = _NotSelected;
            AceMailController.Instance.SetViewingEmail(null);
        } else
        {
            _IsViewing = true;
            this.gameObject.GetComponent<Image>().color = _Selected;
            AceMailController.Instance.SetViewingEmail(EmailData);
        }
    }

    public void SetEmailData(Email eml)
    {
        EmailData = eml;
        EmailAccount Email = GameController.Instance.GetActiveTerminal().EmailAccount;

        if(EmailData.ToUser == "" || EmailData.ToUser == null)
        {
            _Email.text = "draft";
        } else
        {
            _Email.text = EmailData.FromUser;
        }

        
        _Subject.text = EmailData.Subject;
    }
}
