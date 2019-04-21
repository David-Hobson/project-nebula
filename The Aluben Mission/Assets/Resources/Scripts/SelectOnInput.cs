using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;
    public GameObject startObject;

    private bool buttonSelected;

    // Use this for initialization
    void Start()
    {
        startObject = selectedObject;
        OnEnable();
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(null);
        if (startObject != null) { 
            eventSystem.SetSelectedGameObject(startObject);
            startObject.GetComponent<Button>().Select();
            startObject.GetComponent<Button>().OnSelect(null);
            buttonSelected = true;
        }
    }

    //update when there is a keyboard or controller pressed.
    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("P1LSY") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }

    //private void DeselectClickedButton(GameObject button)
    //{
    //    if (EventSystem.current.currentSelectedGameObject == button)
    //    {
    //        EventSystem.current.SetSelectedGameObject(null);
    //    }
    //}
}