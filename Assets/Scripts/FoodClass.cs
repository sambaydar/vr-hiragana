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
        grabbablesc.enabled = true;

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
        grabbablesc.enabled = false;
        gameObject.transform.position= initialPos ;
        gameObject.transform.rotation= intialRot ;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        grabbablesc.enabled = true;
    }
    public void resetTransform()
    {
        if (!die)
        {
            die = true;
            if (this != null)
            {
                StartCoroutine(resetTransformCoroutine());
            }
        }
    }

    IEnumerator resetTransformCoroutine()
    {
        yield return new WaitForSeconds(4f);
        if (this == null)
        {
            yield break; // Stop the coroutine if the object no longer exists
        }
        grabbablesc.enabled = false;
        // for now it's like this but actually I may not need to insstantiate, this is going to make the CPU consumption lower I just need to deactivate the xr grabbable.. 
        GameObject newFood = Instantiate(this.gameObject, initialPos, intialRot);
        FoodClass newFoodscript = newFood.GetComponent<FoodClass>();
        newFoodscript.FoodId = FoodId;
        
        Destroy(this.gameObject);

    }

    
}
