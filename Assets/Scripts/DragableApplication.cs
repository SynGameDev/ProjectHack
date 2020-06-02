using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableApplication : MonoBehaviour
{
    private bool _IsDraging;


    private void Update() {
        if(_IsDraging) {                            // If the object is being dragged, than follow the mouse position
            transform.position = Input.mousePosition;
        }
    }

    public void ToggleDrag() => _IsDraging = !_IsDraging;               // Toggle Draging
}
