using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodClass : MonoBehaviour
{
    public int FoodId;
    public Vector3 initialPos;
    public Quaternion intialRot;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = gameObject.transform.position;
        intialRot= gameObject.transform.rotation;
    }
    public void resetTransform()
    {
        gameObject.transform.position= initialPos;
        gameObject.transform.rotation = intialRot;
    }
}
