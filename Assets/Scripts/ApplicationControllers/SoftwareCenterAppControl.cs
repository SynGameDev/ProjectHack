using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoftwareCenterAppControl : MonoBehaviour, IPointerClickHandler
{
    public ScriptableObject _App;
    
    public void OnPointerClick(PointerEventData pointerEventData) {
        GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>().SetViewingApp(_App);
        OpenApplication();
    }

    private void OpenApplication() {
        GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>().GetViewingAppWindow().SetActive(true);
    }
}
