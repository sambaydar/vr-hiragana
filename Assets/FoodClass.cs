using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodClass : MonoBehaviour
{
    public int FoodId;
    public Transform initialTransform;
    // Start is called before the first frame update
    void Start()
    {
        initialTransform=gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
