using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodClass : MonoBehaviour
{
    public Vector3 minimum = new Vector3(-4.8f, 0.8f,-1.2f);
    public Vector3 max= new Vector3(-1.5f, 2.8f, 2.2f);
    //void Update()
    //{
    //    if(gameObject.transform.position.y < minimum.y || gameObject.transform.position.x < minimum.x|| gameObject.transform.position.z < minimum.z)
    //    {
    //        resetTransform();
    //    }
    //    if(gameObject.transform.position.y > max.y || gameObject.transform.position.x > max.x || gameObject.transform.position.z > max.z)
    //    {
    //        resetTransform();
    //    }
    //}
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
