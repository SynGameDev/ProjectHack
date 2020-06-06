using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoftwareCenterAppControl : MonoBehaviour, IPointerClickHandler
{

    
    public void OnPointerClick(PointerEventData pointerEventData) {
        OpenApplication();
    }

    private void OpenApplication() {
        GameObject.FindGameObjectWithTag("SoftwareCenterViewingWindow").SetActive(true);
    }
}
