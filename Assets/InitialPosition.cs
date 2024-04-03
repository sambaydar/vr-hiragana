using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPosition : MonoBehaviour
{
    public Transform origin;
    public Transform target;
    public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        Recenter();
    }
    private void Recenter()
    {
        Vector3 offset = head.position - origin.position ;
        offset.y = 0;
        origin.position = target.position - offset ;


        
    }
    
}
