using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickObject : MonoBehaviour, IPointerClickHandler
{
    public static RightClickObject Instance;

    [Header("Dropdown Items")]
    [SerializeField] private List<GameObject> DropdownItems = new List<GameObject>();

    [Header("Object")]
    [SerializeField] private GameObject _DropdownContainer;
    [SerializeField] private string _MenuSpawnPointTag;
    private GameObject LoadedDropdownMenu;
    

    [Header("Variables")]
    private bool _DropdownMenuOpen;

    private void Awake() {
        if(Instance == null) {
            Instance = this;

        } else {
            Destroy(this.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Right) {
            DisplayMenu(eventData.position);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(_DropdownMenuOpen) {
                HideDropdownMenu();
            }
        }
    }

    private void DisplayMenu(Vector3 Position) {
        // Create the dropdown menus
        LoadedDropdownMenu = Instantiate(_DropdownContainer);
        //LoadedDropdownMenu.transform.localScale = Vector3.one;
        LoadedDropdownMenu.transform.SetParent(GameObject.FindGameObjectWithTag(_MenuSpawnPointTag).transform);
        LoadedDropdownMenu.transform.position = Position;

        // Loop through each item in for the menu
        foreach(var item in DropdownItems) {
            var go = Instantiate(item);
            go.transform.SetParent(LoadedDropdownMenu.transform);
            go.transform.localScale = Vector3.one;
        }

        _DropdownMenuOpen = true;
        GameController.Instance.SetDropdownItem(this.gameObject);
    }

    public void HideDropdownMenu() {
        Destroy(LoadedDropdownMenu);
        _DropdownMenuOpen = false;
    }
}
