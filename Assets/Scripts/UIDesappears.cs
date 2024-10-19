using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDesappears : MonoBehaviour
{
    // timer variables
    private float currentTime = 0f;
    public float timeWaiting = 6f;
    void OnEnable()
    {
        currentTime = 0f;
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    currentTime += Time.deltaTime;
    //    if (currentTime > timeWaiting)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
