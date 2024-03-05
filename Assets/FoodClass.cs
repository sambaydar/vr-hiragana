using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodClass : MonoBehaviour
{

    public int FoodId;
    public Vector3 initialPos;
    public Quaternion intialRot;
    private bool firstime = true;
    private bool die= false;
    private XRGrabInteractable grabbablesc;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = gameObject.transform.position;
        intialRot = gameObject.transform.rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        firstime = false;
        grabbablesc = GetComponent<XRGrabInteractable>() ;
    }
    void OnEnable()
    {
        if(!firstime)
        {
            resetpos();
           
        }
    }

        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollideFood")
        {
            resetTransform();
        }
    }
    public void resetpos()
    {

        gameObject.transform.position= initialPos ;
        gameObject.transform.rotation= intialRot ;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
        public void resetTransform()
    {
        if (!die)
        {
            die = true;
            StartCoroutine(resetTransformCoroutine());  
        }
    }

    IEnumerator resetTransformCoroutine()
    {
        yield return new WaitForSeconds(2f);
        
        GameObject newFood = Instantiate(this.gameObject, initialPos, intialRot, gameObject.transform.parent.gameObject.transform);
        grabbablesc.enabled = false; 
        FoodClass newFoodscript = newFood.GetComponent<FoodClass>();
        newFoodscript.FoodId = FoodId;
        
        Destroy(this.gameObject);

    }

    
}
