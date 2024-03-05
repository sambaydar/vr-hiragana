using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodClass : MonoBehaviour
{

    public int FoodId;
    public Vector3 initialPos;
    public Quaternion intialRot;
    private bool firstime = true;
    private bool die= false;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = gameObject.transform.position;
        Debug.Log(initialPos);
        intialRot = gameObject.transform.rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        firstime = false;

    }
    void OnEnable()
    {
        if(!firstime)
        {
            resetTransform();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollideFood")
        {
            resetTransform();
        }
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
        GameObject newFood= Instantiate(this.gameObject, initialPos, intialRot, gameObject.transform.parent.gameObject.transform);
        FoodClass newFoodscript = newFood.GetComponent<FoodClass>();
        newFoodscript.FoodId = FoodId;
        
        Destroy(this.gameObject);

    }

    
}
