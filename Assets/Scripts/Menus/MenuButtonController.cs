using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _Hovering = false;
    private Animator _Anim;

    private void Start() {
        _Anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update() {
        _Anim.SetBool("Hovered", _Hovering);
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        _Hovering = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData1) {
        _Hovering = false;
    }
}
