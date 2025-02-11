using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(BoxCollider))]
public class ButtonTrigger : Button
{
    // Enable the button
    public void EnableButton()
    {
        this.interactable = true;
    }

    // Disable the button
    public void DisableButton()
    {
        this.interactable = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Hand"){
            ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

}
