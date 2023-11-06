using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(BoxCollider))]
public class ButtonTrigger : Button
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current),ExecuteEvents.submitHandler);
        UnityEngine.Debug.Log("entered");
      

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
