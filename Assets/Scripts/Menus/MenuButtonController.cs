using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    private Animator _Anim;
    private bool _Hovering;

    private void Awake() {
        _Anim = GetComponent<Animator>();
        _Hovering = false;
    }

    public void ToggleHover() {
        if(_Hovering) {
            _Hovering = false;
        } else {
            _Hovering = true;
        }

        _Anim.SetBool("Hover", _Hovering);
    }
}
